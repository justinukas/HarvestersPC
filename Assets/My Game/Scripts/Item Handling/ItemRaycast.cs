using System.Linq;
using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemRaycast : MonoBehaviour
    {
        public void CheckRaycast(ref string currentItem, ref GameObject grabbedObject)
        {
            if (currentItem != null) Debug.Log(currentItem);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            LayerMask Grabbables = 1 << 6;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, Grabbables) && currentItem == "null")
                {
                    grabbedObject = hit.collider.gameObject;

                    if (ItemNames.itemNames.Contains(grabbedObject.name))
                    {
                        switch (grabbedObject.name)
                        {
                            case "Scythe":
                            case "Hoe":
                            case "Axe":
                            case "Bag":
                                currentItem = grabbedObject.name;
                                break;
                            case "Wheat Seed Bag":
                            case "Carrot Seed Bag":
                                currentItem = "Seed Bag";
                                break;
                        }

                        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving
                        
                        if (currentItem == "Seed Bag" && grabbedObject.GetComponent<Animator>())
                        {
                            Destroy(grabbedObject.GetComponent<Animator>());
                            Destroy(grabbedObject.transform.Find("Item particle").gameObject);
                        }
                    }
                }
            }
        }
    }
}
