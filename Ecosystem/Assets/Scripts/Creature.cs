using UnityEngine;
using UnityEngine.AI;

public enum CreatureType {Carnivores, Herbivores, Herb}
public abstract class Creature : MonoBehaviour
{
    [Header("Creature Stats")]
    public CreatureType CreatureType;
    public int ID;
    [SerializeField] internal float NutritionalValue;
    [SerializeField] internal float Health = 100f;
    [SerializeField] internal float Hunger = 100f;
    [SerializeField] internal float BreedingTimeCoolDown = 50f;
    [SerializeField] internal float CurrentBreedTime = 50f;
    [SerializeField] internal int JoyPercent;
    [SerializeField] internal float EnergyConsumption;
    [SerializeField] internal NavMeshAgent Agent;

    [Header("Minimal Requirements")]
    [SerializeField] internal int MinimalJoy = 60;
    [SerializeField] internal int MinimalHunger = 50;
    [SerializeField] internal float SpeedRangeMin = 5f;
    [SerializeField] internal float SpeedRangeMax = 10f;
    [SerializeField] internal float AttackPowerRangeMin = 5f;
    [SerializeField] internal float AttackPowerRangeMax = 10f;


    [Header("Creature Sub-Stats")]
    [SerializeField] internal float Speed;
    [SerializeField] internal float AttackPower;
    [SerializeField] internal float UseRadius;
    [SerializeField] internal Color EatZoneColor;
    [SerializeField] internal Creature FindedCreature;

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
        Speed = Random.Range(SpeedRangeMin, SpeedRangeMax);
        CurrentBreedTime = BreedingTimeCoolDown;
        AttackPower = Random.Range(AttackPowerRangeMin, AttackPowerRangeMax);
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
            Creature creature = Instantiate(gameObject, transform.position + Vector3.forward, Quaternion.identity).GetComponent<Creature>();
            CreaturesCounter.InitCreatureCounter.AddCreature(this);
            ChangeCreatureOptions.InitChangeCreatureOptions.AddNewCreature(creature);
            CurrentBreedTime = BreedingTimeCoolDown;
            IsReadyToBreed = false;
            return;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public virtual void Kill()
    {
        Health = 0f;
        Agent.enabled = false;
        CreaturesCounter.InitCreatureCounter.RemoveCreature(this);
        ChangeCreatureOptions.InitChangeCreatureOptions.RemoveCreature(this);
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