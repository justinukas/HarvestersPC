using UnityEngine;
using Main.Farming.SeedBags;
using Main.Bag;

namespace Main.ItemHandling
{
    public class ItemUse : MonoBehaviour
    {
        PlantDeposit PlantDeposit;
        private void Start()
        {
            PlantDeposit = GetComponent<PlantDeposit>();

        }
        public void UseItem(string currentTool, GameObject grabbedTool, ref GameObject grabbedPlant)
        {
            Animator currentAnimator;
            if (Input.GetMouseButtonDown(0))
            {
                if (grabbedTool != null && grabbedPlant == null)
                {
                    currentAnimator = grabbedTool.transform.Find(grabbedTool.name).GetComponent<Animator>();
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
                    {
                        currentAnimator.Play("UseItem");

                        if (currentTool == "Seed Bag" && grabbedTool.GetComponent<SeedBagManager>().timesUsed < grabbedTool.GetComponent<SeedBagManager>().maxTimesUsed)
                        {
                            grabbedTool.transform.Find(grabbedTool.name).Find("Seed Particles").GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (grabbedPlant != null && currentTool == "Bag")
                {
                    currentAnimator = grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>();
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState") && grabbedTool.GetComponent<BagInventory>().isBagOpen == true)
                    {
                        currentAnimator.Play("UsePlant");
                        StartCoroutine(PlantDeposit.DepositPlant());
                    }
                }
            }
        }
    }
}
