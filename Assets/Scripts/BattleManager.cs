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
    Dictionary<int, string> stepMethod = new Dictionary<int, string>{
        { 0, "TurnStart" },
        { 1, "InTurn" },
        { 2, "Check" },
        { 3, "EndTurn" },
        { 4, "BeforeStart" }
    };

    bool timeFlow = false;
    float time = 0;
    private void FixedUpdate()
    {
        if (time > 1f && battleSM.uiEnd)
        {
            time = 0;
            RunNextStep();
            Debug.Log(battleSM.uiEnd);
        }
        if (timeFlow)
            time += Time.fixedDeltaTime;
    }
    public void SetBattle()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player.SetPlayer(gm.characterNum, gm.skillSet);
        enemy.SetPlayer(0, new int[4]{ 0,0,0,0});

        Invoke("BeforeStart", 1);
    }

    public void BeforeStart()
    {
        gameStatus = 0;
        turnNum++;
        StartCoroutine(battleSM.BeforeStart());
        timeFlow = true;
    }

    public void TurnStart()
    {
        gameStatus = 1;
        StartCoroutine(battleSM.TurnStart());

        //Game End Check
    }

    public void InTurn()
    {
        gameStatus = 2;
        StartCoroutine( battleSM.InTurn());
        timeFlow = false;
    }

    public void Check()
    {
        gameStatus = 3;
        StartCoroutine(battleSM.Check());
        timeFlow = false;
        BattleProgress();
        timeFlow = true;
    }

    public void EndTurn()
    {
        gameStatus = 4;
        StartCoroutine(battleSM.EndTurn());
    }

    public void RunNextStep()
    {
        Invoke(stepMethod[gameStatus], 0);
    }

    int playerSkillNum = -1, playerDiceNum = -1;
    int enemySkillNum = -1, enemyDiceNum = -1;
    public void SelectSkillNum(int num)
    {
        //Skill Cancel
        if(playerSkillNum == num)
        {
            playerSkillNum = -1;
            battleSM.DiceCanvasOff();
            battleSM.CheckCanvasOff();
            return;
        }

        playerSkillNum = num;
        battleSM.DiceCanvasOn();
    }

    public void SelectDiceNum(int num)
    {
        if(playerDiceNum == num)
        {
            playerDiceNum = -1;
            battleSM.CheckCanvasOff();
            return;
        }

        playerDiceNum = num;
        battleSM.CheckCanvasOn();
    }

    public void CheckSkillAndDice()
    {
        timeFlow = true;
        StartCoroutine(battleSM.InTurnEnd());
    }

    public void BattleProgress()
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
