using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemUse : MonoBehaviour
    {

        public void UseItem(string currentItem, GameObject grabbedObject, GameObject grabbedPlant)
        {
            if (currentItem == "null" || grabbedObject == null) return;

            Animator currentAnimator;
            if (Input.GetMouseButtonDown(0))
            {
                currentAnimator = grabbedObject.transform.Find(grabbedObject.name).GetComponent<Animator>();
                if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
                {
                    currentAnimator.Play("UseItem");

                    if (grabbedObject.transform.Find(grabbedObject.name).Find("Seed Particles"))
                    {
                        grabbedObject.transform.Find(grabbedObject.name).Find("Seed Particles").GetComponent<ParticleSystem>().Play();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                currentAnimator = grabbedPlant.transform.Find(grabbedPlant.name).GetComponent<Animator>();
                if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
                {
                    currentAnimator.Play("DepositPlant");
                }
            }
        }
    }
}
