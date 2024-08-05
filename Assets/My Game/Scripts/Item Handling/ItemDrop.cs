using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemDrop : MonoBehaviour
    {
        public void DropItem(ref string currentItem, ref GameObject grabbedObject, ref string currentPlant, ref GameObject grabbedPlant)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // if an item and a plant are equipped at the same time, drop the plant first.
                if (currentItem != "null" && currentPlant != "null")
                {
                    grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>().Play("DefaultState");
                    Destroy(grabbedPlant.transform.Find(currentPlant).GetComponent<Animator>());

                    grabbedPlant.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    currentPlant = "null";
                    grabbedPlant = null;
                }

                else if (currentItem != "null")
                {
                    grabbedObject.transform.Find(grabbedObject.name).GetComponent<Animator>().Play("DefaultState");

                    grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    currentItem = "null";
                    grabbedObject = null;
                }

                else if (currentPlant != "null")
                {
                    grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>().Play("DefaultState");
                    Destroy(grabbedPlant.transform.Find(currentPlant).GetComponent<Animator>());

                    grabbedPlant.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    currentPlant = "null";
                    grabbedPlant = null;
                }
            }
        }
    }
}
