using Main.ItemHandling;
using UnityEngine;
using UnityEngine.UI;


// split this script up according to single responsibility principle

namespace Main.UI
{
    public class ChargeBar : MonoBehaviour
    {
        [SerializeField] private Gradient Gradient;
        [SerializeField] private GameObject Bag;
        [SerializeField] private ItemManager ItemManager;
        [SerializeField] private ItemDrop itemDrop;
        [SerializeField] private Animator BagAnimator;
        [SerializeField] private Slider Slider;
        [SerializeField] private GameObject Fill;
        [SerializeField] private CanvasGroup ChargeBarsCanvasGroup;
        private Transform MainCamera;


        private bool isFillFull = false;
        private bool isFillLocked = false;

        private void Start()
        {
            MainCamera = Camera.main.transform;

            ChargeBarsCanvasGroup.alpha = 0f;
            Slider.maxValue = 1;
            Slider.minValue = 0;
        }

        private void Update()
        {
            FadeInBar();
            if (ItemManager.grabbedPlant != null) { ResetSlider(); return; }
            FillBar();
            LockBar();
            ThrowBag();
            HideBar();
        }

        // shows bar when the bag is picked up
        private void FadeInBar()
        {
            if (ItemManager.currentTool == "Bag" && ChargeBarsCanvasGroup.alpha < 1f && isFillLocked == false)
            {
                ChargeBarsCanvasGroup.alpha += 3f * Time.deltaTime;
            }
            // fix for if it gets softlocked
            else if (ChargeBarsCanvasGroup.alpha < 1f && Slider.value > 0f && ItemManager.currentTool == "Bag")
            {
                ChargeBarsCanvasGroup.alpha = 0f;
                Slider.value = 0f;
                isFillLocked = false;
            }
        }

        // fills bar up and down and changes color accordingly
        private void FillBar()
        {
            if (Input.GetMouseButton(0))
            {
                if (isFillLocked == false && ChargeBarsCanvasGroup.alpha == 1)
                {
                    if (BagAnimator.GetCurrentAnimatorStateInfo(0).IsName("Charge Up Bag Throw") || BagAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hold Charge"))
                    {
                        if (Slider.value == 1) isFillFull = true;
                        if (Slider.value == 0) isFillFull = false;

                        if (isFillFull == false)
                        {
                            Slider.value += 2 * Time.deltaTime;
                        }
                        if (isFillFull)
                        {
                            Slider.value -= 2 * Time.deltaTime;
                        }

                        Fill.GetComponent<Image>().color = Gradient.Evaluate(Slider.value);
                    }
                }
            }
        }

        // makes chargebar fade out when LMB is released and locks it from changing in value
        private void LockBar()
        {
            if (!Input.GetMouseButton(0))
            {
                if (Slider.value > 0 && isFillLocked == false)
                {
                    isFillLocked = true;
                    BagAnimator.SetTrigger("Throw");
                }
            }
        }

        private void ThrowBag()
        {
            if (isFillLocked == true && ChargeBarsCanvasGroup.alpha == 1)
            {
                if (BagAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
                {
                    Bag.transform.position = Bag.transform.position + new Vector3(0, 0.314f, 0);

                    itemDrop.DropTool(ref ItemManager.currentTool, ref ItemManager.grabbedTool);

                    float throwingForce = Slider.value * 10f;
                    Bag.GetComponent<Rigidbody>().AddForce(MainCamera.forward * throwingForce, ForceMode.VelocityChange);
                }
            }
        }

        // hide charge bar after animation is finished and bar is locked
        private void HideBar()
        {
            if (ChargeBarsCanvasGroup.alpha <= 1f && isFillLocked == true && BagAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
            {
                ChargeBarsCanvasGroup.alpha -= 3f * Time.deltaTime;
                if (ChargeBarsCanvasGroup.alpha == 0f)
                {
                    ResetSlider();
                }
            }
            else if (ItemManager.grabbedTool == null)
            {
                ChargeBarsCanvasGroup.alpha -= 3f * Time.deltaTime;
                if (ChargeBarsCanvasGroup.alpha == 0f)
                {
                    ResetSlider();
                }
            }
        }

        private void ResetSlider()
        {
            isFillLocked = false;
            Slider.value = 0f;
            BagAnimator.Play("DefaultState");
        }
    }
}
