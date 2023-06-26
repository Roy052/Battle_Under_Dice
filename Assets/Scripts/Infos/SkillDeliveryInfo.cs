using System.Collections.Generic;

public class SkillDeliveryInfo
{
    //The Count of Each Skill Delivery Num Have
    public static int[] skillDeliveryCount = new int [6]
    {
        1,
        1,
        2,
        2,
        1,
        1
    };

    public static int[,,] skillDeliveryInfos = new int[6, 5, 4]
    {
        //IsBuff, Type, Target, Value
        {
            { 0, 0, 0, 1},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
        },
        {
            { 0, 0, 0, 2},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
        },
        {
            { 0, 0, 0, 1},
            { 0, 0, 0, 1},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
        },
        {
            { 0, 0, 0, 1},
            { 0, 0, 0, 1},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
        },
        {
            { 0, 0, 0, 1},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
        },
        {
            { 0, 0, 0, 1},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
        },
    };

    public static string[] buffName = new string[]
    {
        "AddDamage",
        "AddDefense",
        "AddEvades",
        "AddSpeed",
        "AddEndurance",
        "BuffToSkill",
        "EnhancePassive",
    };

    public static string[] debuffName = new string[]
    {
        "ReduceDealtDamage",
        "ReduceHp",
        "ReduceDamage",
        "ReduceSpeed",
        "ReduceDefense",
        "ReduceEvades",
        "ReduceEndurance",
        "DebuffToSkill",
        "AddDealtDamage",
    };

    public static string[] buffDesc = new string[]
    {
        "���� ��, �������� [d]��ŭ �����Ѵ�",
        "��� ��, ��� ��ġ�� [d]��ŭ �����Ѵ�",
        "ȸ�� ��, ȸ�� ��ġ�� [d]��ŭ �����Ѵ�",
        "��ų ��� ��, �ӵ��� [d]��ŭ �����Ѵ�",
        "��ų ��� ��, �γ��� [d]��ŭ �����Ѵ�",
    };

    public static string[] debuffDesc = new string[]
    {
        "���� ��, �������� [d]��ŭ �����Ѵ�",
        "��� ��, ��� ��ġ�� [d]��ŭ �����Ѵ�",
        "ȸ�� ��, ȸ�� ��ġ�� [d]��ŭ �����Ѵ�",
        "��ų ��� ��, �ӵ��� [d]��ŭ �����Ѵ�",
        "��ų ��� ��, �γ��� [d]��ŭ �����Ѵ�",
    };


    public static List<Buff> GetBuff(int id)
    {
        List<Buff> buffs = new List<Buff>();
        
        //No Delivery
        if (id == -1) return buffs;

        for (int i = 0; i < skillDeliveryCount[id]; i++)
        {
            if(skillDeliveryInfos[id,i,0] == 0)
            {
                Buff temp = new Buff();
                temp.buffType = (BuffType)skillDeliveryInfos[id, i, 1];
                temp.target = (TargetType)skillDeliveryInfos[id, i, 2];
                temp.value = (short)skillDeliveryInfos[id, i, 3];

                buffs.Add(temp);
            }
        }

        return buffs;
    }

    public static List<Debuff> GetDebuff(int id)
    {
        List<Debuff> debuffs = new List<Debuff>();

        //No Delivery
        if (id == -1) return debuffs;
        for (int i = 0; i < skillDeliveryCount[id]; i++)
        {
            if (skillDeliveryInfos[id, i, 0] == 1)
            {
                Debuff temp = new Debuff();

                temp.debuffType = (DebuffType)skillDeliveryInfos[id, i, 1];
                temp.target = (TargetType)skillDeliveryInfos[id, i, 2];
                temp.value = (short)skillDeliveryInfos[id, i, 3];

                debuffs.Add(temp);
            }
        }

        return debuffs;
    }
}
