using UnityEngine;
using UnityEngine.UI;

public class TimeScaler : MonoBehaviour
{
    public void TimeScaleSlider(Slider slider)
    {
        Time.timeScale = slider.value;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
