using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main.ItemHandling
{
    public class Inventory : MonoBehaviour
    {
        private readonly SortedDictionary<int, (string, GameObject)> inventoryDict = new SortedDictionary<int, (string, GameObject)>();

        private int slotSelected;
        private int oldSlotSelected;

        private ItemManager itemManager;
        private Transform canvas;

        [Header("Sprites")]
        [SerializeField] private GameObject ScytheSprite;
        [SerializeField] private GameObject AxeSprite;
        [SerializeField] private GameObject HoeSprite;

        private void Start()
        {
            canvas = gameObject.transform.Find("Canvas");
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
            inventoryDict[inventoryDict.Count + 1] = (itemManager.currentTool, itemManager.grabbedTool);
            slotSelected = inventoryDict.Count;
            canvas.Find($"Slot{slotSelected}").gameObject.SetActive(true);

            ExtendUI();
            SelectItem();
        }

        private void ExtendUI()
        {
            (string, GameObject) itemTuple = inventoryDict[slotSelected];
            switch (itemTuple.Item1)
            {
                case "Scythe":
                    Instantiate(ScytheSprite, canvas.Find($"Slot{slotSelected}"));
                    break;
                case "Axe":
                    Instantiate(AxeSprite, canvas.Find($"Slot{slotSelected}"));
                    break;
                case "Hoe":
                    Instantiate(HoeSprite, canvas.Find($"Slot{slotSelected}"));
                    break;
            }
        }

        private void SelectItem()
        {
            itemManager.currentTool = null;
            Destroy(itemManager.grabbedTool);
            itemManager.grabbedTool = null;

            // reset previously selected slot appearance after selecting new slot
            if (oldSlotSelected != 0) 
            {
                ResetSlot(oldSlotSelected);
            }
            
            if (inventoryDict.ContainsKey(slotSelected) && slotSelected != 0)
            {
                (string, GameObject) itemTuple = inventoryDict[slotSelected];

                itemManager.currentTool = itemTuple.Item1;
                    
                GameObject newTool = Instantiate(itemTuple.Item2);
                string[] toolNameParts = newTool.name.Split("(");
                newTool.name = toolNameParts[0];
                itemManager.grabbedTool = newTool;
                itemManager.SetParents();

                Transform selectedSlot = canvas.Find($"Slot{slotSelected}");
                RectTransform selectedRectTransform = selectedSlot.GetComponent<RectTransform>();
                selectedSlot.GetComponent<Image>().color = new Color(0.5490196f, 0.7529412f, 0.8f);

                int randomNumber = Random.Range(0, 1);

                // randomly rotates it to left or right
                switch (randomNumber)
                {
                    case 0:
                        selectedRectTransform.pivot = new Vector2(1f, 1f);
                        selectedRectTransform.rotation = Quaternion.Euler(0, 0, -5);
                        break;

                    case 1:
                        selectedRectTransform.pivot = new Vector2(0.15f, 1f);
                        selectedRectTransform.rotation = Quaternion.Euler(0, 0, 5);
                        break;
                }
                oldSlotSelected = slotSelected;
            }
            else return;
        }

        private void ResetSlot(int slotToReset)
        {
            Transform selectedSlot = canvas.Find($"Slot{slotToReset}");
            selectedSlot.GetComponent<Image>().color = new Color(0.3686275f, 0.5803922f, 0.6313726f);
            selectedSlot.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            selectedSlot.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        }

        public void RemoveItemFromSlot()
        {
            inventoryDict.Remove(slotSelected);

            ResetSlot(slotSelected);

            canvas.Find($"Slot{slotSelected}").gameObject.SetActive(false);

            foreach (Transform child in canvas.Find($"Slot{slotSelected}"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
