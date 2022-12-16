using UnityEngine;

public class Herbivore : Creature
{
    public override void DoAction()
    {
        base.DoAction();

        if (CurrentBreedTime <= 0f)
        {
            if (JoyPercent > 60 && Hunger > 50)
            {
                IsReadyToBreed = true;
                Creature creature = FindNearbyCreature();

                if (creature)
                {
                    if (creature.CreatureType == CreatureType && creature.ID != ID)
                    {
                        if (FindedCreature == null)
                        {
                            SetTarget(creature);
                        }
                    }
                }
                else
                {
                    CreateMovementPoint();
                    MoveTo();
                }
            }
        }

        if (Hunger <= 50f)
        {
            IsHunger = true;
            Creature creature = FindNearbyCreature();

            if (FindedCreature == null)
            {
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
        }
        else
        {
            IsHunger = false;
        }
    }
}
