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
    public int JoyPercent;
    public float EnergyConsumption;
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
    public Creature FindedCreature;

    private void Start()
    {
        Speed = Random.Range(5f, 10f);
        AttackPower = Random.Range(0f, 10f);
        EnergyConsumption = Random.Range(1f, 3f);
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = Speed;
        CalculateNutritionalValue();
    }

    private void Update()
    {
        CheckStats();
        SubtractStats();
        DoAction();
    }

    private void CalculateNutritionalValue()
    {
        NutritionalValue = Mathf.RoundToInt((Speed * AttackPower) / 2);

        if (NutritionalValue <= 0f)
        {
            NutritionalValue = Random.Range(10f, 40f);
        }
    }

    private void CalculateJoy()
    {
        int sumHealthHungerPercent = Mathf.RoundToInt(Health + Hunger);
        JoyPercent = Mathf.RoundToInt(GetPercent(200, sumHealthHungerPercent)); //Слева указывается стопроцентное значение (но само число не является процентным), справа текущее значение (не процентное число)
    }

    private int GetPercent(int oneHundredValueInPercent, int currentValue)
    {
        int percent = currentValue * 100 / oneHundredValueInPercent;
        return percent;
    }

    private void CheckStats()
    {
        if (Health <= 0f)
        {
            gameObject.SetActive(false);
            Health = 0;
        }

        if (Hunger <= 0f)
        {
            Health -= Time.deltaTime;
            Hunger = 0f;
        }

        CalculateJoy();
    }

    private void SubtractStats()
    {
        Hunger -= Time.deltaTime * EnergyConsumption;
    }

    public virtual void DoAction()
    {
        if (Hunger > 50f)
        {
            CreateMovementPoint();
            MoveTo();
        }
    }

    public void CreateMovementPoint()
    {
        if (Distance <= 5f)
        {
            Vector3 newPointPosition = new Vector3(Random.Range(MinX, MaxX), transform.position.y, Random.Range(MinZ, MaxZ));
            Target = newPointPosition;
        }
    }

    public void MoveTo()
    {
        if (CreatureType != CreatureType.Herb)
        {
            Agent.SetDestination(Target);
            Distance = (Target - transform.position).sqrMagnitude;
                
            if (Distance < 5f)
            {
                if (IsHunger)
                {
                    Eat();
                }
            }
        }
    }

    public Creature FindFoodTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders != null)
            {
                Creature findedCreature = colliders[Random.Range(0, colliders.Length)].GetComponent<Creature>();
                return findedCreature;
            }
            else
            {
                CreateMovementPoint();
            }
        }
        return null;
    }
    
    public void SetTarget(Creature creature)
    {
        Target = creature.transform.position;
        FindedCreature = creature;
        return;
    }

    private void GetNutrients()
    {
        Health += FindedCreature.NutritionalValue / FindedCreature.Health;
        Hunger += FindedCreature.NutritionalValue;
    }

    private void Eat()
    {
        if (FindedCreature)
        {
            if (FindedCreature.Health <= 0f)
            {
                GetNutrients();
                FindedCreature.gameObject.SetActive(false);
                FindedCreature = null;
            }
            else
            {
                FindedCreature.TakeDamage((AttackPower*Speed)/2);
            }
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
