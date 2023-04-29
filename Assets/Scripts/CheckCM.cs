using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCM : MonoBehaviour
{
    public Text skillDescText;

    public void OnClickSkill(int skillNum) => OnClickSkill(skillNum, 0);
    public void OnClickSkill(int skillNum, int diceNum)
    {
        //Skill Unchecked
        if (skillNum == -1) skillDescText.text = "";

        int playerNum = Approach.gm.characterNum_player;
        string str = SkillInfo.skillDescriptionText[playerNum, skillNum, diceNum];
        Skill skill = Approach.player.skillManager.UseSkill(playerNum, skillNum, diceNum);
        SkillDesc.GetSkillDescString(str, skill, diceNum);
    }
}
