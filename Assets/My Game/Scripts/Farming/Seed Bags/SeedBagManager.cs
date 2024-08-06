using UnityEngine;

namespace Main.Farming.SeedBags
{
    public class SeedBagManager : MonoBehaviour
    { 
        [HideInInspector] public int timesUsed;
        [HideInInspector] public int maxTimesUsed = 50;
        [HideInInspector] public string bagVariant;
        [HideInInspector] public GameObject tilledDirt;

        [Header("Prefabs For Scripts")]
        [SerializeField] ParticleSystem plantParticles;
        [SerializeField] GameObject Wheat;
        [SerializeField] GameObject Carrot;

        private RaycastHandler raycastHandler;
        private PlantInfoHandler plantInfoHandler;
        private PlantSpawningHandler plantSpawningHandler;
        private ColorHandler colorHandler;
        private SoundHandler soundHandler;
        private ParticleHandler particleHandler;

        private void Start()
        {
            raycastHandler = GetComponent<RaycastHandler>();
            plantInfoHandler = GetComponent<PlantInfoHandler>();
            plantSpawningHandler = GetComponent<PlantSpawningHandler>();
            colorHandler = GetComponent<ColorHandler>();
            soundHandler = GetComponent<SoundHandler>();
            particleHandler = GetComponent<ParticleHandler>();

            // gets the "Carrot" or "Wheat" part from the gameobject name and assigns it to bagVariant
            string[] _string = gameObject.name.Split(' ');
            bagVariant = _string[0];

            //renders bag unusable on game start
            timesUsed = maxTimesUsed; 
            colorHandler.SetColor();
        }

        private void Update()
        {
            raycastHandler.CheckRaycast(ref timesUsed, maxTimesUsed, ref tilledDirt);
        }

        // initialized by the raycast check above
        public void InitializePlanting()
        {
            plantInfoHandler.InitializePlantInfo(tilledDirt, Wheat, Carrot);
            plantSpawningHandler.SpawnPlants(bagVariant, tilledDirt);
            colorHandler.FadeBagColor();
            soundHandler.PlayPlantingSFX();
            particleHandler.EmitPlantingParticles(tilledDirt, plantParticles);
        }
    }
}
