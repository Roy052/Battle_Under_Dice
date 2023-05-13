using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCM : CanvasManager
{
    public Image skillDescImage;
    public Image skillImage, diceImage;
    public Text nameText ,conditionText, speedValueText, enduranceValueText, skillDescText;
    public Button checkButton;

    public void OnClickSkill(int skillNum) => OnClickSkill(skillNum, -1);
    public void OnClickSkill(int skillNum, int diceNum)
    {
        //Skill Unchecked
        if (skillNum == -1) return;

        int playerNum = Approach.gm.characterNum_player;

        string str = "";
        Skill skill = null;
        if (diceNum == -1)
        {
            str = SkillInfo.skillDescriptionText[playerNum, skillNum, 5];
            skill = Approach.player.skillManager.UseSkill(playerNum, skillNum, 5);
        }
        else
        {
            str = SkillInfo.skillDescriptionText[playerNum, skillNum, diceNum];
            skill = Approach.player.skillManager.UseSkill(playerNum, skillNum, diceNum);
        }

        Debug.Log(playerNum + ", " + skillNum);
        nameText.text = SkillInfo.skillNameText[playerNum, skillNum];
        conditionText.text = SkillDesc.GetSkillCondString(playerNum, skillNum);
        if (diceNum == -1)
        {
            speedValueText.text = "?";
            enduranceValueText.text = "?";
        }
        else
        {
            speedValueText.text = skill.speed.ToString();
            enduranceValueText.text = skill.endurance.ToString();
        }
        skillDescText.text = SkillDesc.GetSkillDescString(str, skill, diceNum == -1 ? true : false);
    }

    public void CheckButtonOn()
    {
        checkButton.gameObject.SetActive(true);
    }
    public void CheckButtonOff()
    {
        checkButton.gameObject.SetActive(false);
    }

    public void SkillDescOn()
    {
        skillDescImage.gameObject.SetActive(true);
    }

    public void SkillDescOff()
    {
        skillDescImage.gameObject.SetActive(false);
    }
}
