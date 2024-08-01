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

        private Slider Slider;
        private GameObject Fill;
        private CanvasGroup ChargeBarsCanvasGroup;
        private Transform MainCamera;
        private ItemManager ItemManager;
        private Animator BagAnimator;

        private bool isFillFull = false;
        private bool isFillLocked = false;

        private void Start()
        {
            Slider = gameObject.transform.Find("Canvas").Find("Slider").GetComponent<Slider>();
            Fill = gameObject.transform.Find("Canvas").Find("Slider").Find("Fill Area").Find("Fill").gameObject;
            ChargeBarsCanvasGroup = gameObject.transform.Find("Canvas").GetComponent<CanvasGroup>();
            MainCamera = Camera.main.transform;
            ItemManager = GameObject.Find("Player").GetComponent<ItemManager>();
            BagAnimator = Bag.transform.Find("Bag").GetComponent<Animator>();

            ChargeBarsCanvasGroup.alpha = 0f;
            Slider.maxValue = 1;
            Slider.minValue = 0;
        }

        private void Update()
        {
            FadeInBar();
            FillBar();
            LockBar();
            ThrowBag();
            HideBar();
        }

        // shows bar when the bag is picked up
        private void FadeInBar()
        {
            if (ItemManager.currentItem == "Bag" && ChargeBarsCanvasGroup.alpha < 1f && isFillLocked == false)
            {
                ChargeBarsCanvasGroup.alpha += 3f * Time.deltaTime;
            }
            // fix for if it gets softlocked
            else if (ChargeBarsCanvasGroup.alpha < 1f && Slider.value > 0f && ItemManager.currentItem == "Bag")
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

                    ItemManager.grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    Debug.Log(ItemManager.grabbedObject.name);
                    ItemManager.isSwinging = false;
                    ItemManager.currentItem = "null";
                    ItemManager.grabbedObject = null;

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
                    Slider.value = 0f;
                    isFillLocked = false;
                }
            }
        }
    }
}
