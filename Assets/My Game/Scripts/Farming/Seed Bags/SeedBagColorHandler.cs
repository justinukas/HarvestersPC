using UnityEngine;

namespace Main.Farming
{
    public class SeedBagColorHandler : MonoBehaviour
    {
        [HideInInspector] public float r;
        private readonly float rFullBag = 0.05490192f;
        private readonly float rDepletedBag = 0.509804f;

        private void Start()
        {
            r = gameObject.transform.Find("Seed Bag").GetComponent<Renderer>().materials[0].color.r;
            Debug.Log(r);
        }

        // fades color of bag to indicate uses left
        public void FadeBagColor()
        {
            r += 0.01019608f;
            ChangeColor();
        }

        // change color of bag according to times used
        private void ChangeColor()
        {
            if (r <= rDepletedBag)
            {
                var bagMaterials = gameObject.transform.Find("Seed Bag").GetComponent<Renderer>().materials;
                bagMaterials[0].color = new Color(r, 0.396f, 0.0666f);
            }
        }

        public void ResetColors()
        {
            r = rFullBag;
            ChangeColor();
        }
    }
}
