using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemUse : MonoBehaviour
    {

        public void UseItem(ref string currentItem, ref GameObject grabbedObject, ref bool isSwinging)
        {
            if (currentItem == "null" || grabbedObject == null)
                return;

            Animator currentAnimator = grabbedObject.transform.Find(grabbedObject.name).GetComponent<Animator>();
            if (Input.GetMouseButtonDown(0))
            {
                if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
                {
                    currentAnimator.Play("UseItem");

                    if (grabbedObject.transform.Find(grabbedObject.name).Find("Seed Particles"))
                    {
                        grabbedObject.transform.Find(grabbedObject.name).Find("Seed Particles").GetComponent<ParticleSystem>().Play();
                    }
                }
            }

            if (currentItem == "Scythe" || currentItem == "Axe" || currentItem == "Hoe")
            { isSwinging = currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"Swing {currentItem}"); }
        }
    }
}
