using UnityEngine;

namespace Main.Controls
{
    public class ItemUse : MonoBehaviour
    {
        public void UseItem(ref string currentItem, ref GameObject grabbedObject, ref bool isSwinging)
        {
            if (currentItem == "null" || grabbedObject == null)
                return;

            Animator currentAnimator = grabbedObject.GetComponent<Animator>();

            if (Input.GetMouseButtonDown(0))
            {
                currentAnimator = grabbedObject.transform.Find(currentItem).GetComponent<Animator>();

                if (currentItem == "Scythe" || currentItem == "Axe" || currentItem == "Hoe")
                {
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"{currentItem} Swing") == false)
                    {
                        currentAnimator.Play($"{currentItem} Swing");
                    }
                }

                else if (currentItem == "Bag")
                {
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Charge Up Bag Throw") == false && GameObject.Find("Throwing Charge Bar").transform.Find("Canvas").GetComponent<CanvasGroup>().alpha == 1)
                    {
                        currentAnimator.Play("Charge Up Bag Throw");
                    }
                }

                else if (currentItem == "Wheat Seed Bag" || currentItem == "Carrot Seed Bag")
                {
                    if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Plant Seeds") == false && currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Plant Seeds_Return") == false)
                    {
                        currentAnimator.Play("Plant Seeds");
                        grabbedObject.transform.Find(currentItem).Find("Seed Particles").GetComponent<ParticleSystem>().Play();
                        //grabbedObject.GetComponent<SeedBag>().PlantingRaycast();
                    }
                }
            }

            if (currentItem == "Scythe" || currentItem == "Axe" || currentItem == "Hoe")
            { isSwinging = currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"{currentItem} Swing"); }
        }
    }
}
