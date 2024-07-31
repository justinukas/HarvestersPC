using UnityEngine;

namespace Main.Farming
{
    public class SeedBagSoundHandler : MonoBehaviour
    {
        public void PlayPlantingSFX()
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
