using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gang : Player
{
    public override void SetPlayer(int characterNum, int[] skillSet)
    {
        base.SetPlayer(characterNum, skillSet);
        buffList.Add(new Buff());
    }
}
