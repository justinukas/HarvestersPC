using UnityEngine;
using Main.Bag;

namespace Main.Economy
{
    public class Selling : MonoBehaviour
    {
        [SerializeField] BagInventory BagInventory;
        [SerializeField] MoneyCounter MoneyCounter;
        [SerializeField] BagToPlayer BagToPlayer;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Closed Bag") || collider.gameObject.CompareTag("Open Bag"))
            {

                if (collider.gameObject.CompareTag("Closed Bag"))
                {
                    MoneyCounter.moneyNr += BagInventory.value;

                    BagInventory.ResetCounters();
                    BagToPlayer.StartMoving();
                    Leave();
                }

                if (collider.gameObject.CompareTag("Open Bag"))
                {
                    BagToPlayer.StartMoving();
                }
            }
        }

        private void Leave()
        {
            Animator boatAnimator = gameObject.transform.parent.GetComponent<Animator>();
            boatAnimator.SetTrigger("triggerLeave");
        }

        private void Update()
        {
            if (BagToPlayer.move == true)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else if (BagToPlayer.move == false)
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
