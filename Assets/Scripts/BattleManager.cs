using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameManager gm;

    [SerializeField] Player player;
    [SerializeField] Player enemy;

    public void SetBattle()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player.SetPlayer(gm.characterNum, gm.skillSet);
        enemy.SetPlayer(0, new int[4]{ 0,0,0,0});
    }
}
