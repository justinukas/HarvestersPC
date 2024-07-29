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
        private Vector3 dirtSurface;

        // lists for plant positions
        private List<Vector3> wheatPositionsList = new();
        private List<Vector3> carrotPositionsList = new();

        // references for plant parent transforms
        private Transform WheatParent;
        private Transform CarrotParent;

        private readonly Dictionary<string, (List<Vector3>, Transform, GameObject)> plantInfo;

        private void Start()
        {
            // renders bag unusable on game start
            timesUsed = 50; 

            r = rFinal;

            if (gameObject.name == "Carrot Seed Bag") 
                BagVariant = "Carrot";
            else if (gameObject.name == "Wheat Seed Bag") 
                BagVariant = "Wheat";

            
            ChangeColor();
        }

        private void InitializePlantInfo()
        {
            plantInfo["Wheat"] = (wheatPositionsList, WheatParent, Wheat);
            plantInfo["Carrot"] = (carrotPositionsList, CarrotParent, Carrot);
        }

        private void OnCollisionEnter(Collision collider)
        {
            if (collider.gameObject.CompareTag("TilledDirt"))
            {
                tilledDirt = collider.gameObject;

                WheatParent = tilledDirt.transform.Find("WheatParent");
                CarrotParent = tilledDirt.transform.Find("CarrotParent");

                InitializePlantInfo();

                PlantingEnabler PlantingEnabler = collider.gameObject.GetComponent<PlantingEnabler>();

                if (PlantingEnabler.plantingAllowed == true && timesUsed <= 50)
                {
                    timesUsed++;

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
                float currentWheatOffsetX = 0.4f;
                float currentWheatOffsetZ = 0.45f;

                float offset = 0.2f;
                for (int rows = 1; rows <= 5; rows++)
                {
                    for (int columns = 1; columns <= 4; columns++)
                    {
                        wheatPositionsList.Add(new Vector3(currentWheatOffsetX, 0.3f, currentWheatOffsetZ));
                        currentWheatOffsetZ -= offset;
                    }
                    wheatPositionsList.Add(new Vector3(currentWheatOffsetX, 0.3f, currentWheatOffsetZ));
                    currentWheatOffsetX -= offset;
                    currentWheatOffsetZ = 0.45f;
                }
            }

            else if (BagVariant == "Carrot")
            {
                Vector3[] carrotOffsets = { 
                    new Vector3(-0.4f, 0.07f, -0.4f)/*bottom right*/, 
                    new Vector3(0f, 0.07f, -0.4f)/*middle right*/, 
                    new Vector3(0f, 0.07f, -0.05f)/*middle*/, 
                    new Vector3(0f, 0.07f, 0.3f)/*middle left*/,
                    new Vector3(0.4f, 0.07f, -0.4f), /*top right*/ 
                    new Vector3(-0.4f, 0.07f, 0.3f)/*bottom left*/, 
                    new Vector3(0.4f, 0.07f, 0.3f)/*top right*/, 
                    new Vector3(0.4f, 0.07f, -0.05f)/*middle top*/,
                    new Vector3(-0.4f, 0.07f, -0.05f) /*middle bottom*/};
                carrotPositionsList.AddRange(carrotOffsets);
            }
        }

        // Instantiate plants at fixed locations on the top surface of the dirt
        private void SpawnPlants()
        {
            Vector3 dirtPosition = tilledDirt.transform.position;
            // Get the top surface position of the dirt
            dirtSurface = dirtPosition + Vector3.up * dirtPosition.y / 2f;

            if (plantInfo.TryGetValue(BagVariant, out var info))
            {
                List<Vector3> positions = info.Item1;
                Transform parent = info.Item2;
                GameObject plant = info.Item3;

                foreach (Vector3 spawnOffset in positions)
                {
                    Vector3 finalSpawnPosition = dirtSurface + spawnOffset;
                    GameObject plantClone = Instantiate(plant, finalSpawnPosition, Quaternion.identity);
                    plantClone.transform.SetParent(parent);
                }
            }
        }

        // emits particles when the wheat is planted
        private void PlantingParticlesEmit()
        {
            ParticleSystem PlantParticlesCopy = Instantiate(PlantParticles, dirtSurface, Quaternion.identity);

            PlantParticlesCopy.transform.position = new Vector3(dirtSurface.x, dirtSurface.y + 0.45f, dirtSurface.z);

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
