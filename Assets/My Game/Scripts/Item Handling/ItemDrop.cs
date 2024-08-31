using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemDrop : MonoBehaviour
    {
        PlantDeposit plantDeposit;
        [SerializeField] Inventory inventory;
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
                    DropPlant(ref currentPlant, ref grabbedPlant);
                }

                else if (currentTool != "null")
                {
                    DropTool(ref currentTool, ref grabbedTool);
                }

                else if (currentPlant != "null")
                {
                    DropPlant(ref currentPlant, ref grabbedPlant);
                }
            }
        }

        private void DropPlant(ref string currentPlant, ref GameObject grabbedPlant)
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

            StopCoroutine(plantDeposit.DepositPlant());
            grabbedPlant.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            grabbedPlant.transform.parent = null;
            grabbedPlant = null;
        }

        public void DropTool(ref string currentTool, ref GameObject grabbedTool)
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
            
            inventory.RemoveItemFromSlot();
        }
    }
}
