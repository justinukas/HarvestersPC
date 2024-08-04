using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemDrop : MonoBehaviour
    {
        public void DropItem(ref string currentItem, ref GameObject grabbedObject, ref string currentPlant, ref GameObject grabbedPlant)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (currentItem != "null")
                {
                    grabbedObject.transform.Find(grabbedObject.name).GetComponent<Animator>().Play("DefaultState");
                    
                    grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    currentItem = "null";
                    grabbedObject = null;

                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (currentPlant != "null")
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
