using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemDrop : MonoBehaviour
    {
        public void DropItem(ref string currentItem, ref bool isSwinging, ref GameObject grabbedObject)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (currentItem != "null")
                {
                    if (currentItem == "Scythe" || currentItem == "Axe" || currentItem == "Hoe" || currentItem == "Bag")
                    {
                        grabbedObject.transform.Find(currentItem).GetComponent<Animator>().Play("DefaultState");
                    }
                    else if (currentItem == "Wheat Seed Bag" || currentItem == "Carrot Seed Bag")
                    {
                        grabbedObject.transform.Find("Seed Bag").GetComponent<Animator>().Play("DefaultState");
                    }
                    
                    grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    isSwinging = false;
                    currentItem = "null";
                    grabbedObject = null;

                }
            }
        }
    }
}
