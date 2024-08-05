using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemUse : MonoBehaviour
    {
        PlantDeposit PlantDeposit;
        private void Start()
        {
            PlantDeposit = GetComponent<PlantDeposit>();
        }
        public void UseItem(string currentItem, GameObject grabbedObject, ref string currentPlant, ref GameObject grabbedPlant)
        {
            Animator currentAnimator;
            if (Input.GetMouseButtonDown(0))
            {
                if (grabbedObject != null)
                {
                    currentAnimator = grabbedObject.transform.Find(grabbedObject.name).GetComponent<Animator>();
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
                    {
                        currentAnimator.Play("UseItem");

                        if (currentItem == "Seed Bag")
                        {
                            grabbedObject.transform.Find(grabbedObject.name).Find("Seed Particles").GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (grabbedPlant != null && currentItem == "Bag")
                {
                    currentAnimator = grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>();
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
                    {
                        currentAnimator.Play("UsePlant");
                        StartCoroutine(PlantDeposit.DepositPlant());
                    }
                }
            }
        }
    }
}
