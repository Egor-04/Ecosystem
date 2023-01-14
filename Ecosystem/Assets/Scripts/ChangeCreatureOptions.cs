using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class ChangeCreatureOptions : MonoBehaviour
{
    public static ChangeCreatureOptions InitChangeCreatureOptions;
    [SerializeField] private List<Creature> _creatureList = new List<Creature>();
    [SerializeField] private List<Creature> _creatureCarnivoresList = new List<Creature>();
    [SerializeField] private List<Creature> _creatureHerbivoresList = new List<Creature>();

    public void Awake()
    {
        InitChangeCreatureOptions = this;
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
    }

    public void RemoveCreature(Creature creature)
    {
        if (creature.CreatureType != CreatureType.Herb)
        {
            _creatureList.Remove(creature);
        }
    }

    public void SetSpeedValue(TMP_InputField inputfield)
    {
        for (int i = 0; i < _creatureList.Count; i++)
        {
            _creatureList[i].Agent.speed = Convert.ToSingle(inputfield.text);
        }
    }

    public void SetAttackPowerValue(TMP_InputField inputfield)
    {
        for (int i = 0; i < _creatureList.Count; i++)
        {
            _creatureList[i].AttackPower = Convert.ToSingle(inputfield.text);
        }
    }
}
