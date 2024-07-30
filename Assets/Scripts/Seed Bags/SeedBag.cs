using UnityEngine;
using System.Collections.Generic;
using Main.Farming;

namespace Main.SeedBags
{
    public class SeedBag : MonoBehaviour
    {
        // References to plant prefabs
        [SerializeField] private GameObject Wheat;
        [SerializeField] private GameObject Carrot;
        [SerializeField] private ParticleSystem PlantParticles;
        [SerializeField] private ParticleSystem SeedParticles;
        [HideInInspector] public int timesUsed = 0; // measurement for bag uses
        [HideInInspector] public string BagVariant;

        // bag color stuff
        private float r; // R value for bag color RGB
        private readonly float rDefault = 0.05490192f;
        private readonly float rFinal = 0.509804f;

        // stuff for determining plant position
        private GameObject tilledDirt;

        // lists for plant positions
        private readonly List<Vector3> wheatPositionsList = new();
        private readonly List<Vector3> carrotPositionsList = new();

        // references for plant parent transforms
        private Transform WheatParent;
        private Transform CarrotParent;

        private Dictionary<string, (List<Vector3>, Transform, GameObject)> plantInfo;

        private void Start()
        {
            // renders bag unusable on game start
            //timesUsed = 50; 

            r = rFinal;

            if (gameObject.name == "Carrot Seed Bag") 
                BagVariant = "Carrot";
            else if (gameObject.name == "Wheat Seed Bag") 
                BagVariant = "Wheat";

            Debug.Log(BagVariant);

            ChangeColor();
        }

        
        private void InitializePlantInfo()
        {
            plantInfo = new Dictionary<string, (List<Vector3>, Transform, GameObject)>
            {
                {"Wheat", (wheatPositionsList, WheatParent, Wheat)},
                {"Carrot", (carrotPositionsList, CarrotParent, Carrot)}
            };
        }

        private void OnCollisionEnter(Collision collider)
        {
            if (collider.gameObject.CompareTag("TilledDirt"))
            {
                tilledDirt = collider.gameObject;

                WheatParent = tilledDirt.transform.Find("WheatParent");
                CarrotParent = tilledDirt.transform.Find("CarrotParent");

                PlantingEnabler PlantingEnabler = collider.gameObject.GetComponent<PlantingEnabler>();

                if (PlantingEnabler.plantingAllowed == true && timesUsed <= 50)
                {
                    timesUsed++;

                    InitializePlantInfo();

                    PlantPositionListing();
                    SpawnPlants();
                    PlantingParticlesEmit();
                    PlayPlantingSFX();
                    FadeBagColor();
                }
            }
        }

        private void PlantPositionListing()
        {
            if (BagVariant == "Wheat")
            {
                float initialWheatOffsetX = 0.4f;
                float initialWheatOffsetZ = 0.4f;

                float offset = 0.2f;

                float currentWheatOffsetX = initialWheatOffsetX;
                float currentWheatOffsetZ = initialWheatOffsetZ;

                for (int rows = 1; rows <= 5; rows++)
                {
                    for (int columns = 1; columns <= 5; columns++)
                    {
                        wheatPositionsList.Add(new Vector3(currentWheatOffsetX, 0.30f, currentWheatOffsetZ));
                        currentWheatOffsetZ -= offset;
                    }
                    currentWheatOffsetX -= offset;
                    currentWheatOffsetZ = initialWheatOffsetZ;
                }
            }

            else if (BagVariant == "Carrot")
            {
                Vector3[] carrotOffsets = { 
                    new Vector3(-0.4f, 0.2f, -0.382f)/*bottom right*/, 
                    new Vector3(0f, 0.2f, -0.382f)/*middle right*/, 
                    new Vector3(0f, 0.2f, 0f)/*middle*/, 
                    new Vector3(0f, 0.2f, 0.3935f)/*middle left*/,
                    new Vector3(0.4f, 0.2f, -0.382f), /*top right*/ 
                    new Vector3(-0.4f, 0.2f, 0.3935f)/*bottom left*/, 
                    new Vector3(0.4f, 0.2f, 0.3935f)/*top right*/, 
                    new Vector3(0.4f, 0.2f, 0f)/*middle top*/,
                    new Vector3(-0.4f, 0.2f, 0f) /*middle bottom*/};
                carrotPositionsList.AddRange(carrotOffsets);
            }
            
        }

        // Instantiate plants at fixed locations on the top surface of the dirt
        private void SpawnPlants()
        {
            if (plantInfo.TryGetValue(BagVariant, out var info))
            {
                List<Vector3> positions = info.Item1;
                Transform parent = info.Item2;
                GameObject plant = info.Item3;

                foreach (Vector3 spawnOffset in positions)
                {
                    GameObject plantClone = Instantiate(plant, spawnOffset + tilledDirt.transform.position, Quaternion.identity);
                    plantClone.transform.SetParent(parent);
                    plantClone.name = plant.name;
                }
            }
        }

        // emits particles when the wheat is planted
        private void PlantingParticlesEmit()
        {
            ParticleSystem PlantParticlesCopy = Instantiate(PlantParticles, tilledDirt.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

            PlantParticlesCopy.Emit(10);
            PlantParticlesCopy.Stop();
        }

        //play planting sfx
        private void PlayPlantingSFX()
        {
            GetComponent<AudioSource>().Play();
        }

        // fades color of bag to indicate uses left
        private void FadeBagColor()
        {
            r += 0.01019608f;
            ChangeColor();
        }

        private void ChangeColor()
        {
            // changes color of only the first assigned material
            var bagMaterials = gameObject.transform.Find("Seed Bag").GetComponent<Renderer>().materials;
            bagMaterials[0].color = new Color(r, 0.396f, 0.0666f);
        }

        public void Bought()
        {
            timesUsed = 0;
            r = rDefault;
        }
    }
}
