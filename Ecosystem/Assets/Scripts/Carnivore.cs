using UnityEngine;

public class Carnivore : Creature
{
    public override void DoAction()
    {
        base.DoAction();

        if (Hunger <= 50f)
        {
            IsHunger = true;
            Creature creature = FindFoodTarget();
            
            if (creature)
            {
                if (creature.CreatureType == CreatureType.Herbivores)
                {
                    SetTarget(creature);
                }
            }
            else
            {
                CreateMovementPoint();
                MoveTo();
            }
        }
        else
        {
            IsHunger = false;
        }
    }
}
