using UnityEngine;

namespace Main.Farming
{
    public class SeedBagBuyingHandler : MonoBehaviour
    {
        private SeedBagColorHandler colorHandler;

        public void BuyBag(ref int timesUsed)
        {
            timesUsed = 0;
            colorHandler = GetComponent<SeedBagColorHandler>();
            colorHandler.ResetColors();
        }
    }
}
