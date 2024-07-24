using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] private Slider Slider;
    [SerializeField] private GameObject Fill;
    [SerializeField] private Gradient Gradient;
    [SerializeField] private CanvasGroup ChargeBarsCanvasGroup;
    [SerializeField] private GameObject Bag;
    [SerializeField] private Transform MainCamera;
    [SerializeField] private ItemInteractions ItemInteractions;

    private bool isFillFull = false;
    private bool isFillLocked = false;
    private Animator BagAnimator;

    private void Start()
    {
        ChargeBarsCanvasGroup.alpha = 0f;
        Slider.maxValue = 1;
        Slider.minValue = 0;
        BagAnimator = Bag.transform.Find("Bag").GetComponent<Animator>();
    }

    private void Update()
    {
        ShowBar();
        FillBar();
        LockBar();
        ThrowBag();
        HideBar();
        Debug.Log(ChargeBarsCanvasGroup.alpha);
        Debug.Log(isFillLocked);
        Debug.Log(ItemInteractions.currentItem);
    }

    // shows bar when the bag is picked up
    private void ShowBar()
    {
        if (ItemInteractions.currentItem == "Bag" && ChargeBarsCanvasGroup.alpha < 1f && isFillLocked == false)
        {
            ChargeBarsCanvasGroup.alpha += 3f * Time.deltaTime;
        }
        // fix for if it gets softlocked
        else if (ChargeBarsCanvasGroup.alpha < 1f && Slider.value > 0f && ItemInteractions.currentItem == "Bag")
        {
            ChargeBarsCanvasGroup.alpha = 0f;
            Slider.value = 0f;
        }
    }

    // fills bar up and down and changes color accordingly
    private void FillBar()
    {
        if (Input.GetMouseButton(0) && isFillLocked == false && ChargeBarsCanvasGroup.alpha == 1)
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

    // makes chargebar fade out when LMB is released and locks it from changing in value
    private void LockBar()
    {
        if (Input.GetMouseButton(0) == false && Slider.value > 0 && isFillLocked == false)
        {
            isFillLocked = true;
            BagAnimator.SetTrigger("Throw");
        }
    }

    private void ThrowBag()
    {
        if (isFillLocked == true && ChargeBarsCanvasGroup.alpha == 1)
        {
            if (BagAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState") && ItemInteractions.currentItem != "null")
            {
                Bag.transform.position = ItemInteractions.defaultToolPosition.position + new Vector3(0, 0.314f, 0);

                ItemInteractions.grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                ItemInteractions.isSwinging = false;
                ItemInteractions.currentItem = "null";
                ItemInteractions.grabbedObject = null;

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
