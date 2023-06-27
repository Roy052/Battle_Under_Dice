using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebuffType
{
    None = -1,
    ReduceHp = 0,
    ReduceDamage = 1,
    ReduceSpeed = 2,
    ReduceDefense = 3,
    ReduceEvades = 4,
    ReduceEndurance = 5,
    DebuffToSkill = 6,
    AddDealtDamage = 7,

    Others = 10,
}

public class Debuff : SkillDelivery
{
    public DebuffType debuffType;
}
