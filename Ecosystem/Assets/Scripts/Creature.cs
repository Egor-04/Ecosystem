using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CreatureType {Carnivores, Herbivores, Herb} //Плотоядные и Травоядные
public abstract class Creature : MonoBehaviour
{
    [Header("Creature Stats")]
    [SerializeField] private CreatureType _creatureType;
    [SerializeField] private float _health = 100f; // Здоровье
    [SerializeField] private float _hunger = 100f; // Голод
    [SerializeField] private NavMeshAgent _agent;

    [Header("Creature Sub-Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _attackPower;
    
    [Header("Target Point")]
    [SerializeField] private Vector3 _target;

    [SerializeField] private float _minX, _maxX;
    [SerializeField] private float _minZ, _maxZ;
    [SerializeField] private float _radius = 10f;
    [SerializeField] private Color _color;
    private Creature _findedCreature;

    private void Start()
    {
        _speed = Random.Range(5f, 10f);
        _attackPower = Random.Range(0f, 10f);
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        DoAction();
    }

    private void DoAction()
    {
        if (_hunger > 50f)
        {
            CreateMovementPoint();
            MoveTo();
        }
    }

    private void CreateMovementPoint()
    {
        if (_target == Vector3.zero)
        {
            _target = new Vector3(Random.Range(_minX, _maxX), 1f, Random.Range(_minZ, _maxZ));
            Debug.LogError("Target");
        }
    }

    private void MoveTo()
    {
        if (_target != Vector3.zero)
        {
            _agent.SetDestination(_target);

            float distance = (_target - transform.position).sqrMagnitude;
            
            if (distance < 6f)
            {
                _target = Vector3.zero;
                return;
            }
        }
    }

    private Creature FindFoodTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders != null)
            {
                _findedCreature = colliders[Random.Range(0, colliders.Length)].GetComponent<Creature>();
            }
            else
            {
                CreateMovementPoint();
            }
        }
        return _findedCreature;
    }

    public CreatureType GetCreatureType()
    {
        return _creatureType;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
