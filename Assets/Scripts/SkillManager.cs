using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public int type;
    public int value;
    public int speed;
    public int endurance;
    public int effect;
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
        temp.effect = SkillInfo.effects[characterNum, skillNum, diceNum];

        if(upgrade != null)
        {
            temp.value += upgrade[characterNum, skillNum].value;
            temp.speed += upgrade[characterNum, skillNum].speed;
            temp.endurance += upgrade[characterNum, skillNum].endurance;
            temp.effect += upgrade[characterNum, skillNum].effect;
        }

        return temp;
    }
}

