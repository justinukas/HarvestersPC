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
                Rigidbody.drag = 1;
                Rigidbody.angularDrag = 1;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (BagToPlayer.move == false)
            {
                Rigidbody.drag = 0;
                Rigidbody.angularDrag = 0;
            }
        }
    }
}

