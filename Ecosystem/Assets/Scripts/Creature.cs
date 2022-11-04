using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CreatureType {Carnivores, Herbivores, Herb} //Плотоядные, Травоядные и Растения
public abstract class Creature : MonoBehaviour
{
    [Header("Creature Stats")]
    public CreatureType CreatureType;
    public float NutritionalValue;
    public float Health = 100f; // Здоровье
    public float Hunger = 100f; // Голод
    public NavMeshAgent Agent;

    [Header("Creature Sub-Stats")]
    public float Speed;
    public float AttackPower;
    public float EatRadius;
    public Color EatZoneColor;

    [Header("Current creature State")]
    public bool IsHunger;

    [Header("Target Point")]
    public Vector3 Target;
    public float Distance;

    public float MinX = -180f, MaxX = 180f;
    public float MinZ = -180f, MaxZ = 180f;
    public float Radius = 10f;
    public Color FindZoneColor;
    private Creature _findedCreature;

    private void Start()
    {
        Speed = Random.Range(5f, 10f);
        AttackPower = Random.Range(0f, 10f);
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = Speed;
        CalculateNutritionalValue();
    }

    private void Update()
    {
        DoAction();
    }

    private void CalculateNutritionalValue()
    {
        NutritionalValue = (Speed * AttackPower) / 2;

        if (NutritionalValue <= 0f)
        {
            NutritionalValue = Random.Range(10f, 40f);
        }
    }

    public virtual void DoAction()
    {
        if (Hunger > 50f)
        {
            CreateMovementPoint();
        }

        MoveTo();
    }

    public void CreateMovementPoint()
    {
        if (Distance <= 5f)
        {
            Target = new Vector3(Random.Range(MinX, MaxX), transform.position.y, Random.Range(MinZ, MaxZ));
        }
    }

    public void MoveTo()
    {
        if (CreatureType != CreatureType.Herb)
        {
            Agent.SetDestination(Target);
            Distance = (Target - transform.position).sqrMagnitude;
                
            Debug.LogError(Distance);
            if (Distance < 5f)
            {
                if (IsHunger)
                {
                    Eat();
                }
                return;
            }
        }
    }

    public void SetTarget(Creature creature)
    {
        Target = creature.transform.position;
        return;
    }

    public Creature FindFoodTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);

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

    private void GetNutrients()
    {
        Hunger += _findedCreature.NutritionalValue;
    }

    private void Eat()
    {
        Debug.LogError("Eaaat");
        // Ошибки важные были решены, теперь надо здесь написать проверку какой коллайдер ест существо иначе в проверке он будет высвечивать ошибку, что не найдено существо
        if (_findedCreature.Health <= 0f)
        {
            GetNutrients();
            Destroy(_findedCreature.gameObject);
            _findedCreature = null;
        }
        else
        {
            _findedCreature.TakeDamage((AttackPower*Speed)/2);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = FindZoneColor;
        Gizmos.DrawWireSphere(transform.position, Radius);

        Gizmos.color = EatZoneColor;
        Gizmos.DrawWireSphere(transform.position, EatRadius);
    }
}
