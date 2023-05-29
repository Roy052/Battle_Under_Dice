using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDesc : MonoBehaviour
{
    const string ConditionReplace = "[c]";
    const string SkillReplace = "[s]";
    const string DeliveryReplace = "[d]";
    const string AddEffectText = "And ";

    public static string GetSkillDescString(string str, Skill skill, bool noDiceNum = false)
    {
        string result = str;

        List<int> skillValues = new List<int>();
        List<int> deliveryValues = new List<int>();

        //Skill Value Setup
        skillValues.Add(skill.value);

        //Buff Value Setup
        foreach (var buff in skill.skillBuffs)
        {
            if (buff.count != 999)
            {
                deliveryValues.Add(buff.count);
            }

            deliveryValues.Add(buff.value);
        }

        //DeBuff Value Setup
        foreach (var debuff in skill.skillDebuffs)
        {
            if (debuff.count != 999)
            {
                deliveryValues.Add(debuff.count);
            }

            deliveryValues.Add(debuff.value);
        }

        //Skill Str Setup
        int skillCount = skillValues.Count;
        for (int i = 0; i < skillCount; i++)
        {
            if (result.Contains(SkillReplace) == false)
                continue;
            
            string strTurn = $"<color=#690000>{skillValues[0]}</color>";
            if(noDiceNum)
                strTurn = $"<color=#690000>{'?'}</color>";
            result = result.Replace(SkillReplace, strTurn);

            skillValues.RemoveAt(0);
        }

        //Delivery Str Setup
        int deliveryCount = deliveryValues.Count;
        for (int i = 0; i < deliveryCount; i++)
        {
            if (result.Contains(DeliveryReplace) == false)
                continue;

            string strTurn = $"<color=#690000>{deliveryValues[0]}</color>";
            if (noDiceNum)
                strTurn = $"<color=#690000>{'?'}</color>";
            result = result.Replace(DeliveryReplace, strTurn);

            deliveryValues.RemoveAt(0);
        }

        return result;
    }

    public static string GetSkillCondString(int playerNum, int skillNum)
    {
        string result = "";

        //Condition
        for (int i = 0; i < 3; i++)
        {
            int conditionNum = SkillInfo.condTypes[playerNum, skillNum, i];
            if (conditionNum == -1) break;
            result += SkillInfo.skillCondTypeText[conditionNum];

            if (result.Contains(ConditionReplace) == false)
                continue;

            string strTurn = $"<color=#690000>{SkillInfo.condValues[playerNum, skillNum, i]}</color>";
            result = result.Replace(ConditionReplace, strTurn);

            result += ", ";
        }

        return result;
    }
}
