using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGS.UI
{
    public class SliderUI : MonoBehaviour
    {
        public Slider Slider;
        public TextMeshProUGUI SliderText;


        public void SetSliderValue(float value)
        {
            Slider.value = value;

            SliderText.text = value.ToString("0");
        }
    }

}