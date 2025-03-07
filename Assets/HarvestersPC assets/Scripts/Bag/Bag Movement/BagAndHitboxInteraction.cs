using UnityEngine;

namespace Main.Bag
{
    public class BagAndHitboxInteraction : MonoBehaviour
    {
        [SerializeField] private BagToPlayer BagToPlayer;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.name == "Bag")
            {
                BagToPlayer.StartMoving();
                DisableHitbox();
            }
        }

        private void DisableHitbox()
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }

        public void EnableHitbox()
        {
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }
}
