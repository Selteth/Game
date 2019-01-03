using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private readonly IList<ISkill> skills = new List<ISkill>(3);

    void Awake()
    {
        AddSkill<SpeedSkill>();
        AddSkill<JumpSkill>();
        AddSkill<GhostSkill>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ActivateSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ActivateSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ActivateSkill(2);
    }
    
    public void AddSkill<SkillType>() where SkillType : Component, ISkill
    {
        gameObject.AddComponent<SkillType>();
        skills.Add(GetComponent<SkillType>());
    }
    
    public void ActivateSkill(int index)
    {
        if (index >= 0 && index < skills.Count)
            skills[index].Activate();
    }
}
