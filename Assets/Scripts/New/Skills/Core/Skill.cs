using System.Collections;
using UnityEngine;

public enum SkillState
{
    Ready, Activated, Cooldown
}

public abstract class Skill : MonoBehaviour, ISkill
{
    protected float cooldown;
    protected float duration;

    private SkillState state;

    public void Activate()
    {
        if (state == SkillState.Ready)
        {
            DoActivate();
            state = SkillState.Activated;
            StartCoroutine("WaitForDuration");
        }
    }

    public void Deactivate()
    {
        if (state == SkillState.Activated)
        {
            Cooldown();
            state = SkillState.Cooldown;
            DoDeactivate();
        }
    }

    protected abstract void DoActivate();
    protected abstract void DoDeactivate();

    private void Cooldown()
    {
        if (state == SkillState.Cooldown)
            StartCoroutine("WaitForCooldown");
    }

    private IEnumerator WaitForDuration()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

    private IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        state = SkillState.Ready;
    }
}