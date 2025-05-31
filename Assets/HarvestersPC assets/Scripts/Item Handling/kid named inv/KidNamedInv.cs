using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KidNamedInv : MonoBehaviour
{
    // Item stuff
    [Header("Item Database")]
    [SerializeField] private List<Item> itemDatabase;

    [Header("Transforms")]
    [SerializeField] private Transform defaultToolPosition;
    [SerializeField] private Transform defaultPlantPosition;

    [Header("Misc")]
    [SerializeField] private MeshCollider bagCollider;
    [SerializeField] private RuntimeAnimatorController controller;

    private Item currentToolItem;
    private GameObject grabbedTool;
    private Item currentPlantItem;
    private GameObject grabbedPlant;


    // Inventory related stuff
    [SerializeField] private Transform canvas; // inventory canvas
    private List<Item> inventoryDict = new List<Item>(new Item[10]);

    private Dictionary<string, (string, GameObject)> toolsDict;
    private Dictionary<string, GameObject> spritesDict;

    private int slotSelected;
    private int oldSlotSelected;

    private void Update()
    {
        //EquipItem();
        CheckRaycast();
        UseItem();
        DropInput();
    }

    private Item FindItemByName(string name)
    {
        foreach (Item item in itemDatabase)
        {
            if (item.itemName == name)
            {
                return item;
            }
        }
        return null;
    }

    private void FreezeObject(GameObject obj) => obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    private void CheckRaycast()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
            LayerMask Grabbables = 1 << 6;

            if (!Physics.Raycast(ray, out RaycastHit hit, 1.5f, Grabbables)) return;

            GameObject hitObj = hit.collider.gameObject;
            Item item = FindItemByName(hitObj.name);

            if (item.itemType == ItemType.Tool && currentPlantItem == null)
            {
                currentToolItem = item;
                grabbedTool = hitObj;

                FreezeObject(grabbedTool);

                if (currentToolItem.itemType == ItemType.Seedbag && grabbedTool.GetComponent<Animator>())
                {
                    Destroy(grabbedTool.GetComponent<Animator>());
                    Destroy(grabbedTool.transform.Find("Item particle").gameObject);
                }
                //AddItemToInventory();
                UpdateItemPositionAndRotation(ref grabbedTool);
            }

            if (item.itemType == ItemType.Crop && currentPlantItem == null)
            {
                currentPlantItem = item;
                grabbedPlant = hitObj;

                grabbedPlant.transform.SetParent(null);

                grabbedPlant.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;

                if (!grabbedPlant.transform.Find(currentPlantItem.itemName).gameObject.GetComponent<Animator>())
                {
                    Animator plantAnimator = grabbedPlant.transform.Find(currentPlantItem.itemName).gameObject.AddComponent<Animator>();
                    plantAnimator.runtimeAnimatorController = controller;
                }
                UpdateItemPositionAndRotation(ref grabbedPlant);
            }
        }
    }

    private void UseItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbedTool != null && grabbedPlant == null)
            { UseTool(); }
        }

        if (Input.GetKeyDown(KeyCode.F))
        { UseBag(); }
    }

    private void UseTool()
    {
        Animator animator;
        animator = grabbedTool.transform.Find(grabbedTool.name).GetComponent<Animator>();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
        {
            animator.Play("UseItem");

            if (currentToolItem.itemType == ItemType.Seedbag)
            {
                SeedBagManager sbagManager = grabbedTool.GetComponent<SeedBagManager>();

                if (sbagManager.timesUsed < sbagManager.maxTimesUsed)
                { grabbedTool.transform.Find(grabbedTool.name).Find("Seed Particles").GetComponent<ParticleSystem>().Play(); }
            }
        }
    }

    private void UseBag()
    {
        Animator animator;
        if (grabbedPlant != null && currentToolItem.itemName == "Bag")
        {
            animator = grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>();
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState") && grabbedTool.GetComponent<BagInventory>().isBagOpen == true)
            {
                animator.Play("UsePlant");
                StartCoroutine(DepositPlant());
            }
        }
    }

    private void UpdateItemPositionAndRotation(ref GameObject grabbedObject)
    {
        // set rotations
        Transform objTransform = grabbedObject.transform;
        objTransform.parent = defaultToolPosition;
        objTransform.SetPositionAndRotation(defaultToolPosition.position, defaultToolPosition.rotation);
    }

    private void DropInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // drop plant first if theres a plant and tool at the same time
            if (currentPlantItem != null)
            { DropItem(ref grabbedPlant, ref currentPlantItem); }

            else if (currentToolItem != null)
            { DropItem(ref grabbedTool, ref currentToolItem); }
        }
    }

    private void DropItem(ref GameObject grabbedObject, ref Item currentItem)
    {
        if (grabbedObject.GetComponent<Collider>())
        { grabbedPlant.GetComponent<Collider>().enabled = true; }

        grabbedObject.transform.Find(grabbedObject.name).GetComponent<Animator>().Play("DefaultState");
        if (currentItem.itemType == ItemType.Crop)
        {
            Destroy(grabbedObject.transform.Find(currentPlantItem.itemName).GetComponent<Animator>());
            StopCoroutine(DepositPlant());
        }

        currentItem = null;
        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        grabbedObject.transform.parent = null;
        grabbedPlant = null;

        if (currentItem.itemType == ItemType.Tool || currentItem.itemType == ItemType.Seedbag)
        { /*RemoveItemFromSlot(); */ }
    }

    private IEnumerator DepositPlant()
    {
        bagCollider.enabled = false;
        yield return new WaitForSeconds(0.98f);

        bagCollider.enabled = true;

        if (grabbedPlant == null) yield break;

        grabbedPlant.transform.parent = null;
        grabbedPlant = null;
        currentPlantItem = null;
    }

    /*
    private void EquipItem()
    {
        for (int i = 0; i <= 9; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;

            if (Input.GetKeyDown(key))
            {
                slotSelected = i;
                SelectItem();
                break;
            }
        }
    }

    public void AddItemToInventory(Item item)
    {
        if (slotSelected == 0) return;

        int index = inventoryDict.Count + 1;
        inventoryDict[index] = item;

        canvas.Find($"Slot{index}").gameObject.SetActive(true);
        Instantiate(item.itemSprite, canvas.Find($"Slot{index}"));
        slotSelected = index;
        SelectItem();
    }

    private void SelectItem()
    {
        //Debug.Log(inventoryDict.Count);

        currentTool = null;
        grabbedTool = null;

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
            FreezeObject(newTool);
            newTool.name = inventoryDictTuple.Item1;

            currentTool = inventoryDictTuple.Item1;
            grabbedTool = newTool;

            if (newTool.GetComponent<CapsuleCollider>())
            {
                newTool.GetComponent<CapsuleCollider>().enabled = false;
            }
            else if (newTool.GetComponent<SphereCollider>())
            {
                newTool.GetComponent<SphereCollider>().enabled = false;
            }

            UpdateItemPositionAndRotation();

            Transform slot = canvas.Find($"Slot{slotSelected}");
            RectTransform rect = slot.GetComponent<RectTransform>();
            int randomNumber = Random.Range(0, 1);
            slot.GetComponent<Image>().color = new Color(0.5490196f, 0.7529412f, 0.8f);

            // randomly rotates it to left or right
            switch (randomNumber)
            {
                case 0:
                    rect.pivot = new Vector2(1f, 1f);
                    rect.rotation = Quaternion.Euler(0, 0, -5);
                    break;

                case 1:
                    rect.pivot = new Vector2(0.15f, 1f);
                    rect.rotation = Quaternion.Euler(0, 0, 5);
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
    }*/
}
