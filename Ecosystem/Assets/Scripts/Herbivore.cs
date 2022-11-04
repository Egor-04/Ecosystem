using UnityEngine;

public class Herbivore : Creature
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
                if (creature.CreatureType == CreatureType.Herb)
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
