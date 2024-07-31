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
            if (collider.gameObject.name == "Bag")
            {
                if (BagInventory.weight > 0)
                {
                    MoneyCounter.moneyNr += BagInventory.value;
                    MoneyCounter.UpdateMoneyCount();

                    BagInventory.ResetAllCounters();
                    BagToPlayer.StartMoving();
                    BoatLeave();
                }
            }
        }

        private void BoatLeave()
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
