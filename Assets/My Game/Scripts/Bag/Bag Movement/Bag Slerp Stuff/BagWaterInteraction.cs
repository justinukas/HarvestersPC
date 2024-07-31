using UnityEngine;

namespace Main.Bag
{
    public class BagWaterInteraction : MonoBehaviour
    {
        [SerializeField] private BagToPlayer BagToPlayer;
        private bool collided;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.name == "Open Bag")
            {
                BagToPlayer.StartMoving();
                collided = true;
            }
        }

        // hides the water hitbox so the bag doesnt buggout
        private void Update()
        {
            if (collided)
            {
                if (BagToPlayer.move == true)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }

                if (BagToPlayer.move == false)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    collided = false;
                }
            }
        }
    }
}
