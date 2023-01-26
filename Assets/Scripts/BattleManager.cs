using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameManager gm;

    [SerializeField] BattleSM battleSM;
    [SerializeField] Player player, enemy;
    [SerializeField] DiceManager diceManager;

    int gameStatus = 0; //0 : before start(Clean up), 1 : turn start(Effect), 2 : In turn, 3 : Check, 4 : End turn(Effect)

    public int turnNum = 0;
    public void SetBattle()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player.SetPlayer(gm.characterNum, gm.skillSet);
        enemy.SetPlayer(0, new int[4]{ 0,0,0,0});
    }

    public void BeforeStart()
    {
        gameStatus = 0;
        turnNum++;
        battleSM.BeforeStart();
    }

    public void TurnStart()
    {
        gameStatus = 1;
        battleSM.TurnStart();

        //Game End Check
    }

    public void InTurn()
    {
        gameStatus = 2;
        battleSM.InTurn();
    }

    public void Check()
    {
        gameStatus = 3;
        battleSM.Check();
    }

    public void EndTurn()
    {
        gameStatus = 4;
        battleSM.EndTurn();
    }

    int playerSkillNum, playerDiceNum;
    int enemySkillNum, enemyDiceNum;
    public void SelectSkillNum(int num)
    {
        playerSkillNum = num;
    }

    public void SelectDiceNum(int num)
    {
        playerDiceNum = num;
    }

    public void CheckSkillAndDice()
    {
        Skill playerSkill = player.UseSkill(playerSkillNum, playerDiceNum);

        //Random Mehod -> AI Method -> Network Method
        enemySkillNum = Random.Range(0, 4);
        enemyDiceNum = Random.Range(1, 7);
        Skill enemySkill = enemy.UseSkill(enemySkillNum, enemyDiceNum);


        //Calculate Battle Result
        bool playerFirst;
        int playerSpeed = player.characterManager.character.speed + playerSkill.speed;
        int enemySpeed = enemy.characterManager.character.speed + enemySkill.speed;

        //Who First
        // 1. Higher Speed
        if (playerSpeed != enemySpeed) playerFirst = (playerSpeed > enemySpeed);
        else
        {
            // 2. Lower Dice Num
            if (playerDiceNum != enemyDiceNum) playerFirst = playerDiceNum > enemyDiceNum;
            else
            {
                // 3. Lower hp
                if (player.characterManager.character.hp != enemy.characterManager.character.hp)
                    playerFirst = player.characterManager.character.hp < enemy.characterManager.character.hp;
                else // 4. Lower Dice Total Value
                    playerFirst = player.diceManager.TotalDiceValue() < enemy.diceManager.TotalDiceValue();
            }
        }


        int playerEndurance = player.characterManager.character.endurance + playerSkill.endurance;
        int enemyEndurance = enemy.characterManager.character.endurance + enemySkill.endurance;

        //Skill Type = 0 : Attack, 1 : Defense, 2 : Evade, 3 : Buff, 4 : Debuff
        int defense = 0, evade = 0;
        if (playerFirst)
        {
            if (playerSkill.type == 0) enemy.characterManager.ChangeHp(playerSkill.value);
            if (playerSkill.type == 1) defense = playerSkill.value;
            if (playerSkill.type == 2) evade = playerSkill.value;

            //Check Game End
        }
    }
}
