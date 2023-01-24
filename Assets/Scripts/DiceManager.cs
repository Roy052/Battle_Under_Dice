using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public int[] diceArray = new int[6];

    GameInfo gameInfo = new GameInfo();
    public void DiceReset()
    {
        diceArray = new int[6];

        for (int i = 0; i < 6; i++)
            diceArray[i] = gameInfo.diceResetArray[i];
    }

    public void UseDice(int diceNum)
    {
        if(diceArray[diceNum] <= 0)
        {
            Debug.LogError("UseDice() Error");
            return;
        }
        diceArray[diceNum]--;
    }


}
