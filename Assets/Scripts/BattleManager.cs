using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    NotStarted = -1,
    BeforeStart = 0,
    TurnStart = 1,
    InTurn = 2,
    Check = 3,
    Battle = 4,
    EndTurn = 5
}

public class BattleManager : MonoBehaviour
{
    GameManager gm;

    [SerializeField] BattleSM battleSM;
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    [SerializeField] AnimationManager animationManager;

    //-1 : NotStarted, 0 : before start(Clean up), 1 : turn start(Effect), 2 : In turn, 3 : Check, 4 : Battle, 5 : End turn(Effect)
    public GameStatus gameStatus = GameStatus.NotStarted; 


    //Turn
    public int turnNum = 0;
    Dictionary<int, string> stepMethod = new Dictionary<int, string>{
        { 0, "TurnStart" },
        { 1, "InTurn" },
        { 2, "Check" },
        { 3, "Battle" },
        { 4, "EndTurn" },
        { 5, "BeforeStart" }
    };

    bool timeFlow = false;
    float time = 0;
    //Skill & Dice
    int playerSkillNum = -1, playerDiceNum = -1;
    int enemySkillNum = -1, enemyDiceNum = -1;

    //Battle
    public int playerDefense = 0, playerEvade = 0;
    public int enemyDefense = 0, enemyEvade = 0;
    bool playerStunned = false, enemyStunned = false;

    //EnemyStatus
    int isAI = 1;

    public bool setupEnd = false;
    private void Awake()
    {
        Approach.battleManager = this;
    }

    private void FixedUpdate()
    {
        if (setupEnd)
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
    }
    public void SetBattle()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player.SetPlayer(gm.characterNum_player, gm.skillSet_player);
        if (enemy == null) Debug.Log("a");
        enemy.SetPlayer(gm.characterNum_enemy, gm.skillSet_enemy);
        isAI = gm.isAI;

        animationManager.AnimationSet(gm.characterNum_player, gm.characterNum_enemy);

