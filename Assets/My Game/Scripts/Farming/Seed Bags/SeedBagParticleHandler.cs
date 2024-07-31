using UnityEngine;

namespace Main.Farming
{
    public class SeedBagParticleHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem plantParticles;

        public void EmitPlantingParticles(GameObject tilledDirt)
        {
            ParticleSystem PlantParticlesCopy = Instantiate(plantParticles, tilledDirt.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            PlantParticlesCopy.Emit(10);
            PlantParticlesCopy.Stop();
        }
    }
}
