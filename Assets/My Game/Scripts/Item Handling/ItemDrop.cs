using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemDrop : MonoBehaviour
    {
        PlantDeposit plantDeposit;
        private void Start()
        {
            plantDeposit = GetComponent<PlantDeposit>();
        }
        public void DropItem(ref string currentTool, ref GameObject grabbedTool, ref string currentPlant, ref GameObject grabbedPlant)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // if an tool and a plant are equipped at the same time, drop the plant first.
                if (currentTool != "null" && currentPlant != "null")
                {
                    grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>().Play("DefaultState");
                    Destroy(grabbedPlant.transform.Find(currentPlant).GetComponent<Animator>());

                    StopCoroutine(plantDeposit.DepositPlant());
                    grabbedPlant.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    grabbedPlant.transform.parent = null;
                    currentPlant = "null";
                    grabbedPlant = null;
                }

                else if (currentTool != "null")
                {
                    grabbedTool.transform.Find(grabbedTool.name).GetComponent<Animator>().Play("DefaultState");

                    grabbedTool.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    grabbedTool.transform.parent = null;
                    currentTool = "null";
                    grabbedTool = null;
                }

                else if (currentPlant != "null")
                {
                    grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>().Play("DefaultState");
                    Destroy(grabbedPlant.transform.Find(currentPlant).GetComponent<Animator>());

                    StopCoroutine(plantDeposit.DepositPlant());
                    grabbedPlant.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    grabbedPlant.transform.parent = null;
                    currentPlant = "null";
                    grabbedPlant = null;
                }
            }
        }
    }
}
