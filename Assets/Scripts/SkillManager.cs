using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public int type;
    public int value;
    public int speed;
    public int stiff;
    public int effect;
}
public class SkillManager : MonoBehaviour
{
    Skill[,] upgrade;

    public void SetUpgrade(Skill[,] upgrade)
    {
        this.upgrade = upgrade;
    }
    public Skill UseSkill(int characterNum, int skillNum)
    {
        Skill temp = new Skill();
        temp.type = SkillInfo.types[characterNum, skillNum];
        temp.value = SkillInfo.values[characterNum, skillNum];
        temp.speed = SkillInfo.speeds[characterNum, skillNum];
        temp.stiff = SkillInfo.stiffs[characterNum, skillNum];
        temp.effect = SkillInfo.effects[characterNum, skillNum];

        if(upgrade != null)
        {
            temp.value += upgrade[characterNum, skillNum].value;
            temp.speed += upgrade[characterNum, skillNum].speed;
            temp.stiff += upgrade[characterNum, skillNum].stiff;
            temp.effect += upgrade[characterNum, skillNum].effect;
        }

        return temp;
    }
}

