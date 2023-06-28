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
            { 0, 100, 0, 1},
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

    public static Dictionary<int, string> buffName = new Dictionary<int, string>()
    {
        {0, "AddDamage" },
        {1, "AddDefense" },
        {2, "AddEvades" },
        {3, "AddSpeed" },
        {4, "AddEndurance" },
        {5, "BuffToSkill" },
        {6, "EnhancePassive" },
        {100, "Anger" },
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

    public static Dictionary<int, string> buffDesc = new Dictionary<int, string>()
    {
        {0,  "���� ��, �������� [d]��ŭ �����Ѵ�" },
        {1, "��� ��, ��� ��ġ�� [d]��ŭ �����Ѵ�" },
        {2, "ȸ�� ��, ȸ�� ��ġ�� [d]��ŭ �����Ѵ�" },
        {3, "��ų ��� ��, �ӵ��� [d]��ŭ �����Ѵ�" },
        {4, "��ų ��� ��, �γ��� [d]��ŭ �����Ѵ�" },

        {100, "���� ��, �������� [d]��ŭ �ӵ��� [d]��ŭ �����Ѵ�." },
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
