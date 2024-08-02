using UnityEngine;

namespace Main.Farming.SeedBags
{
    public class RaycastHandler : MonoBehaviour
    {
        private Animator animator;
        private SeedBagManager seedBagManager;

        private void Start()
        {
            animator = transform.Find(gameObject.name).GetComponent<Animator>();
            seedBagManager = GetComponent<SeedBagManager>();
        }

        public void CheckRaycast(ref int timesUsed, ref GameObject tilledDirt)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Plant Seeds") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                LayerMask TilledDirt = 1 << 9;

                if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, TilledDirt))
                {
                    tilledDirt = hit.collider.gameObject;
                    PlantingEnabler plantingEnabler = tilledDirt.GetComponent<PlantingEnabler>();

                    if (plantingEnabler.plantingAllowed && timesUsed <= 50)
                    {
                        timesUsed++;
                        seedBagManager.InitializePlanting();
                        plantingEnabler.plantingAllowed = false;
                    }
                }
            }
        }
    }
}
