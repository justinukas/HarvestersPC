using System.Linq;
using UnityEngine;

namespace Main.Controls
{
    public class ItemRaycast : MonoBehaviour
    {
        public void CheckRaycast(ref string currentItem, ref GameObject grabbedObject)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            LayerMask Grabbables = 1 << 6;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, Grabbables) && currentItem == "null")
                {
                    grabbedObject = hit.collider.gameObject;

                    if (ItemNames.itemNames.Contains(grabbedObject.name))
                    {
                        currentItem = grabbedObject.name;

                        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving
                    }
                }
            }
        }
    }
}
