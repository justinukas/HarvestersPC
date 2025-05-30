using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KidNamedInv : MonoBehaviour
{
    [SerializeField] MeshCollider bagCollider;
    [SerializeField] private RuntimeAnimatorController controller;

    private readonly SortedDictionary<int, (string, GameObject)> inventoryDict = new();
    public static string[] itemNames = new string[] { "Scythe", "Axe", "Hoe", "Bag", "Carrot Seed Bag", "Wheat Seed Bag" /* . . . */ };
    public static string[] plantNames = new string[] { "Carrot", "Pumpkin" /* . . . */ };

    private Dictionary<string, (string, GameObject)> toolsDict;
    private Dictionary<string, GameObject> spritesDict;

    private int slotSelected;
    private int oldSlotSelected;

    private Transform canvas;

    [Header("Tool Objects")]
    [SerializeField] private GameObject ScytheObject;
    [SerializeField] private GameObject AxeObject;
    [SerializeField] private GameObject HoeObject;
    [SerializeField] private GameObject BagObject;

    [Header("Sprites")]
    [SerializeField] private GameObject ScytheSprite;
    [SerializeField] private GameObject AxeSprite;
    [SerializeField] private GameObject HoeSprite;
    [SerializeField] private GameObject BagSprite;

    [HideInInspector] private string currentTool = "null";
    [HideInInspector] private string currentPlant = "null";
    [HideInInspector] private GameObject grabbedTool;
    [HideInInspector] private GameObject grabbedPlant;
    [SerializeField] private Transform defaultToolPosition;
    [SerializeField] private Transform defaultPlantPosition;

    private void Update()
    {
        EquipItem();
        CheckRaycast();
        UseItem();
        DropItem();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        canvas = gameObject.transform.Find("Canvas");

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

    public void CheckRaycast()
    {
        Ray ray = new (Camera.main.transform.position, Camera.main.transform.forward);
        LayerMask Grabbables = 1 << 6;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, Grabbables))
            {
                GameObject hitObj = hit.collider.gameObject;
                if (ItemNames.itemNames.Contains(hitObj.name))
                {
                    if (hitObj.name != "Bag" && currentPlant != "null") return; // makes it so you cant grab a tool thats not a bag while holding a plant

                    grabbedTool = hitObj;

                    if (grabbedTool.name != "Wheat Seed Bag" && grabbedTool.name != "Carrot Seed Bag")
                    {
                        currentTool = grabbedTool.name;
                    }
                    else { currentTool = "Seed Bag"; }

                    grabbedTool.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving

                    if (currentTool == "Seed Bag" && grabbedTool.GetComponent<Animator>())
                    {
                        Destroy(grabbedTool.GetComponent<Animator>());
                        Destroy(grabbedTool.transform.Find("Item particle").gameObject);
                    }
                    AddItemToInventory();
                }

                if (currentPlant == "null" && ItemNames.plantNames.Contains(hitObj.name))
                {
                    grabbedPlant = hitObj;

                    grabbedPlant.transform.SetParent(null);
                    currentPlant = grabbedPlant.name;

                    grabbedPlant.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;

                    if (!grabbedPlant.transform.Find(currentPlant).gameObject.GetComponent<Animator>())
                    {
                        Animator plantAnimator = grabbedPlant.transform.Find(currentPlant).gameObject.AddComponent<Animator>();
                        plantAnimator.runtimeAnimatorController = controller;
                    }
                }
            }
            UpdateItemPositionAndRotation();
        }
    }

    public void UseItem()
    {
        Animator currentAnimator;
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbedTool != null && grabbedPlant == null)
            {
                currentAnimator = grabbedTool.transform.Find(grabbedTool.name).GetComponent<Animator>();
                if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
                {
                    currentAnimator.Play("UseItem");

                    if (currentTool == "Seed Bag")
                    {
                        SeedBagManager sbagManager = grabbedTool.GetComponent<SeedBagManager>();

                        if (sbagManager.timesUsed < sbagManager.maxTimesUsed)
                            grabbedTool.transform.Find(grabbedTool.name).Find("Seed Particles").GetComponent<ParticleSystem>().Play();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (grabbedPlant != null && currentTool == "Bag")
            {
                currentAnimator = grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>();
                if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState") && grabbedTool.GetComponent<BagInventory>().isBagOpen == true)
                {
                    currentAnimator.Play("UsePlant");
                    StartCoroutine(DepositPlant());
                }
            }
        }
    }

    private void UpdateItemPositionAndRotation()
    {
        // set rotations
        if (grabbedTool != null)
        {
            Transform toolTran = grabbedTool.transform;
            toolTran.parent = defaultToolPosition;
            toolTran.SetPositionAndRotation(defaultToolPosition.position, defaultToolPosition.rotation);
        }

        if (grabbedPlant != null)
        {
            Transform plantTran = grabbedPlant.transform;
            plantTran.parent = defaultPlantPosition;
            plantTran.SetPositionAndRotation(defaultPlantPosition.position, defaultPlantPosition.rotation);
        }
    }

    private void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // drop plant first if theres a plant and tool at the same time
            if (currentPlant != "null")
            {
                DropPlant();
            }

            else if (currentTool != "null")
            {
                DropTool();
            }
        }
    }

    private void DropPlant()
    {
        if (grabbedPlant.GetComponent<CapsuleCollider>())
        {
            grabbedPlant.GetComponent<CapsuleCollider>().enabled = true;
        }
        else if (grabbedPlant.GetComponent<SphereCollider>())
        {
            grabbedPlant.GetComponent<SphereCollider>().enabled = true;
        }

        currentPlant = "null";
        grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>().Play("DefaultState");
        Destroy(grabbedPlant.transform.Find(currentPlant).GetComponent<Animator>());

        StopCoroutine(DepositPlant());
        grabbedPlant.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        grabbedPlant.transform.parent = null;
        grabbedPlant = null;
    }

    public void DropTool()
    {
        if (grabbedTool.GetComponent<CapsuleCollider>())
        {
            grabbedTool.GetComponent<CapsuleCollider>().enabled = true;
        }
        else if (grabbedTool.GetComponent<SphereCollider>())
        {
            grabbedTool.GetComponent<SphereCollider>().enabled = true;
        }

        currentTool = "null";

        grabbedTool.transform.Find(grabbedTool.name).GetComponent<Animator>().Play("DefaultState");
        grabbedTool.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        grabbedTool.transform.parent = null;
        grabbedTool = null;

        RemoveItemFromSlot();
    }

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

    public void AddItemToInventory()
    {
        if (slotSelected != 0)
        {
            inventoryDict[inventoryDict.Count + 1] = (currentTool, grabbedTool);
            (string, GameObject) inventoryDictTuple = inventoryDict[inventoryDict.Count];
            inventoryDict[inventoryDict.Count] = toolsDict[inventoryDictTuple.Item1];
            Destroy(grabbedTool);

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
            newTool.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
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
    }

    public IEnumerator DepositPlant()
    {
        bagCollider.enabled = false;
        yield return new WaitForSeconds(0.98f);

        bagCollider.enabled = true;

        if (grabbedPlant == null) yield break;

        grabbedPlant.transform.parent = null;
        grabbedPlant = null;
        currentPlant = "null";
    }
}
