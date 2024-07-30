using Main.SeedBags;
using UnityEngine;

namespace Main.Economy
{
    public class BuyScript : MonoBehaviour
    {
        // get the money stuff
        [SerializeField] private MoneyCounter MoneyCounter;

        // float numbers for calculating how long the object has been in an area
        private readonly float minStayingLength = 4f;
        private float timeOnEnter;
        private int price;

        void OnTriggerEnter(Collider collider)
        {
            timeOnEnter = Time.time;
        }

        void OnTriggerStay(Collider collider)
        {
            if (collider.gameObject.transform.parent.GetComponent<SeedBag>())
            {
                SeedBag SeedBag = collider.gameObject.transform.parent.GetComponent<SeedBag>();
                string BagVariant = SeedBag.BagVariant;

                switch (BagVariant)
                {
                    case "Carrot":
                        price = 10;
                        break;
                    case "Wheat":
                        price = 15;
                        break;
                }

                if (SeedBag.timesUsed >= 10 && Time.time - timeOnEnter >= minStayingLength && MoneyCounter.moneyNr >= 10)
                {
                    MoneyCounter.moneyNr -= price;
                    MoneyCounter.UpdateMoneyCount();

                    SeedBag.Bought();
                }
            }

        }
    }
}
