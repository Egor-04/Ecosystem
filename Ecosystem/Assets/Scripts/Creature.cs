using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Targets {Canivores, Herbivores} //Плотоядные и Травоядные
public class Creature : MonoBehaviour
{
    [Header("Creature Stats")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _hunger = 100f;
    [SerializeField] private float _fatigue = 100f;

    [Header("Creature Sub-Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _attackPower;
    [SerializeField] private Transform _target;
    [SerializeField] private CharacterController _characterController;

    private void Start()
    {
        _speed = Random.Range(5f, 10f);
        _attackPower = Random.Range(0f, 10f);
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        SelectAction();
    }

    private void SelectAction()
    {
        if (_target == null)
        {

        }
    }

    private void CreateTarget()
    {

    }
}
