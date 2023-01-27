using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public int[] diceArray = new int[6];

    public void DiceReset()
    {
        diceArray = new int[6];

        for (int i = 0; i < 6; i++)
            diceArray[i] = GameInfo.diceResetArray[i];
    }

    public void UseDice(int diceNum)
    {
        if(diceNum > 5 || diceNum < 0 || diceArray[diceNum] <= 0)
        {
            Debug.LogError("UseDice() Error");
            return;
        }
        diceArray[diceNum]--;
    }

    public int TotalDiceValue()
    {
        int sum = 0;
        for (int i = 0; i < diceArray.Length; i++)
            sum += diceArray[i] * (i + 1);

        return sum;
    }
}
