using UnityEngine;

namespace Main.Farming.SeedBags
{
    public class BuyingHandler : MonoBehaviour
    {
        private ColorHandler colorHandler;

        public void BuyBag(ref int timesUsed)
        {
            timesUsed = 0;
            colorHandler = GetComponent<ColorHandler>();
            colorHandler.ResetColors();
        }
    }
}
