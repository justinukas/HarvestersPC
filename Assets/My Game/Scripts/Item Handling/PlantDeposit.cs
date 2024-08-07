using System.Collections;
using UnityEngine;

namespace Main.ItemHandling
{
    public class PlantDeposit : MonoBehaviour
    {
        [SerializeField] MeshCollider bagCollider;
        ItemManager itemManager;
        private void Start()
        {
            itemManager = GetComponent<ItemManager>();
        }

        public IEnumerator DepositPlant()
        {
            bagCollider.enabled = false;
            yield return new WaitForSeconds(0.98f);

            bagCollider.enabled = true;

            if (itemManager.grabbedPlant == null) yield break;

            itemManager.grabbedPlant.transform.parent = null;
            itemManager.grabbedPlant = null;
            itemManager.currentPlant = "null";
        }
    }
}
