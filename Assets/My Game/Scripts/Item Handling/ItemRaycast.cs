using System.Linq;
using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemRaycast : MonoBehaviour
    {
        public void CheckRaycast(ref string currentItem, ref GameObject grabbedTool, ref string currentPlant, ref GameObject grabbedPlant)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            LayerMask Grabbables = 1 << 6;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, Grabbables))
                {
                    if (currentItem == "null")
                    {
                        if (ItemNames.itemNames.Contains(hit.collider.gameObject.name))
                        {
                            grabbedTool = hit.collider.gameObject;

                            if (grabbedTool.name != "Wheat Seed Bag" || grabbedTool.name != "Carrot Seed Bag")
                            {
                                currentItem = grabbedTool.name;
                            }
                            else { currentItem = "Seed Bag"; }

                            grabbedTool.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving

                            if (currentItem == "Seed Bag" && grabbedTool.GetComponent<Animator>())
                            {
                                Destroy(grabbedTool.GetComponent<Animator>());
                                Destroy(grabbedTool.transform.Find("Item particle").gameObject);
                            }
                        }
                    }

                    if (currentPlant == "null")
                    {
                        if (ItemNames.plantNames.Contains(hit.collider.gameObject.name))
                        {
                            grabbedPlant = hit.collider.gameObject;

                            grabbedPlant.transform.SetParent(null);
                            currentPlant = grabbedPlant.name;

                            grabbedPlant.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                        }
                    }
                }
            }
        }
    }
}
