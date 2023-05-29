using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Condition
{
    public ConditionType type;
    public int value;
}

public class Skill
{
    public int type; //0 : Attack, 1 : Defense, 2 : Evade, 3 : Buff, 4 : Debuff
    public int value;
    public int speed;
    public int endurance;
    public List<Buff> skillBuffs = new List<Buff>();
    public List<Debuff> skillDebuffs = new List<Debuff>();

    public List<Condition> conditions;
}

public class SkillManager : MonoBehaviour
{
    Skill[,] upgrade;

    public void SetUpgrade(Skill[,] upgrade)
    {
        this.upgrade = upgrade;
    }
    public Skill UseSkill(int characterNum, int skillNum, int diceNum)
    {
        Skill temp = new Skill();
        temp.type = SkillInfo.types[characterNum, skillNum];
        temp.value = SkillInfo.values[characterNum, skillNum, diceNum];
        temp.speed = SkillInfo.speeds[characterNum, skillNum, diceNum];
        temp.endurance = SkillInfo.endurances[characterNum, skillNum, diceNum];

        //Add Buff and Debuff
        List<Buff> buffs = SkillDeliveryInfo.GetBuff(SkillInfo.skillDelivery[characterNum, skillNum, diceNum]);
        foreach(Buff buff in buffs)
        {
            temp.skillBuffs.Add(buff);
        }

        List<Debuff> debuffs = SkillDeliveryInfo.GetDebuff(SkillInfo.skillDelivery[characterNum, skillNum, diceNum]);
        foreach (Debuff debuff in debuffs)
        {
            temp.skillDebuffs.Add(debuff);
        }

        //Add Conditions
        

        //Upgrade
        if (upgrade != null)
        {
            temp.value += upgrade[characterNum, skillNum].value;
            temp.speed += upgrade[characterNum, skillNum].speed;
            temp.endurance += upgrade[characterNum, skillNum].endurance;
            temp.skillBuffs.AddRange(upgrade[characterNum, skillNum].skillBuffs);
            temp.skillDebuffs.AddRange(upgrade[characterNum, skillNum].skillDebuffs);
        }

        return temp;
    }
}

