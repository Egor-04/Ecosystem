using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

enum CreatureOptions {All, Carnivores, Herbivores}
public class ChangeCreatureOptions : MonoBehaviour
{
    public static ChangeCreatureOptions InitChangeCreatureOptions;
    [SerializeField] private TMP_Dropdown _dropdownOptionsCreature;
    [SerializeField] private CreatureOptions _creatureOptionsType;
    [SerializeField] private List<Creature> _creatureList = new List<Creature>();
    [SerializeField] private List<Creature> _creatureCarnivoresList = new List<Creature>();
    [SerializeField] private List<Creature> _creatureHerbivoresList = new List<Creature>();

    public void Awake()
    {
        InitChangeCreatureOptions = this;

        string[] enumNames = Enum.GetNames(typeof(CreatureOptions));
        List<string> options = new List<string>(enumNames);
        _dropdownOptionsCreature.AddOptions(options);
    }

    public void GetDropdown(TMP_Dropdown dropdown)
    {
        CreatureOptions creatureOptions = (CreatureOptions)dropdown.value;
        _creatureOptionsType = creatureOptions;
    }

    public void SetJoyValue(Slider slider)
    {
        Debug.LogError("CHANGED");
        for (int i = 0; i < _creatureList.Count; i++)
        {
            _creatureList[i].MinimalJoy = Convert.ToInt32(slider.value);
        }
    }

    public void SetHungerValue(Slider slider)
    {
        for (int i = 0; i < _creatureList.Count; i++)
        {
            _creatureList[i].MinimalHunger = Convert.ToInt32(slider.value);
        }
    }

    public void AddNewCreature(Creature creature)
    {
        if (creature.CreatureType != CreatureType.Herb)
        {
            _creatureList.Add(creature);
        }

        switch (creature.CreatureType)
        {
            case CreatureType.Herb: return;
            case CreatureType.Herbivores:
                {
                    _creatureHerbivoresList.Add(creature);
                    break;
                }
            case CreatureType.Carnivores:
                {
                    _creatureCarnivoresList.Add(creature);
                    break;
                }
        }
    }

    public void RemoveCreature(Creature creature)
    {
        if (creature.CreatureType != CreatureType.Herb)
        {
            _creatureList.Remove(creature);
        }

        switch (creature.CreatureType)
        {
            case CreatureType.Herb: return;
            case CreatureType.Herbivores:
                {
                    _creatureHerbivoresList.Remove(creature);
                    break;
                }
            case CreatureType.Carnivores:
                {
                    _creatureCarnivoresList.Remove(creature);
                    break;
                }
        }
    }

    public void SetSpeedValue(TMP_InputField inputfield)
    {
        switch (_creatureOptionsType)
        {
            case CreatureOptions.All:
                {       
                    for (int i = 0; i < _creatureList.Count; i++)
                    {
                        _creatureList[i].Agent.speed = Convert.ToSingle(inputfield.text);
                    }
                    break;
                }
            case CreatureOptions.Carnivores:
                {
                    for (int i = 0; i < _creatureCarnivoresList.Count; i++)
                    {
                        _creatureCarnivoresList[i].Agent.speed = Convert.ToSingle(inputfield.text);
                    }
                    break;
                }
            case CreatureOptions.Herbivores:
                {
                    for (int i = 0; i < _creatureHerbivoresList.Count; i++)
                    {
                        _creatureHerbivoresList[i].Agent.speed = Convert.ToSingle(inputfield.text);
                    }
                    break;
                }
        }
    }

    public void SetAttackPowerValue(TMP_InputField inputfield)
    {
        switch (_creatureOptionsType)
        {
            case CreatureOptions.All:
                {
                    for (int i = 0; i < _creatureList.Count; i++)
                    {
                        _creatureList[i].AttackPower = Convert.ToSingle(inputfield.text);
                    }
                    break;
                }
            case CreatureOptions.Carnivores:
                {
                    for (int i = 0; i < _creatureCarnivoresList.Count; i++)
                    {
                        _creatureCarnivoresList[i].AttackPower = Convert.ToSingle(inputfield.text);
                    }
                    break;
                }
            case CreatureOptions.Herbivores:
                {
                    for (int i = 0; i < _creatureHerbivoresList.Count; i++)
                    {
                        _creatureHerbivoresList[i].AttackPower = Convert.ToSingle(inputfield.text);
                    }
                    break;
                }
        }
    }

    public void SetBreedTimeValue(TMP_InputField inputfield)
    {
        switch (_creatureOptionsType)
        {
            case CreatureOptions.All:
                {
                    for (int i = 0; i < _creatureList.Count; i++)
                    {
                        _creatureList[i].BreedingTimeCoolDown = Convert.ToSingle(inputfield.text);
                    }
                    break;
                }
            case CreatureOptions.Carnivores:
                {
                    for (int i = 0; i < _creatureCarnivoresList.Count; i++)
                    {
                        _creatureCarnivoresList[i].BreedingTimeCoolDown = Convert.ToSingle(inputfield.text);
                    }
                    break;
                }
            case CreatureOptions.Herbivores:
                {
                    for (int i = 0; i < _creatureHerbivoresList.Count; i++)
                    {
                        _creatureHerbivoresList[i].BreedingTimeCoolDown = Convert.ToSingle(inputfield.text);
                    }
                    break;
                }
        }
    }
}