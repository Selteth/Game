using UnityEngine;

public class SpeedSkill : Skill
{
    private readonly float speedMultiplier = 2f;
    private Movement movement;

    void Awake()
    {
        cooldown = 1f;
        duration = 2f;
        movement = GetComponent<Movement>();
    }
    
    protected override void DoActivate()
    {
        //movement.ChangeRunSpeed(speedMultiplier);
    }
    
    protected override void DoDeactivate()
    {
        //movement.ChangeRunSpeed(1.0f / speedMultiplier);
    }

}
