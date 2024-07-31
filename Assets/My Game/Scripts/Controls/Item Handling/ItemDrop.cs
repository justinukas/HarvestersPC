using UnityEngine;

namespace Main.Controls
{
    public class ItemDrop : MonoBehaviour
    {
        public void DropItem(ref string currentItem, ref bool isSwinging, ref GameObject grabbedObject)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (currentItem != "null")
                {
                    grabbedObject.transform.Find(currentItem).GetComponent<Animator>().Play("DefaultState");
                    grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    isSwinging = false;
                    currentItem = "null";
                    grabbedObject = null;
                }
            }
        }
    }
}
