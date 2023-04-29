using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDeliveryInfo
{
    public static int[] skillDeliveryCount = new int [5]
    {
        1,
        1,
        2,
        2,
        1
    };

    public static int[,,] skillDeliveryInfos = new int[5, 5, 7]
    {
        //IsBuff, Type, Name, ActStatus, Value, ReduceStatus, Count
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 }
        },
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 }
        },
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 }
        },
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 }
        },
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 }
        }
    };

    public string[] skillDeliveryName = new string[16]
    {
        "AddDamage",
        "AddSpeed",
        "AddDefense",
        "AddEvades",
        "AddEndurance",
        "BuffToSkill",
        "EnhancePassive",
        "ReduceDealtDamage",
        "ReduceHp",
        "ReduceDamage",
        "ReduceSpeed",
        "ReduceDefense",
        "ReduceEvades",
        "ReduceEndurance",
        "DebuffToSkill",
        "AddDealtDamage"
    };

    public static List<Buff> GetBuff(int id)
    {
        List<Buff> buffs = new List<Buff>();

        for(int i = 0; i < skillDeliveryCount[id]; i++)
        {
            SkillDelivery temp;
            if(skillDeliveryInfos[id,i,0] == 0)
            {
                temp = new Buff();

            }
            else
            {
                temp = new Debuff();

            }

        }

        return buffs;
    }

    public static List<Debuff> GetDebuff(int id)
    {
        List<Debuff> debuffs = new List<Debuff>();

        for (int i = 0; i < skillDeliveryCount[id]; i++)
        {
            SkillDelivery temp;
            if (skillDeliveryInfos[id, i, 0] == 0)
            {
                temp = new Buff();

            }
            else
            {
                temp = new Debuff();

            }

        }

        return debuffs;
    }
}
