using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour
{
    IEnumerator Start()
    {
        //Setup BattleManager
        while(Access.battleManager == null)
            yield return null;

        Access.battleManager.SetBattle();

        //Setup BattleSM
        while (Access.battleSM == null)
            yield return null;

        Access.battleSM.SetUp();

        while(!(Access.battleManager.setupEnd == true && Access.battleSM.setupEnd == true))
        {
            Access.battleManager.GameStart();
        }
    }
}
