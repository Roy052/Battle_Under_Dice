using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    None = -1,
    AddHp = 0,
    AddDamage = 1,
    AddSpeed = 2,
    AddDefense = 3,
    AddEvades = 4,
    AddEndurance = 5,
    BuffToSkill = 6,
    EnhancePassive = 7,
    ReduceEnemyHp = 8,
    ReduceDealtDamage = 9,

    Others = 10,
}

public class Buff
{
    public string buffName;
    public BuffType buffType;
    public GameStatus activateStatus;
    public short buffValue;
    public short buffTurnCount;
}
