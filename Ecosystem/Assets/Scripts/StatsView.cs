using UnityEngine;
using TMPro;

public class StatsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _hunger;
    [SerializeField] private TMP_Text _joy;
    [SerializeField] private Canvas _statCanvas;
    [SerializeField] private Creature _creature;

    private void Start()
    {
        _creature = GetComponent<Creature>();
    }

    private void Update()
    {
        _health.text = "Health: " + _creature.Health;
        _hunger.text = "Hunger: " + _creature.Hunger;
        _joy.text = "Joy: " + _creature.JoyPercent;
    }

    private void OnMouseDown()
    {
        if (_statCanvas.gameObject.activeSelf)
        {
            _statCanvas.gameObject.SetActive(false);
        }
        else
        {
            _statCanvas.gameObject.SetActive(true);
        }
    }
}
