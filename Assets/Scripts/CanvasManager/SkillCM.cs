using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCM : CanvasManager
{
    public Button[] buttons;

    public SkillButton[] skillButtons;
    int current = -1;

    public void Setup(int characterNum_player, int[] skillSet_player)
    {
        //SkillButtonText
        for (int i = 0; i < 6; i++)
        {
            skillButtons[i].Set(characterNum_player, skillSet_player[i]);
            skillButtons[i].Deactivate();
        }
    }

    public void RefreshUI()
    {
        Skill[] skillList = null;
        for(int i = 0; i < skillButtons.Length; i++)
        {
            skillButtons[i].Deactivate();
        }
        current = -1;
    }

    public void SelectSkillNum(int num)
    {
        if (current == num)
        {
            skillButtons[num].Deactivate();
            current = -1;
        }
        else
        {
            if (current != -1)
                skillButtons[current].Deactivate();
            skillButtons[num].Activate();
            current = num;
        }

        Approach.battleSM.SelectSkillNum(current);
    }
}
