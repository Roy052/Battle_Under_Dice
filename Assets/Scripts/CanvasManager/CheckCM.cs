using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCM : MonoBehaviour
{
    public Image skillImage, diceImage;
    public Text conditionText, speedValueText, enduranceValueText, skillDescText;

    public void OnClickSkill(int skillNum) => OnClickSkill(skillNum, 0);
    public void OnClickSkill(int skillNum, int diceNum)
    {
        //Skill Unchecked
        if (skillNum == -1) skillDescText.text = "";

        int playerNum = Approach.gm.characterNum_player;
        string str = SkillInfo.skillDescriptionText[playerNum, skillNum, diceNum];
        Skill skill = Approach.player.skillManager.UseSkill(playerNum, skillNum, diceNum);

        conditionText.text = SkillDesc.GetSkillCondString(playerNum, skillNum);
        speedValueText.text = skill.speed.ToString();
        enduranceValueText.text = skill.endurance.ToString();
        skillDescText.text = SkillDesc.GetSkillDescString(str, skill, diceNum);
    }
}
