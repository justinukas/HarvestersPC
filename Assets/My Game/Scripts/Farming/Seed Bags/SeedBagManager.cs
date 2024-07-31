using UnityEngine;

namespace Main.Farming
{
    public class SeedBagManager : MonoBehaviour
    { 
        public int timesUsed;
        [HideInInspector] public string bagVariant;
        [HideInInspector] public GameObject tilledDirt;

        private PlantingRaycastHandler plantingRaycastHandler;
        private PlantInfoHandler plantInfoHandler;
        private PlantSpawningHandler plantSpawningHandler;
        private SeedBagColorHandler seedBagColorHandler;
        private SeedBagSoundHandler seedBagSoundHandler;
        private SeedBagParticleHandler seedBagParticleHandler;

        private void Start()
        {
            // renders bag unusable on game start
            //timesUsed = 50; 

            plantingRaycastHandler = GetComponent<PlantingRaycastHandler>();
            plantInfoHandler = GetComponent<PlantInfoHandler>();
            plantSpawningHandler = GetComponent<PlantSpawningHandler>();
            seedBagColorHandler = GetComponent<SeedBagColorHandler>();
            seedBagSoundHandler = GetComponent<SeedBagSoundHandler>();
            seedBagParticleHandler = GetComponent<SeedBagParticleHandler>();
        }

        private void Update()
        {
            plantingRaycastHandler.CheckRaycast(ref timesUsed, ref tilledDirt);
        }

        // initialized by the raycast check above
        public void InitializePlanting()
        {
            plantInfoHandler.InitializePlantInfo(ref bagVariant, tilledDirt);
            plantSpawningHandler.SpawnPlants(bagVariant, tilledDirt);
            seedBagColorHandler.FadeBagColor();
            seedBagSoundHandler.PlayPlantingSFX();
            seedBagParticleHandler.EmitPlantingParticles(tilledDirt);
        }
    }
}
