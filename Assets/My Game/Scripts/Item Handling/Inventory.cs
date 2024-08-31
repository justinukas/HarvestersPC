using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Main.ItemHandling
{
    public class Inventory : MonoBehaviour
    {
        private readonly SortedDictionary<int, (string, GameObject)> inventoryDict = new SortedDictionary<int, (string, GameObject)>();
        private Dictionary<string, (string, GameObject)> toolsDict;
        private Dictionary<string, GameObject> spritesDict;

        private int slotSelected;
        private int oldSlotSelected;

        private ItemManager itemManager;
        private Transform canvas;

        [Header("Tool Objects")]
        [SerializeField] private GameObject ScytheObject;
        [SerializeField] private GameObject AxeObject;
        [SerializeField] private GameObject HoeObject;

        [Header("Sprites")]
        [SerializeField] private GameObject ScytheSprite;
        [SerializeField] private GameObject AxeSprite;
        [SerializeField] private GameObject HoeSprite;

        private void Start()
        {
            canvas = gameObject.transform.Find("Canvas");
            itemManager = GameObject.Find("Player").GetComponent<ItemManager>();

            toolsDict = new Dictionary<string, (string, GameObject)>()
            {
                {"Scythe", ("Scythe", ScytheObject) },
                {"Axe", ("Axe", AxeObject) },
                {"Hoe", ("Hoe", HoeObject) }
            };

            spritesDict = new Dictionary<string, GameObject>()
            {
                {"Scythe", ScytheSprite },
                {"Axe", AxeSprite },
                {"Hoe", HoeSprite }
            };
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

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                slotSelected = 4;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                slotSelected = 5;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                slotSelected = 6;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                slotSelected = 7;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                slotSelected = 8;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                slotSelected = 9;
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
            if (slotSelected != 10)
            {
                inventoryDict[inventoryDict.Count + 1] = (itemManager.currentTool, itemManager.grabbedTool);
                (string, GameObject) inventoryDictTuple = inventoryDict[inventoryDict.Count];
                inventoryDict[inventoryDict.Count] = toolsDict[inventoryDictTuple.Item1];
                Destroy(itemManager.grabbedTool);

                slotSelected = inventoryDict.Count;

                canvas.Find($"Slot{slotSelected}").gameObject.SetActive(true);

                AddSpriteToSlot();
                SelectItem();
            }
        }

        private void AddSpriteToSlot()
        {
            (string, GameObject) inventoryDictTuple = inventoryDict[slotSelected];
            GameObject newSprite = spritesDict[inventoryDictTuple.Item1];
            Instantiate(newSprite, canvas.Find($"Slot{slotSelected}"));
        }

        private void SelectItem()
        {
            Debug.Log(inventoryDict.Count);
            itemManager.currentTool = null;
            itemManager.grabbedTool = null;

            foreach (Transform child in Camera.main.transform.Find("Tool Position"))
            {
                Destroy(child.gameObject);
            }

            // reset previously selected slot appearance after selecting new slot
            if (oldSlotSelected != 0) 
            {
                ResetSlot(oldSlotSelected);
            }
            
            if (inventoryDict.ContainsKey(slotSelected) && slotSelected != 0 && slotSelected != 10)
            {
                (string, GameObject) inventoryDictTuple = inventoryDict[slotSelected];

                // dupe
                GameObject newTool = Instantiate(inventoryDictTuple.Item2);
                newTool.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                newTool.name = inventoryDictTuple.Item1;

                itemManager.currentTool = inventoryDictTuple.Item1;
                itemManager.grabbedTool = newTool;

                if (newTool.GetComponent<CapsuleCollider>())
                {
                    newTool.GetComponent<CapsuleCollider>().enabled = false;
                }
                else if (newTool.GetComponent<SphereCollider>())
                {
                    newTool.GetComponent<SphereCollider>().enabled = false;
                }

                itemManager.SetParents();

                Transform selectedSlot = canvas.Find($"Slot{slotSelected}");
                RectTransform selectedRectTransform = selectedSlot.GetComponent<RectTransform>();
                int randomNumber = Random.Range(0, 1);
                selectedSlot.GetComponent<Image>().color = new Color(0.5490196f, 0.7529412f, 0.8f);

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

        private void ResetSlot(int slotToReset)
        {
            Transform selectedSlot = canvas.Find($"Slot{slotToReset}");

            selectedSlot.GetComponent<Image>().color = new Color(0.3686275f, 0.5803922f, 0.6313726f);
            selectedSlot.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            selectedSlot.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
