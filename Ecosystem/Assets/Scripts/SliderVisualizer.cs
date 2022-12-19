using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SliderVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text _textTimeSpeed;

    public void SliderVisualize(Slider slider)
    {
        _textTimeSpeed.text = slider.value.ToString("0.00");
    }
}