using System.Collections.Generic;
using UnityEngine;

public class SkillManagerOld : MonoBehaviour {

    // Player skills
    private readonly IList<ISkillOld> skills = new List<ISkillOld>();
    // Last skill used by player
    private ISkillOld lastSkill;

    void Awake()
    {
        AddSkill<TeleportationSkill>();
        AddSkill<SpeedSkillOld>();
        AddSkill<MechanismControlSkill>();
    }

    // Debug only
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ActivateSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ActivateSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ActivateSkill(2);

        if (Input.GetButtonDown("Fire2") && lastSkill != null)
            lastSkill.CancelCast();
    }
    // End debug only

    // Adds new skill of specified type
    public void AddSkill<SkillType>() where SkillType : Component
    {
        gameObject.AddComponent<SkillType>();
        skills.Add(GetComponent<SkillType>() as ISkillOld);

        // Then show new skill in UI...
    }

    // Activates skill with specified index
    public void ActivateSkill(int index)
    {
        if (index >= 0 && index < skills.Count)
        {
            if (lastSkill != null)
                lastSkill.CancelCast();

            lastSkill = skills[index];
            lastSkill.Activate();
        }
    }
}
