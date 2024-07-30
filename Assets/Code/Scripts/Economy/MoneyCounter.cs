using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    public float moneyNr;
    private string moneyText;

    public void UpdateMoneyCount()
    {
        // converts int to string
        moneyText = moneyNr.ToString();

        // writes text on money ui
        gameObject.GetComponent<Text>().text = moneyText;
    }
}
