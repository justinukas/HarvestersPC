using UnityEngine;

namespace Main.Farming
{
    public class SeedBagManager : MonoBehaviour
    { 
        public int timesUsed;
        [HideInInspector] public string bagVariant;
        [HideInInspector] public GameObject tilledDirt;

        private PlantingRaycastHandler PlantingRaycastHandler;
        private PlantInfoHandler PlantInfoHandler;
        private PlantSpawningHandler PlantSpawningHandler;
        private SeedBagColorHandler SeedBagColorHandler;
        private SeedBagSoundHandler SeedBagSoundHandler;
        private SeedBagParticleHandler SeedBagParticleHandler;
        
        private void Start()
        {
            // renders bag unusable on game start
            //timesUsed = 50; 

            PlantingRaycastHandler = GetComponent<PlantingRaycastHandler>();
            PlantInfoHandler = GetComponent<PlantInfoHandler>();
            PlantSpawningHandler = GetComponent<PlantSpawningHandler>();
            SeedBagColorHandler = GetComponent<SeedBagColorHandler>();
            SeedBagSoundHandler = GetComponent<SeedBagSoundHandler>();
            SeedBagParticleHandler = GetComponent<SeedBagParticleHandler>();
        }

        private void Update()
        {
            PlantingRaycastHandler.CheckRaycast(ref timesUsed, ref tilledDirt);
        }

        // initialized by the raycast check above
        public void InitializePlanting()
        {
            PlantInfoHandler.InitializePlantInfo(ref bagVariant, tilledDirt);
            PlantSpawningHandler.SpawnPlants(bagVariant, tilledDirt);
            SeedBagColorHandler.FadeBagColor();
            SeedBagSoundHandler.PlayPlantingSFX();
            SeedBagParticleHandler.EmitPlantingParticles(tilledDirt);
        }
    }
}
