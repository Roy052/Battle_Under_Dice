using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extended : MonoBehaviour
{
    public static bool IsSkillConditionFulfilled(bool isPlayer, int skillNum)
    {
        bool result = true;
        for(int i = 0; i < 3; i++)
        {
            if (SkillInfo.condTypes[Approach.player.GetCharacterNum(), skillNum, i] == -1) break;
            if (Approach.player.characterManager.IsConditionFulfilled(
                SkillInfo.condTypes[Approach.player.GetCharacterNum(), skillNum, i],
                SkillInfo.condValues[Approach.player.GetCharacterNum(), skillNum, i]) == false)
            {
                result = false;
                break;
            }
        }
        

        return result;
    }
}
