using UnityEngine;

public class BuyScript : MonoBehaviour
{
    // get the money stuff
    [SerializeField] private MoneyCounter moneyCounter;

    // float numbers for calculating how long the object has been in an area
    private float minStayingLength = 4f;
    private float timeOnEnter;

    void OnTriggerEnter(Collider collider)
    {
        timeOnEnter = Time.time;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("CarrotBag"))
        {
            // script reference
            CarrotSeed CarrotSeed = collider.gameObject.transform.parent.GetComponent<CarrotSeed>();

            if (CarrotSeed.timesUsed >= 10 && Time.time - timeOnEnter >= minStayingLength && moneyCounter.moneyNr >= 10)
            {
                moneyCounter.moneyNr -= 10;

                CarrotSeed.Bought();
            }
        }

        if (collider.gameObject.CompareTag("WheatBag"))
        {
            // script reference
            WheatSeed WheatSeed = collider.gameObject.transform.parent.GetComponent<WheatSeed>();

            if (WheatSeed.timesUsed >= 10 && Time.time - timeOnEnter >= minStayingLength && moneyCounter.moneyNr >= 15)
            {
                moneyCounter.moneyNr -= 15;
                WheatSeed.Bought();
            }
        }
    }
}
