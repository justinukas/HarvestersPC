using System.Collections.Generic;
using UnityEngine;

namespace Main.Farming
{
    public class PlantInfoHandler : MonoBehaviour
    {
        private List<Vector3> wheatPositionsList = PlantPositions.wheatPositionsList;
        private List<Vector3> carrotPositionsList = PlantPositions.carrotPositionsList;

        [SerializeField] private GameObject Wheat;
        [SerializeField] private GameObject Carrot;

        // references for plant parent transforms
        private Transform WheatParent;
        private Transform CarrotParent;

        public Dictionary<string, (List<Vector3>, Transform, GameObject)> plantInfo;

        public void InitializePlantInfo(ref string bagVariant, GameObject tilledDirt)
        {
            WheatParent = tilledDirt.transform.Find("WheatParent");
            CarrotParent = tilledDirt.transform.Find("CarrotParent");

            switch (gameObject.name)
            {
                case "Carrot Seed Bag":
                    bagVariant = "Carrot";
                    break;
                case "Wheat Seed Bag":
                    bagVariant = "Wheat";
                    break;
            }

            plantInfo = new Dictionary<string, (List<Vector3>, Transform, GameObject)>
            {
                {"Wheat", (wheatPositionsList, WheatParent, Wheat)},
                {"Carrot", (carrotPositionsList, CarrotParent, Carrot)}
            };
        }
    }
}
