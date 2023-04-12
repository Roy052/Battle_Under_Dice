using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour
{
    IEnumerator Start()
    {
        //Setup BattleManager
        while(Approach.battleManager == null)
            yield return null;

        Approach.battleManager.SetBattle();

        //Setup BattleSM
        while (Approach.battleSM == null)
            yield return null;

        Approach.battleSM.SetUp();

        while(!(Approach.battleManager.setupEnd == true && Approach.battleSM.setupEnd == true))
            yield return null;

        Approach.battleManager.GameStart();
    }
}
