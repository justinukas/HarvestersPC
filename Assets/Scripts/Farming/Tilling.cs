using Main.Controls;
using UnityEngine;

namespace Main.Farming
{
    namespace Main.Controls
    {
        public class Tilling : MonoBehaviour
        {
            private float cooldown = 0.25f;
            private void Update()
            {
                cooldown -= Time.deltaTime;
            }

            [SerializeField] private GameObject TilledDirt;
            [SerializeField] private ParticleSystem ParticleSystem;

            private void OnCollisionEnter(Collision collider)
            {
                foreach (ContactPoint contactPoint in collider.contacts) // this foreach is for checking if the loclcollider colliding is the correct one
                {
                    if (collider.gameObject.CompareTag("Dirt") && cooldown <= 0 && GameObject.Find("Player").GetComponent<ToolInteractions>().isSwinging == true
                        && contactPoint.thisCollider.gameObject.name == "Head")

                    {
                        TillCounter TillCounter = collider.gameObject.GetComponent<TillCounter>();
                        TillCounter.timesTilled += 1;

                        cooldown = 0.25f;

                        Transform grassTransform = collider.gameObject.transform;

                        ParticleSystem dirtParticles = Instantiate(ParticleSystem, new Vector3(grassTransform.position.x, grassTransform.position.y + 0.67f, grassTransform.transform.position.z),
                            Quaternion.Euler(new Vector3(-90, 0, 0)));
                        dirtParticles.Emit(8);
                        dirtParticles.Stop();


                        if (TillCounter.timesTilled == 3)
                        {
                            dirtParticles.Emit(8);
                            dirtParticles.Stop();

                            Instantiate(TilledDirt, collider.gameObject.transform.position, collider.gameObject.transform.rotation);

                            Destroy(collider.gameObject);
                        }
                    }
                }
            }
        }
    }
}

