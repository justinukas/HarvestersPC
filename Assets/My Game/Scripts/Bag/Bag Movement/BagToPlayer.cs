using UnityEngine;

namespace Main.Bag
{
    public class BagToPlayer : MonoBehaviour
    {
        [HideInInspector] public bool move;

        [SerializeField] private Transform player;
        private Transform bag;
        private Rigidbody bagRigidbody;

        private BagAndHitboxInteraction[] BagAndHitboxInteractions;

        private readonly float travelTime = 10f;
        private readonly float speed = 0.25f; // must be a value from 0f to 1f
        private float startTime;

        private void Start()
        {
            bagRigidbody = GetComponent<Rigidbody>();
            BagAndHitboxInteractions = FindObjectsOfType<BagAndHitboxInteraction>();
        }

        public void StartMoving()
        {
            move = true;
            startTime = Time.time;
            bag = transform; 
        }

        void Update()
        {
            if (!move) return;

            Vector3 center = (bag.position + player.position) / 2f - new Vector3(0, 1, 0);
            Vector3 bagCenter = bag.position - center;
            Vector3 playerCenter = player.position + (player.forward * 0.2f) - center;

            float fracComplete = (Time.time - startTime) / travelTime;

            bag.position = Vector3.Slerp(bagCenter, playerCenter, fracComplete * speed) + center;

            float distanceToPlayer = Vector3.Distance(bag.position, player.position);

            /*if (distanceToPlayer < 5)
            {
                bagRigidbody.angularDrag = 10;
                bagRigidbody.drag = 10;
            }*/

            if (distanceToPlayer < 0.5)
            {
                move = false;
                foreach (BagAndHitboxInteraction bagAndHitboxInteraction in BagAndHitboxInteractions)
                {
                    bagAndHitboxInteraction.EnableHitbox();
                }
            }
        }
    }
}
