using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Enemy : Player
{
    public int skillNum;
    public int diceNum;
    List<int> diceList;
    Queue<int> diceQueue;

    private void Start()
    {
        diceList = new List<int>();
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < GameInfo.diceResetArray[i]; j++)
                diceList.Add(i);
        diceQueue = new Queue<int>(diceList.OrderBy(a => Guid.NewGuid()).ToList());
    }
    public override void SetPlayer(int characterNum, int[] skillSet)
    {
        base.SetPlayer(characterNum, skillSet);
    }

    public override Skill UseSkill(int skillNum, int isAI)
    {
        if (isAI > 0) {
            diceNum = diceQueue.Dequeue();
        }
        Debug.Log("[" + skillNum + ", " + diceNum + "]");
        return base.UseSkill(skillNum, diceNum);
    }
}
