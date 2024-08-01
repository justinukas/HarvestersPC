using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemUse : MonoBehaviour
    {
        Animator currentAnimator;

        public void UseItem(ref string currentItem, ref GameObject grabbedObject, ref bool isSwinging)
        {
            if (currentItem == "null" || grabbedObject == null)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (currentItem == "Scythe" || currentItem == "Axe" || currentItem == "Hoe")
                {
                    currentAnimator = grabbedObject.transform.Find(currentItem).GetComponent<Animator>();
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"{currentItem} Swing") == false)
                    {
                        currentAnimator.Play($"{currentItem} Swing");
                    }
                }

                else if (currentItem == "Bag")
                {
                    currentAnimator = grabbedObject.transform.Find(currentItem).GetComponent<Animator>();
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Charge Up Bag Throw") == false && GameObject.Find("Throwing Charge Bar").transform.Find("Canvas").GetComponent<CanvasGroup>().alpha == 1)
                    {
                        currentAnimator.Play("Charge Up Bag Throw");
                    }
                }

                else if (currentItem == "Wheat Seed Bag" || currentItem == "Carrot Seed Bag")
                {
                    currentAnimator = grabbedObject.transform.Find("Seed Bag").GetComponent<Animator>();
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Plant Seeds") == false && currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Plant Seeds_Return") == false)
                    {
                        currentAnimator.Play("Plant Seeds");
                        grabbedObject.transform.Find("Seed Bag").Find("Seed Particles").GetComponent<ParticleSystem>().Play();
                    }
                }
            }

            if (currentItem == "Scythe" || currentItem == "Axe" || currentItem == "Hoe")
            { isSwinging = currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"{currentItem} Swing"); }
        }
    }
}
