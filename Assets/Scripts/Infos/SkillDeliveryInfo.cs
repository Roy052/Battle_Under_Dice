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

    public static int[,,] skillDeliveryInfos = new int[6, 5, 8]
    {
        //IsBuff, Type, Name, Target, ActStatus, Value, ReduceStatus, Count
        {
            { 0, 0, 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
        },
        {
            { 0, 0, 0, 0, 0, 2, 0, 2 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
        },
        {
            { 1, 0, 0, 1, 0, 2, 0, 2 },
            { 1, 0, 0, 1, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
        },
        {
            { 1, 0, 0, 1, 0, 2, 0, 2 },
            { 1, 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
        },
        {
            { 0, 0, 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
        },
        {
            { 0, 0, 0, 0, 0, 2, 0, 2 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
        },
    };

    public static string[] skillDeliveryName = new string[16]
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
        
        //No Delivery
        if (id == -1) return buffs;

        for (int i = 0; i < skillDeliveryCount[id]; i++)
        {
            if(skillDeliveryInfos[id,i,0] == 0)
            {
                Buff temp = new Buff();
                temp.buffType = (BuffType)skillDeliveryInfos[id, i, 1];
                temp.deliveryName = skillDeliveryName[skillDeliveryInfos[id, i, 2]];
                temp.target = (TargetType)skillDeliveryInfos[id, i, 3];
                temp.activateStatus = (GameStatus)skillDeliveryInfos[id, i, 4];
                temp.value = (short)skillDeliveryInfos[id, i, 5];
                temp.reduceCountStatus = (GameStatus)skillDeliveryInfos[id, i, 6];
                temp.count = (short)skillDeliveryInfos[id, i, 7];

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
                temp.deliveryName = skillDeliveryName[skillDeliveryInfos[id, i, 2]];
                temp.target = (TargetType)skillDeliveryInfos[id, i, 3];
                temp.activateStatus = (GameStatus)skillDeliveryInfos[id, i, 4];
                temp.value = (short)skillDeliveryInfos[id, i, 5];
                temp.reduceCountStatus = (GameStatus)skillDeliveryInfos[id, i, 6];
                temp.count = (short)skillDeliveryInfos[id, i, 7];

                debuffs.Add(temp);
            }
        }

        return debuffs;
    }
}
