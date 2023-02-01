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

    //Turn
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
    
    //Battle
    int playerDefense = 0, playerEvade = 0;
    int enemyDefense = 0, enemyEvade = 0;

    //GameEnd Message
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
        player.SetPlayer(gm.characterNum_player, gm.skillSet_player);
        enemy.SetPlayer(gm.characterNum_enemy, gm.skillSet_enemy);

        Invoke("BeforeStart", 1);
    }

    public void BeforeStart()
    {
        gameStatus = 0;
        turnNum++;
        RefreshData();
        //Check Game End
        if (CheckGameEnd())
        {
            battleSM.GameEnd(WhoWin());
        }

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

        //Battle
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
        Debug.Log("(" + gm.characterNum_player + ", " + playerSkillNum + ", " + playerDiceNum);
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
        if (playerFirst)
        {
            if (playerSkill.type == 0)
            {
                //Damage Calculate
                int damage = CalculateDamage(playerSkill.value, true);
                enemy.characterManager.ChangeHp(-damage);
            }
            if (playerSkill.type == 1) playerDefense = playerSkill.value;
            if (playerSkill.type == 2) playerEvade = playerSkill.value;

            //Check Game End
            

            if (enemySkill.type == 0)
            {
                //Damage Calculate
                int damage = CalculateDamage(enemySkill.value, false);
                player.characterManager.ChangeHp(-damage);
            }
            if (playerSkill.type == 1) playerDefense = playerSkill.value;
            if (playerSkill.type == 2) playerEvade = playerSkill.value;
        }
        else
        {
            if (enemySkill.type == 0)
            {
                //Damage Calculate
                int damage = CalculateDamage(enemySkill.value, false);
                player.characterManager.ChangeHp(-damage);
            }
            if (enemySkill.type == 1) enemyDefense = enemySkill.value;
            if (enemySkill.type == 2) enemyEvade = enemySkill.value;

            if (playerSkill.type == 0)
            {
                //Damage Calculate
                int damage = CalculateDamage(playerSkill.value, true);
                enemy.characterManager.ChangeHp(-damage);
            }
            if (playerSkill.type == 1) playerDefense = playerSkill.value;
            if (playerSkill.type == 2) playerEvade = playerSkill.value;
        }
    }

    bool CheckGameEnd()
    {
        bool ret = false;
        //hp == 0
        if (player.characterManager.character.hp == 0 
            || enemy.characterManager.character.hp == 0) 
            ret = true;

        //Turn is Over
        if (turnNum == 13) ret = true;

        return ret;
    }

    int WhoWin()
    {
        //0 : Playerwin, 1 : Draw, 2 : Enemy Win
        if (player.characterManager.character.hp == enemy.characterManager.character.hp) return 1;
        else if (player.characterManager.character.hp > enemy.characterManager.character.hp) return 0;
        else return 2;
    }

    int CalculateDamage(int attack, bool isPlayer)
    {
        int damage = 0;

        if (isPlayer)
        {
            if (enemyDefense != 0)
            {
                if (enemyDefense > attack)
                {
                    damage = 0;
                    enemyDefense -= attack;
                }
                else
                {
                    damage = attack - enemyDefense;
                    enemyDefense = 0;
                }
            }
            else if (enemyEvade >= attack) damage = 0;
        }
        else
        {
            if (playerDefense != 0)
            {
                if (playerDefense > attack)
                {
                    damage = 0;
                    playerDefense -= attack;
                }
                else
                {
                    damage = attack - playerDefense;
                    playerDefense = 0;
                }
            }
            else if (playerEvade >= attack) damage = 0;
        }

        return damage;
    }

    void RefreshData()
    {
        playerDefense = 0;
        playerEvade = 0;
        enemyDefense = 0;
        enemyEvade = 0;


    }
}
