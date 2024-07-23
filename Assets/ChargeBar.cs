using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] private Slider Slider;
    [SerializeField] private GameObject Fill;
    [SerializeField] private Gradient Gradient = null;
    [SerializeField] private CanvasGroup ChrgBarsCanvasGroup;

    private float fillAmount;
    private bool fillIsFull = false;
    private bool lockFill = false;

    private void Start()
    {
        Slider.maxValue = 1;
        Slider.minValue = 0;
    }

    private void Update()
    {
        // fills bar up and down 
        if (Input.GetMouseButton(0) && lockFill == false)
        {
            if (Slider.value == 1) fillIsFull = true;
            if (Slider.value == 0) fillIsFull = false;

            if (fillIsFull == false) 
            {
                Slider.value += 2 * Time.deltaTime;
            }
            if (fillIsFull)
            {
                Slider.value -= 2 * Time.deltaTime;
            }

            Fill.GetComponent<Image>().color = Gradient.Evaluate(Slider.value);
        }

        // makes chargebar fade out when LMB is released and locks it from changing in value
        if (Input.GetMouseButton(0) == false && Slider.value > 0)
        {
            lockFill = true;

            if (ChrgBarsCanvasGroup.alpha >= 0f && ChrgBarsCanvasGroup.alpha <= 1f) 
            {
                ChrgBarsCanvasGroup.alpha -= 1f * Time.deltaTime;
            }
            // add throw bag
        }
    }
}
