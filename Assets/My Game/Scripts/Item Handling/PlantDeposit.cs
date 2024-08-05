using System.Collections;
using UnityEngine;

namespace Main.ItemHandling
{
    public class PlantDeposit : MonoBehaviour
    {
        ItemManager itemManager;
        private void Start()
        {
            itemManager = GetComponent<ItemManager>();
        }

        public IEnumerator DepositPlant()
        {
            yield return new WaitForSeconds(0.98f);
            itemManager.grabbedPlant.transform.position = itemManager.defaultToolPosition.position;
            
            itemManager.grabbedPlant = null;
            itemManager.currentPlant = "null";
        }
    }
}
