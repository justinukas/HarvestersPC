using System.Linq;
using UnityEngine;
using Main.Farming;

namespace Main.ItemHandling
{
    public class ItemRaycast : MonoBehaviour
    {
        [SerializeField] private RuntimeAnimatorController controller;
        private ItemManager itemManager;

        private void Start()
        {
            itemManager = GetComponent<ItemManager>();
        }

        public void CheckRaycast(ref string currentTool, ref GameObject grabbedTool, ref string currentPlant, ref GameObject grabbedPlant)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            LayerMask Grabbables = 1 << 6;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, Grabbables))
                {
                    if (currentTool == "null")
                    {
                        if (ItemNames.itemNames.Contains(hit.collider.gameObject.name))
                        {
                            if (hit.collider.gameObject.name != "Bag" && currentPlant != "null") return; // makes it so you cant grab a tool thats not a bag while holding a plant

                            grabbedTool = hit.collider.gameObject;

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
                        }
                    }

                    if (currentPlant == "null")
                    {
                        if (ItemNames.plantNames.Contains(hit.collider.gameObject.name) && hit.collider.gameObject.GetComponent<Harvestability>().isHarvestable == true)
                        {
                            grabbedPlant = hit.collider.gameObject;

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
                }
                itemManager.SetParents();
            }
        }
    }
}
