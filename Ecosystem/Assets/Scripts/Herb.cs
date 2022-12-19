using UnityEngine;

public class Herb : Creature
{
    public void Update()
    {
        DoAction();
    }

    public override void DoAction()
    {
        if (Health <= 0f)
        {
            Kill();
        }
    }

    public override void Kill()
    {
        Health = 0f;
        Agent.enabled = false;
        gameObject.SetActive(false);
    }
}