        setupEnd = true;
    }

    public void GameStart()
    {
        Invoke("BeforeStart", 1);
    }

    public void BeforeStart()
    {
        gameStatus = GameStatus.BeforeStart;
        turnNum++;
        RefreshData();
        StartCoroutine(battleSM.BeforeStart());

        //Defense Evade Off
        battleSM.DefenseEvadeOff(false);
        battleSM.DefenseEvadeOff(true);
        timeFlow = true;
    }

    public void TurnStart()
    {
        gameStatus = GameStatus.TurnStart;
        StartCoroutine(battleSM.TurnStart());

        //Game End Check
        //Check Game End
        if (CheckGameEnd())
        {
            battleSM.GameEnd(WhoWin());
            GameEnd();
        }
    }

    //InTurn -> battleSM.InTurn -> Waiting -> CheckSkillAndDice -> BattleSM.InTurnEnd
    public void InTurn()
    {
        gameStatus = GameStatus.InTurn;
        StartCoroutine( battleSM.InTurn());
        timeFlow = false;
    }

    public void Check()
    {
        gameStatus = GameStatus.Check;
        StartCoroutine(battleSM.Check());

        //Reveal
        RevealSkillAndDice();
    }

    public void Battle()
    {
        gameStatus = GameStatus.Battle;
        StartCoroutine(battleSM.Battle());
        timeFlow = false;

        //Battle
        StartCoroutine(BattleProgress());
        timeFlow = true;
    }

    public void EndTurn()
    {
        gameStatus = GameStatus.EndTurn;
        StartCoroutine(battleSM.EndTurn());

        //Check Game End
        if (CheckGameEnd())
        {
            battleSM.GameEnd(WhoWin());
            GameEnd();
        }
    }

    public void RunNextStep()
    {
        Invoke(stepMethod[(int)gameStatus], 0);
    }

    
    public void SelectSkillNum(int num)
    {
        //Skill Cancel
        if(playerSkillNum == num)
        {
            playerSkillNum = -1;
            return;
        }

        playerSkillNum = num;
    }

    public void SelectDiceNum(int num)
    {
        if(playerDiceNum == num)
        {
            playerDiceNum = -1;
            return;
        }

        playerDiceNum = num;
    }

    public void CheckSkillAndDice()
    {
        timeFlow = true;
        StartCoroutine(battleSM.InTurnEnd());
        Debug.Log("(" + gm.characterNum_player + ", " + playerSkillNum + ", " + playerDiceNum + ")");
    }

    public void RevealSkillAndDice()
    {
        //Random Mehod -> AI Method -> Network Method
        enemySkillNum = Random.Range(0, 4);

        //Have To Fix
        enemyDiceNum = isAI;

        int enemyCurrentDice = enemy.CurrentDice();

        battleSM.RevealSkillAndDice(playerSkillNum, playerDiceNum, enemySkillNum, enemyCurrentDice);
    }

    public void Battle_PlayerTurn(Skill playerSkill, int enemyEndurance)
    {
        int beforeHp = enemy.characterManager.character.hp;

        //Use Player Skill Motion
        StartCoroutine(animationManager.AnimationOn(true, 
            SkillInfo.skillNameText[gm.characterNum_player, gm.skillSet_player[playerSkillNum]]));
        StartCoroutine(battleSM.IntenseBattleScreen());

        //Attack
        if (playerSkill.type == 0)
        {
            int tempEnemyDefense = enemyDefense;

            //Damage Calculate
            int damage = CalculateDamage(playerSkill.value, true);
            Debug.Log("Damage : " + damage);
            enemy.characterManager.ChangeHp(-damage);

            //TakeDamage(isPlayer, shieldBroke, shieldDamage, damage);
            if (tempEnemyDefense > 0)
            {
                //Shield = Damage
                if (damage == 0 && tempEnemyDefense == playerSkill.value)
                {
                    battleSM.TakeDamage(false, true, playerSkill.value, 0);
                }
                //Shield > Damage
                else if (damage == 0)
                {
                    battleSM.TakeDamage(false, false, playerSkill.value, 0);
                }
                //Damage > Shield
                else
                {
                    battleSM.TakeDamage(false, true, tempEnemyDefense, damage);
                }
            }
            else
                battleSM.TakeDamage(false, false, 0, damage);
        }
        //Defense
        else if (playerSkill.type == 1)
        {
            playerDefense = playerSkill.value;
            battleSM.DefenseEvadeOn(true, 0, playerDefense);
        }
        //Evade
        else if (playerSkill.type == 2)
        {
            playerEvade = playerSkill.value;
            battleSM.DefenseEvadeOn(true, 1, playerEvade);
        }

        //Add SkillDeliveries (Need Target)
        for (int i = 0; i < playerSkill.skillBuffs.Count; i++)
            battleSM.AddSkillDelivery(true, playerSkill.skillBuffs[i]);
        for (int i = 0; i < playerSkill.skillDebuffs.Count; i++)
            battleSM.AddSkillDelivery(false, playerSkill.skillDebuffs[i]);

        //Refresh UI
        battleSM.RefreshUI();


        //Check Game End
        if (CheckGameEnd())
        {
            battleSM.GameEnd(WhoWin());
            GameEnd();
        }

        int afterHp = enemy.characterManager.character.hp;

        //Check Endurance
        if (beforeHp - afterHp > enemyEndurance)
        {
            enemyStunned = true;
            battleSM.CharacterStunned(false);
            Debug.Log("Enemy Stunned");
        }
    }

    public void Battle_EnemyTurn(Skill enemySkill, int playerEndurance)
    {
        int beforeHp = player.characterManager.character.hp;

        //Use Enemy Skill Motion
        StartCoroutine(animationManager.AnimationOn(false,
            SkillInfo.skillNameText[gm.characterNum_enemy, gm.skillSet_player[enemySkillNum]]));
        StartCoroutine(battleSM.IntenseBattleScreen());

        if (!enemyStunned)
        {
            if (enemySkill.type == 0)
            {
                int tempPlayerDefense = playerDefense;

                //Damage Calculate
                int damage = CalculateDamage(enemySkill.value, false);
                player.characterManager.ChangeHp(-damage);

                //TakeDamage(isPlayer, shieldBroke, shieldDamage, damage);
                if (tempPlayerDefense > 0)
                {
                    //Shield = Damage
                    if (damage == 0 && tempPlayerDefense == enemySkill.value)
                        battleSM.TakeDamage(true, true, enemySkill.value, damage);
                    //Shield > Damage
                    else if (damage == 0)
                        battleSM.TakeDamage(true, false, enemySkill.value, damage);
                    //Damage > Shield
                    else
                        battleSM.TakeDamage(true, true, tempPlayerDefense, damage);
                }
                else
                    battleSM.TakeDamage(true, false, 0, damage);
            }
            if (enemySkill.type == 1)
            {
                enemyDefense = enemySkill.value;
                battleSM.DefenseEvadeOn(false, 0, enemyDefense);
            }
            if (enemySkill.type == 2)
            {
                enemyEvade = enemySkill.value;
                battleSM.DefenseEvadeOn(false, 1, enemyEvade);
            }

            //Damage UI
            battleSM.RefreshUI();

            //Check Game End
            if (CheckGameEnd())
            {
                battleSM.GameEnd(WhoWin());
                GameEnd();
            }
        }

        int afterHp = player.characterManager.character.hp;

        if (beforeHp - afterHp > playerEndurance)
        {
            playerStunned = true;
            battleSM.CharacterStunned(true);
        }
    }

    public IEnumerator BattleProgress()
    {
        Skill playerSkill = player.UseSkill(playerSkillNum, playerDiceNum);
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
            if (!playerStunned)
            {
                Battle_PlayerTurn(playerSkill, enemyEndurance);
            }

            yield return new WaitForSeconds(4);

            if (!enemyStunned)
            {
                Battle_EnemyTurn(enemySkill, playerEndurance);   
            }
        }
        else
        {
            if (!enemyStunned)
            {
                Battle_EnemyTurn(enemySkill, playerEndurance);
            }

            yield return new WaitForSeconds(4);

            if (!playerStunned)
            {
                Battle_PlayerTurn(playerSkill, enemyEndurance);
            }
        }

        

        timeFlow = true;
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

    void GameEnd()
    {
        time = 0;
        timeFlow = false;
    }

    int CalculateDamage(int attack, bool isPlayer)
    {
        int damage = 0;

        //Attacker is Player
        if (isPlayer)
        {
            //Shield Exist
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
            //No Shield
            else
            {
                damage = attack;
            }
            
            //Eavade Success
            if (enemyEvade >= attack) damage = 0;
        }
        //Attacker is Enemy
        else
        {
            //Shield Exist
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
            //No Shield
            else
            {
                damage = attack;
            }
            
            //Evade Success
            if (playerEvade >= attack) damage = 0;
        }

        return damage;
    }

    void RefreshData()
    {
        //Skill & Dice
        playerSkillNum = -1;
        playerDiceNum = -1;
        enemySkillNum = -1; 
        enemyDiceNum = -1;

        //Defense & Evade
        playerDefense = 0;
        playerEvade = 0;
        enemyDefense = 0;
        enemyEvade = 0;

        //Refresh Stunned
        playerStunned = false;
        enemyStunned = false;
    }
}
