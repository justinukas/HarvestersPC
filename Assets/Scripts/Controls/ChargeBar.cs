using UnityEngine;
using UnityEngine.UI;


// script is handled by Assets/Scripts/Controls/Controls.cs
public class ChargeBar : MonoBehaviour
{
    [SerializeField] private Slider Slider;
    [SerializeField] private GameObject Fill;
    [SerializeField] private Gradient Gradient;
    [SerializeField] private CanvasGroup ChargeBarsCanvasGroup;
    [SerializeField] private GameObject Bag;
    [SerializeField] private Transform MainCamera;
    [SerializeField] private ToolInteractions ToolInteractions;

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

    // shows bar when the bag is picked up
    public void ShowBar()
    {
        if (ToolInteractions.currentItem == "Bag" && ChargeBarsCanvasGroup.alpha < 1f && isFillLocked == false)
        {
            ChargeBarsCanvasGroup.alpha += 3f * Time.deltaTime;
        }
        // fix for if it gets softlocked
        else if (ChargeBarsCanvasGroup.alpha < 1f && Slider.value > 0f && ToolInteractions.currentItem == "Bag")
        {
            ChargeBarsCanvasGroup.alpha = 0f;
            Slider.value = 0f;
            isFillLocked = false;
        }
    }

    // fills bar up and down and changes color accordingly
    public void FillBar()
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

    // makes chargebar fade out when LMB is released and locks it from changing in value
    public void LockBar()
    {
        if (Slider.value > 0 && isFillLocked == false)
        {
            isFillLocked = true;
            BagAnimator.SetTrigger("Throw");
        }
    }

    public void ThrowBag()
    {
        if (isFillLocked == true && ChargeBarsCanvasGroup.alpha == 1)
        {
            if (BagAnimator.GetCurrentAnimatorStateInfo(0).IsName("DefaultState") && ToolInteractions.currentItem != "null")
            {
                Bag.transform.position = ToolInteractions.defaultToolPosition.position + new Vector3(0, 0.314f, 0);

                ToolInteractions.grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                ToolInteractions.isSwinging = false;
                ToolInteractions.currentItem = "null";
                ToolInteractions.grabbedObject = null;

                float throwingForce = Slider.value * 10f;
                Bag.GetComponent<Rigidbody>().AddForce(MainCamera.forward * throwingForce, ForceMode.VelocityChange);
            }
        }
    }

    // hide charge bar after animation is finished and bar is locked
    public void HideBar()
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
