using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JourneyManager : MonoBehaviour
{
    [SerializeField] BattleRecord battleRecord;

    Skill[] upgrade = new Skill[6];
    List<int> accessoryList = new List<int>();
    private void Awake()
    {
        Approach.journeyManager = this;
    }

    private void OnDestroy()
    {
        Approach.journeyManager = null;
    }

    public void SaveJourneyData()
    {

    }

    public void LoadJourneyData()
    {

    }

    public IEnumerator JourneyStart()
    {
        battleRecord.Set();
        Approach.gm.SceneLoad_Battle();

        while (Approach.battleSM == null)
            yield return null;

        Approach.battleManager.SetUpgrade(upgrade);
    }

    public void AddAccessory(int num)
    {
        if (accessoryList.Contains(num))
        {
            Debug.LogError("Already Exist Accessory");
            return;
        }
        accessoryList.Add(num);     
    }

    public List<int> LoadAccessory()
    {
        return accessoryList;
    }
}
