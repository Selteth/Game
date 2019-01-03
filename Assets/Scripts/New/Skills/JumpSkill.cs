using UnityEngine;

public class JumpSkill : Skill
{
    private readonly float jumpTimeMultiplier = 2f;
    private Movement movement;

    void Awake()
    {
        cooldown = 1f;
        duration = 2f;
        movement = GetComponent<Movement>();
    }
    
    protected override void DoActivate()
    {
        //movement.ChangeJumpTime(jumpTimeMultiplier);
    }
    
    protected override void DoDeactivate()
    {
        //movement.ChangeJumpTime(1.0f / jumpTimeMultiplier);
    }

}
