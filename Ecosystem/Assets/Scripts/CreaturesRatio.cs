using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CreaturesRatio : MonoBehaviour
{
    public static CreaturesRatio InitCreatureRatio;

    [SerializeField] private int _carnivoresValue;
    [SerializeField] private int _herbivoresValue;
    [SerializeField] private int _allCreaturesValue;

    [SerializeField] private Slider _firstSideSlider;
    [SerializeField] private Slider _secondSideSlider;

    [Header("Percent")]
    [SerializeField] private TMP_Text _percentFirstSide;
    [SerializeField] private TMP_Text _percentSecondSide;

    public void Awake()
    {
        InitCreatureRatio = this;
    }

    public void UpdateInfo()
    {
        _allCreaturesValue = CreaturesCounter.InitCreatureCounter.GetAllCreaturesCount();
        _carnivoresValue = CreaturesCounter.InitCreatureCounter.GetCarnivoresCreaturesCount();
        _herbivoresValue = CreaturesCounter.InitCreatureCounter.GetHerbivoresCreaturesCount();

        _firstSideSlider.value = Mathf.RoundToInt(_carnivoresValue * 100f / _allCreaturesValue);
        _secondSideSlider.value = Mathf.RoundToInt(_herbivoresValue * 100f / _allCreaturesValue);

        _percentFirstSide.text = _firstSideSlider.value.ToString() + "%";
        _percentSecondSide.text = _secondSideSlider.value.ToString() + "%";
    }
}
