using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceCM : CanvasManager
{
    public Button[] buttons;
    public GameObject[] activates;

    public Text[] amountTexts;
    int current = -1;

    public void Setup(int[] diceArray)
    {
        //DiceAmount
        for (int i = 0; i < 6; i++)
            amountTexts[i].text = diceArray[i].ToString();
    }

    public void RefreshUI(int[] diceArray)
    {
        //DiceArray
        for (int i = 0; i < 6; i++)
        {
            if (diceArray[i] == 0) buttons[i].enabled = false;
            amountTexts[i].text = diceArray[i].ToString();
        }

        //Activate
        foreach (GameObject activate in activates)
            activate.SetActive(false);
    }

    public void SelectDiceNum(int num)
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

        Approach.battleSM.SelectDiceNum(current);
    }
}
