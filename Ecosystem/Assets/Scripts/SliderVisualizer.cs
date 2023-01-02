using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SliderVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SliderVisualize(Slider slider)
    {
        _text.text = slider.value.ToString("0.00");
    }

    public void PercentSliderVisualize(Slider slider)
    {
        _text.text = slider.value.ToString("0'%'");
    }
}