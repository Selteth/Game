using UnityEngine;
using System.Collections;

public class SpeedSkillOld : SkillOld
{
    // Value to multiply force added to the player while moving by
    private readonly float speedMultiplier = 2f;
    // Skill duration
    private float duration;
    // Player movement script
    private MovementControl movement;

    new void Awake()
    {
        base.Awake();
        cooldown = 1f; // 1 second for debug only
        duration = 15f;
        movement = GetComponent<MovementControl>();
    }

    // Increases player speed
    protected override void DoActivate()
    {
        movement.ChangeSpeed(speedMultiplier);
        state = SkillStateOld.Activated;
        StartCoroutine("SlowDown");
    }

    // Decreases player speed
    protected override void DoDeactivate()
    {
        movement.ChangeSpeed(1.0f / speedMultiplier);
    }

    protected override void Interrupt()
    {
        DoDeactivate();
    }

    // Slows player back to his speed after skill is over
    private IEnumerator SlowDown()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

}
