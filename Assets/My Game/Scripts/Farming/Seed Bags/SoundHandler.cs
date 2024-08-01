using UnityEngine;

namespace Main.Farming.SeedBags
{
    public class SoundHandler : MonoBehaviour
    {
        public void PlayPlantingSFX()
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
