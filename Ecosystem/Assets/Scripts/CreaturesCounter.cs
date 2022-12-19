using UnityEngine;
using TMPro;

public class CreaturesCounter : MonoBehaviour
{
    public static CreaturesCounter InitCreatureCounter;
    [SerializeField] private int _allCreaturesCount;
    [SerializeField] private int _carnivoresCount;
    [SerializeField] private int _herbivoresCount;
    [SerializeField] private TMP_Text _firstCreatures;
    [SerializeField] private TMP_Text _secondCreatures;
    [SerializeField] private TMP_Text _allCreatures;

    private void Awake()
    {
        InitCreatureCounter = this;
    }

    public int GetAllCreaturesCount()
    {
        return _allCreaturesCount;
    }

    public int GetCarnivoresCreaturesCount()
    {
        return _carnivoresCount;
    }

    public int GetHerbivoresCreaturesCount()
    {
        return _herbivoresCount;
    }

    public void AddCreature(Creature creature)
    {
        if (creature.CreatureType == CreatureType.Carnivores)
        {
            _carnivoresCount++;
            _firstCreatures.text = "Count: " + _carnivoresCount;
        }
        else if (creature.CreatureType == CreatureType.Herbivores)
        {
            _herbivoresCount++;
            _secondCreatures.text = "Count: " + _herbivoresCount;
        }

        _allCreaturesCount = _carnivoresCount + _herbivoresCount;
        _allCreatures.text = "All Count: " + _allCreaturesCount;
        
        CreaturesRatio.InitCreatureRatio.UpdateInfo();
    }

    public void RemoveCreature(Creature creature)
    {
        if (creature.CreatureType == CreatureType.Carnivores)
        {
            _carnivoresCount--;
            _firstCreatures.text = "Count: " + _carnivoresCount;
        }
        else if (creature.CreatureType == CreatureType.Herbivores)
        {
            _herbivoresCount--;
            _secondCreatures.text = "Count: " + _herbivoresCount;
        }
        
        _allCreaturesCount = _carnivoresCount + _herbivoresCount;
        _allCreatures.text = "All Count: " + _allCreaturesCount;
        
        CreaturesRatio.InitCreatureRatio.UpdateInfo();
    }
}
