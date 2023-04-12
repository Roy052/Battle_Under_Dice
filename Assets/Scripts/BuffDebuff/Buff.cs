using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    None = -1,
    AddDamage = 0,
    AddSpeed = 1,
    AddDefense = 2,
    AddEvades = 3,
    AddEndurance = 4,
    BuffToSkill = 5,
    EnhancePassive = 6,
    ReduceDealtDamage = 7,

    Others = 10,
}

public class Buff
{
    public string name;
    public BuffType type;
    public GameStatus activateStatus;
    public short value;
    public short lastCount;
    public GameStatus reduceCountStatus;
}