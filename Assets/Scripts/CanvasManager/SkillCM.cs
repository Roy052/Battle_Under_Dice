using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCM : CanvasManager
{
    public Button[] buttons;
    public GameObject[] activates;

    public Text[] buttonTexts;
    public Image[] buttonImages;
    int current = -1;

    public void Setup(int characterNum_player, int[] skillSet_player)
    {
        //SkillButtonText
        for (int i = 0; i < 6; i++)
            buttonTexts[i].text = SkillInfo.skillNameText[characterNum_player, skillSet_player[i]];

        //SkillButtonImage
        for (int i = 0; i < 6; i++)
            buttonImages[i].sprite = Approach.gm.GetSkillSprite(characterNum_player, skillSet_player[i]);
    }

    public void RefreshUI()
    {
        Skill[] skillList = null;
        foreach (GameObject activate in activates)
            activate.SetActive(false);
    }

    public void SelectSkillNum(int num)
    {
        if (current == num)
        {
            activates[num].gameObject.SetActive(false);
            current = -1;
        }
        else
        {
            if (current != -1)
                activates[current].gameObject.SetActive(false);
            activates[num].gameObject.SetActive(true);
            current = num;
        }

        Approach.battleSM.SelectSkillNum(current);
    }
}
