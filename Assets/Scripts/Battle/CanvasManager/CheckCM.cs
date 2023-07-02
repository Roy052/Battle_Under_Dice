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

    public Sprite notSetSprite;

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
            str = SkillInfo.skillDescriptionText[playerNum][skillNum, 5];
            skill = Approach.player.skillManager.UseSkill(playerNum, skillNum, 5);
        }
        else
        {
            str = SkillInfo.skillDescriptionText[playerNum][skillNum, diceNum];
            skill = Approach.player.skillManager.UseSkill(playerNum, skillNum, diceNum);
        }

        Debug.Log(playerNum + ", " + skillNum);
        nameText.text = SkillInfo.skillNameText[playerNum][skillNum];
        conditionText.text = Desc.GetSkillCondString(playerNum, skillNum);
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
        skillDescText.text = Desc.GetSkillDescString(str, skill, diceNum == -1 ? true : false);
        skillImage.sprite = Approach.gm.GetSkillSprite(playerNum, skillNum);
        diceImage.sprite = Approach.gm.GetDiceSprite(diceNum);

        if (diceNum == -1)
            CheckButtonOff();
    }

    public void ResetCanvas()
    {
        //skillImage.sprite = notSetSprite;
        //diceImage.sprite = notSetSprite;
        SkillDescOff();
        CheckButtonOff();
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
