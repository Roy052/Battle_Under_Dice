using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDesc : MonoBehaviour
{
    const string ConditionReplace = "[c]";
    const string SkillReplace = "[s]";
    const string DeliveryReplace = "[d]";
    const string AddEffectText = "And ";

    public static string GetSkillDescString(string str, Skill skill, int diceNum)
    {
        string result = str;

        List<int> conditionValues = new List<int>();
        List<int> skillValues = new List<int>();
        List<int> deliveryValues = new List<int>();

        //Condition Value Setup
        foreach (var condition in skill.conditions)
        {
            if (condition.type == ConditionType.Always)
                continue;

            if (condition.type != ConditionType.FlameCount)
            {
                conditionValues.Add(condition.value);
            }
            else
            {
                int value = condition.value;
                if (value < 0) value = Mathf.Abs(value);

                conditionValues.Add(value);
            }
        }

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

        //Condition Str Setup
        int conditionCount = conditionValues.Count;
        for (int i = 0; i < conditionCount; i++)
        {
            int valueIndex = result.IndexOf(ConditionReplace);
            if (valueIndex < 0) continue;

            string strCondition = $"<color=#7AFFFF>{conditionValues[0]}</color>";
            result = result.Replace(SkillReplace, strCondition);

            conditionValues.RemoveAt(0);
        }

        //Skill Str Setup
        int skillCount = skillValues.Count;
        for (int i = 0; i < skillCount; i++)
        {
            if (result.Contains(SkillReplace) == false)
                continue;

            string strTurn = $"<color=#7AFFFF>{skillValues[0]}</color>";
            result = result.Replace(SkillReplace, strTurn);

            skillValues.RemoveAt(0);
        }

        //Delivery Str Setup
        int deliveryCount = deliveryValues.Count;
        for (int i = 0; i < deliveryCount; i++)
        {
            if (result.Contains(DeliveryReplace) == false)
                continue;

            string strTurn = $"<color=#7AFFFF>{deliveryValues[0]}</color>";
            result = result.Replace(DeliveryReplace, strTurn);

            deliveryValues.RemoveAt(0);
        }

        return result;
    }
}
