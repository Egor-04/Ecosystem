using UnityEngine;
using UnityEngine.AI;

public enum CreatureType {Carnivores, Herbivores, Herb} //Плотоядные, Травоядные и Растения
public abstract class Creature : MonoBehaviour
{
    [Header("Creature Stats")]
    public CreatureType CreatureType;
    public int ID;
    public float NutritionalValue;
    public float Health = 100f; // Здоровье
    public float Hunger = 100f; // Голод
    public float BreedingTimeCoolDown = 50f;
    public float CurrentBreedTime = 50f;
    public int JoyPercent;
    public float EnergyConsumption;
    public NavMeshAgent Agent;

    [Header("Creature Sub-Stats")]
    public float Speed;
    public float AttackPower;
    public float UseRadius;
    public Color EatZoneColor;
    public Creature FindedCreature;

    [Header("Current creature State")]
    public bool IsHunger;
    public bool IsReadyToBreed;

    [Header("Target Point")]
    public Vector3 Target;
    public float Distance;

    public float MinX = -180f, MaxX = 180f;
    public float MinZ = -180f, MaxZ = 180f;
    public float RadiusVision = 10f;
    public Color FindZoneColor;

    private void Awake()
    {
        ID = Random.Range(1, 9999999);
        Speed = Random.Range(5f, 10f);
        CurrentBreedTime = BreedingTimeCoolDown;
        AttackPower = Random.Range(5f, 10f);
        EnergyConsumption = Random.Range(1f, 3f);
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = Speed;
        CalculateNutritionalValue();
    }

    private void Update()
    {
        if (CreatureType != CreatureType.Herb)
        {
            CheckStats();
            SubtractStats();
            DoAction();
        }
    }

    private void CalculateNutritionalValue()
    {
        NutritionalValue = Random.Range(10f, 40f);
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
            Kill();
        }

        if (Hunger <= 0f)
        {
            Health -= Time.deltaTime;
            Hunger = 0f;
        }
        
        if (CurrentBreedTime <= 0f)
        {
            CurrentBreedTime = 0f;
        }

        Health = Mathf.Clamp(Health, 0f, 100f);
        Hunger = Mathf.Clamp(Hunger, 0f, 100f);
        CurrentBreedTime = Mathf.Clamp(CurrentBreedTime, 0f, BreedingTimeCoolDown);
        CalculateJoy();
    }

    private void SubtractStats()
    {
        CurrentBreedTime -= Time.deltaTime;
        Hunger -= Time.deltaTime * EnergyConsumption;
    }

    public virtual void DoAction()
    {
        MoveTo();
        
        if (Hunger > 50f)
        {
            CreateMovementPoint();
        }
    }

    public void CreateMovementPoint()
    {
        if (Distance <= 5f)
        {
            Vector3 newPointPosition = new Vector3(Random.Range(MinX, MaxX), transform.position.y, Random.Range(MinZ, MaxZ));
            Target = newPointPosition;
        }

        // NEW
        if (Distance <= 5f && FindedCreature == null)
        {
            Vector3 newPointPosition = new Vector3(Random.Range(MinX, MaxX), transform.position.y, Random.Range(MinZ, MaxZ));
            Target = newPointPosition;
        }
    }

    public void MoveTo()
    {
        if (Agent)
        {
            if (FindedCreature)
            {
                Agent.SetDestination(FindedCreature.transform.position);
                Distance = (new Vector3(FindedCreature.transform.position.x, transform.position.y, FindedCreature.transform.position.z) - transform.position).sqrMagnitude;
            }
            else
            {
                CreateMovementPoint();// NEW
                Agent.SetDestination(Target);
                Distance = (new Vector3(Target.x, transform.position.y, Target.z) - transform.position).sqrMagnitude;
            }
        }
        
        if (Distance < UseRadius)
        {
            if (IsHunger)
            {
                Eat();
            }

            if (IsReadyToBreed)
            {
                BreedCreature();
            }
        }
    }

    public Creature FindNearbyCreature()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, RadiusVision);

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
        Health += FindedCreature.NutritionalValue;
        Hunger += FindedCreature.NutritionalValue;
    }

    private void Eat()
    {
        if (FindedCreature)
        {
            if (FindedCreature.Health <= 0f)
            {
                GetNutrients();
                FindedCreature.Kill();
                FindedCreature = null;
                return;
            }
            else
            {
                FindedCreature.TakeDamage((AttackPower*Speed)/2);
            }
        }
    }

    private void BreedCreature()
    {
        if (FindedCreature)
        {
            Instantiate(gameObject, transform.position + Vector3.forward, Quaternion.identity);
            CurrentBreedTime = BreedingTimeCoolDown;
            IsReadyToBreed = false;
            return;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void Kill()
    {
        Health = 0f;
        Agent.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = FindZoneColor;
        Gizmos.DrawWireSphere(transform.position, RadiusVision);

        Gizmos.color = EatZoneColor;
        Gizmos.DrawWireSphere(transform.position, UseRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(Target, 2f);
    }
}
