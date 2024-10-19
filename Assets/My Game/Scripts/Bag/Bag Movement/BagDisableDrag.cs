using UnityEngine;

namespace Main.Bag
{
    public class BagDisableDrag : MonoBehaviour
    {
        [SerializeField] BagToPlayer BagToPlayer;
        private Rigidbody Rigidbody;

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name != "Selling Hitbox")
            {
                Rigidbody.linearDamping = 1;
                Rigidbody.angularDamping = 1;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (BagToPlayer.move == false)
            {
                Rigidbody.linearDamping = 0;
                Rigidbody.angularDamping = 0;
            }
        }
    }
}

