using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSM : MonoBehaviour
{
    //Information
    GameManager gm;

    [SerializeField] BattleManager bm;
    [SerializeField] Player player, enemy;
    [SerializeField] CharacterManager characterManager_player, characterManager_enemy;
    [SerializeField] DiceManager diceManager_player;

    //UI Components
    //BattleUI
    [SerializeField] Text turnText, phaseText;
    [SerializeField] Image turnImage;

    //Canvas Managers
    [SerializeField] SkillCM skillCM;
    [SerializeField] DiceCM diceCM;
    [SerializeField] CheckCM checkCM;

    //PlyaerUI
    [SerializeField] PlayerCM playerCM;
    //EnemyUI
    [SerializeField] PlayerCM enemyCM;
    //GameEndUI
    [SerializeField] Sprite[] endImages;
    [SerializeField] Text endText;
    string[] endString = { "Victory", "Draw", "Defeat" };

    //Animation
    [SerializeField] AnimationManager animationManager;

    //BattleScreen
    [SerializeField] GameObject battleScreen;

    int characterNum_player;
    int[] skillSet_player;
    int[] diceArray = new int[6];

    int characterNum_enemy;
    int[] skillSet_enemy;

    public bool uiEnd = false;
    bool coroutineStart = false;

    //SetupEnd
    public bool setupEnd = false;

    private void Awake()
    {
        Approach.battleSM = this;
    }

    private void Update()
    {
        if (setupEnd && coroutineStart)
        {
            if (playerCM.coroutineEnd && enemyCM.coroutineEnd)
            {
                coroutineStart = false;
                uiEnd = true;
            }
        }
    }

    public void SetUp()
    {
        gm = Approach.gm;

        //Preset -> Network
        gm.SetEnemyInfo(0, new int[6] { 0, 1, 2, 3, 4, 5 });

        characterNum_player = gm.characterNum_player;
        skillSet_player = gm.skillSet_player;
        characterNum_enemy = gm.characterNum_enemy;
        skillSet_enemy = gm.skillSet_enemy;

        uiEnd = false;
        phaseText.text = "SetUp";

        //Name
        playerCM.SetNameText(CharacterInfo.characterName[characterNum_player]);
        enemyCM.SetNameText(CharacterInfo.characterName[characterNum_enemy]);

        //Canvas Setup
        skillCM.Setup(characterNum_player, skillSet_player);

        for (int i = 0; i < 6; i++)
            diceArray[i] = GameInfo.diceResetArray[i];
        diceCM.Setup(diceArray);
        
        RefreshUI();

        //Canvases Off
        skillCM.CanvasOff();
        diceCM.CanvasOff();
        checkCM.SkillDescOff();
        checkCM.CheckButtonOff();
        checkCM.CanvasOff();
        
        battleScreen.SetActive(false);

        uiEnd = true;
        setupEnd = true;
    }

    public IEnumerator BeforeStart()
    {
        uiEnd = false;
        phaseText.text = "BeforeStart";
        if (bm.turnNum != 12)
            turnText.text = "Turn " + bm.turnNum;
        else
            turnText.text = "Last Turn";

        RefreshUI();

        //Reset Skill and Dice
        playerCM.SKillDiceOff();
        enemyCM.SKillDiceOff();
        playerCM.PlayerStunnedOff();
        enemyCM.PlayerStunnedOff();

        //Reset Data
        playerSkillNum = -1;
        playerDiceNum = -1;
        playerCM.ResetData();
        enemyCM.ResetData();

        StartCoroutine(FadeManager.FadeIn(turnText, 1));
        StartCoroutine(FadeManager.FadeIn(turnImage, 1));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeManager.FadeOut(turnText, 1));
        StartCoroutine(FadeManager.FadeOut(turnImage, 1));
        yield return new WaitForSeconds(1);
        uiEnd = true;
    }

    public IEnumerator TurnStart()
    {
        uiEnd = false;
        phaseText.text = "TurnStart";
        RefreshUI();
        yield return new WaitForSeconds(3);
        uiEnd = true;
    }

    public IEnumerator InTurn()
    {
        uiEnd = false;
        phaseText.text = "InTurn";
        RefreshUI();

        //Skill Canvas ON
        skillCM.CanvasOn();
        checkCM.CanvasOn();
        yield return new WaitForSeconds(1);
    }

    public IEnumerator InTurnEnd()
    {
        uiEnd = false;
        RefreshUI();
        yield return new WaitForSeconds(1);
        uiEnd = true;
    }

    public IEnumerator Check()
    {
        uiEnd = false;
        phaseText.text = "Check";

        //Canvas Off
        skillCM.CanvasOff();
        diceCM.CanvasOff();
        checkCM.CanvasOff();
        RefreshUI();

        playerCM.coroutineEnd = false;
        enemyCM.coroutineEnd = false;
        coroutineStart = true;
        yield return new WaitForSeconds(5);
    }

    public IEnumerator Battle()
    {
        uiEnd = false;
        phaseText.text = "Battle";
        RefreshUI();

        playerCM.coroutineEnd = false;
        enemyCM.coroutineEnd = false;
        coroutineStart = true;
        yield return new WaitForSeconds(3);
    }

    public IEnumerator EndTurn()
    {
        uiEnd = false;
        phaseText.text = "EndTurn";
        RefreshUI();
        yield return new WaitForSeconds(3);
        uiEnd = true;

    }

    public void GameEnd(int whoWin)
    {
        endText.text = endString[whoWin];
    }

    public void RefreshUI()
    {
        //hp
        playerCM.SetHpBar(characterManager_player.character.hp, characterManager_player.character.maxHp);
        enemyCM.SetHpBar(characterManager_enemy.character.hp, characterManager_enemy.character.maxHp);

        skillCM.RefreshUI();
        //diceNum
        diceArray = diceManager_player.GetDiceArray();
        diceCM.RefreshUI(diceArray);

        //playerCM
        if (bm.playerDefense > 0)
            DefenseEvadeOn(true, 0, bm.playerDefense);
        else if (bm.playerEvade > 0)
            DefenseEvadeOn(true, 1, bm.playerEvade);
        else
            DefenseEvadeOff(true);

        //enemyCM
        if (bm.enemyDefense > 0)
            DefenseEvadeOn(false, 0, bm.enemyDefense);
        else if (bm.enemyEvade > 0)
            DefenseEvadeOn(false, 1, bm.enemyEvade);
        else
            DefenseEvadeOff(false);
    }

    //Reveal
    public void RevealSkillAndDice(int playerSkillNum, int playerDiceNum, int enemySkillNum, int enemyDiceNum)
    {
        StartCoroutine(playerCM.RevealSkillAndDice
            (SkillInfo.skillNameText[characterNum_player, skillSet_player[playerSkillNum]], playerDiceNum));

        StartCoroutine(enemyCM.RevealSkillAndDice
            (SkillInfo.skillNameText[characterNum_enemy, skillSet_enemy[enemySkillNum]], enemyDiceNum));
    }

    //Battle
    public void DefenseEvadeOn(bool isPlayer ,int defenseEvade, int value)
    {
        if (isPlayer)
        {
            StartCoroutine(playerCM.Defense_EvadeOn(defenseEvade, value));
        }
        else
        {
            StartCoroutine(enemyCM.Defense_EvadeOn(defenseEvade, value));
        }
            
    }

    public void DefenseEvadeOff(bool isPlayer)
    {
        if (isPlayer)
            playerCM.Defense_EvadeOff();
        else
            enemyCM.Defense_EvadeOff();
    }
    
    public void CharacterStunned(bool isPlayer)
    {
        if (isPlayer)
            StartCoroutine(playerCM.PlayerStunned());
        else
            StartCoroutine(enemyCM.PlayerStunned());
    }

    public IEnumerator IntenseBattleScreen()
    {
        StartCoroutine(playerCM.HpBarDisabled(GameInfo.battleAnimationDelay));
        StartCoroutine(enemyCM.HpBarDisabled(GameInfo.battleAnimationDelay));

        player.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        enemy.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        player.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        enemy.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        battleScreen.SetActive(true);

        yield return new WaitForSeconds(GameInfo.battleAnimationDelay);

        battleScreen.SetActive(false);
        player.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        enemy.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        player.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        enemy.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
    }

    public void TakeDamage(bool isPlayer, bool shieldBroke, int shieldDamage, int damage)
    {
        if (isPlayer)
        {
            StartCoroutine(playerCM.TakeDamage(shieldBroke, shieldDamage, damage));
            StartCoroutine(animationManager.AnimationOn(true, "TakeDamage"));
        }
        else
        {
            StartCoroutine(enemyCM.TakeDamage(shieldBroke, shieldDamage, damage));
            StartCoroutine(animationManager.AnimationOn(false, "TakeDamage"));
        }
            
    }

    int playerSkillNum = -1, playerDiceNum = -1;

    public void SelectSkillNum(int num)
    {
        playerSkillNum = num;

        //Skill Cancel
        if (num == -1)
        {
            diceCM.CanvasOff();
            checkCM.SkillDescOff();
            checkCM.CheckButtonOff();
            return;
        }

        diceCM.CanvasOn();
        bm.SelectSkillNum(num);
        checkCM.OnClickSkill(playerSkillNum);
        checkCM.SkillDescOn();
    }

    public void SelectDiceNum(int num)
    {
        playerDiceNum = num;

        //Dice Cancel
        if (num == -1)
        {
            checkCM.CheckButtonOff();
            checkCM.OnClickSkill(playerSkillNum, playerDiceNum);
            return;
        }

        checkCM.CheckButtonOn();
        bm.SelectDiceNum(num);
        checkCM.OnClickSkill(playerSkillNum, playerDiceNum);
    }
}
