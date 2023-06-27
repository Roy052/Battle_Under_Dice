using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceCM : CanvasManager
{
    public Button[] buttons;
    public DiceButton[] diceButtons;
    int current = -1;

    public void Setup(int[] diceArray)
    {
        //DiceAmount
        for (int i = 0; i < 6; i++)
            diceButtons[i].Set(diceArray[i]);
    }

    public void RefreshUI(int[] diceArray)
    {
        //DiceArray
        for (int i = 0; i < 6; i++)
        {
            if (diceArray[i] == 0) buttons[i].enabled = false;
            diceButtons[i].Set(diceArray[i]);
        }

        //Activate
        for (int i = 0; i < diceButtons.Length; i++)
            diceButtons[i].Deactivate();
    }

    public void SelectDiceNum(int num)
    {
        if (current == num)
        {
            diceButtons[num].Deactivate();
            current = -1;
        }
        else
        {
            if (current != -1)
                diceButtons[current].Deactivate();

            diceButtons[num].Activate();
            current = num;
        }

        Approach.battleSM.SelectDiceNum(current);
    }
}
