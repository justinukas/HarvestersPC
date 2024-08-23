using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main.ItemHandling
{
    public class Inventory : MonoBehaviour
    {
        private readonly Dictionary<int, (string, GameObject)> inventoryDict = new Dictionary<int, (string, GameObject)>();

        private int slotSelected;
        private int slotCount;

        private ItemManager itemManager;

        [Header("Sprites")]
        [SerializeField] private GameObject ScytheSprite;
        [SerializeField] private GameObject AxeSprite;
        [SerializeField] private GameObject HoeSprite;

        private void Start()
        {
            itemManager = GameObject.Find("Player").GetComponent<ItemManager>();
        }

        private void Update()
        {
            EquipItem();
        }

        private void EquipItem()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                slotSelected = 1;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                slotSelected = 2;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                slotSelected = 3;
                SelectItem();
            }

            // this deselects shit
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                slotSelected = 0;
                SelectItem();
            }
        }

        public void AddItemToInventory()
        {
            if (itemManager.currentTool != null && itemManager.grabbedTool != null)
            {
                slotCount++;
                slotSelected = slotCount;

                inventoryDict[slotCount] = (itemManager.currentTool, itemManager.grabbedTool);

                ExtendUI();
                SelectItem();
            }
        }

        private void ExtendUI()
        {
            (string, GameObject) itemTuple = inventoryDict[slotSelected];
            switch (itemTuple.Item1)
            {
                case "Scythe":
                    Instantiate(ScytheSprite, gameObject.transform.Find("Canvas").Find($"Slot{slotSelected}"));
                    break;
                case "Axe":
                    Instantiate(AxeSprite, gameObject.transform.Find("Canvas").Find($"Slot{slotSelected}"));
                    break;
                case "Hoe":
                    Instantiate(HoeSprite, gameObject.transform.Find("Canvas").Find($"Slot{slotSelected}"));
                    break;
            }
        }

        private void SelectItem()
        {
            itemManager.currentTool = null;
            itemManager.grabbedTool = null;

            gameObject.transform.Find("Canvas").Find($"Slot{slotSelected}").GetComponent<Image>().color = new Color(0.3686275f, 0.5803922f, 0.6313726f);

            if (slotSelected <= slotCount && slotSelected != 0)
            {
                if (inventoryDict.ContainsKey(slotSelected))
                {
                    (string, GameObject) itemTuple = inventoryDict[slotSelected];

                    itemManager.currentTool = itemTuple.Item1;
                    itemManager.grabbedTool = itemTuple.Item2;

                    // set color to 8CC0CC;
                    // set anchor pivots to x=1 y=1 and set rotation to -10 OR
                    // set anchor to x=0.15 y=1 and set rotation to 10,
                    // to indicate selection
                }

                else return;
            }
        }
    }
}
