using System.Collections.Generic;
using UnityEngine;

namespace Main.Farming.SeedBags
{
    public class PlantInfoHandler : MonoBehaviour
    {
        public Dictionary<string, (List<Vector3>, Transform, GameObject)> plantInfo;

        public void InitializePlantInfo(ref string bagVariant, GameObject tilledDirt, GameObject Wheat, GameObject Carrot)
        {
            List<Vector3> wheatPositionsList = PlantPositions.wheatPositionsList;
            List<Vector3> carrotPositionsList = PlantPositions.carrotPositionsList;

            Transform WheatParent = tilledDirt.transform.Find("WheatParent");
            Transform CarrotParent = tilledDirt.transform.Find("CarrotParent");

            // gets the "Carrot" or "Wheat" part from the gameobject name and assigns it to bagVariant
            string[] _string = gameObject.name.Split(' ');
            bagVariant = _string[0];

            plantInfo = new Dictionary<string, (List<Vector3>, Transform, GameObject)>
            {
                {"Wheat", (wheatPositionsList, WheatParent, Wheat)},
                {"Carrot", (carrotPositionsList, CarrotParent, Carrot)}
            };
        }
    }
}
