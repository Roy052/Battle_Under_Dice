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
    [SerializeField] Canvas skillCanvas, diceCanvas, checkCanvas;

    //Canvas Components
    [SerializeField] Text[] skillTexts, diceAmountTexts;
    [SerializeField] Image[] skillButtonImages;
    [SerializeField] Button[] skillButtons, diceButtons;
    [SerializeField] GameObject[] skillUnderlines, diceUnderlines;

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

    //CMs
    [SerializeField] CheckCM checkCM;

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

        //DiceArray
        for (int i = 0; i < 6; i++)
            diceArray[i] = GameInfo.diceResetArray[i];

        //SkillButtonText
        for (int i = 0; i < 6; i++)
            skillTexts[i].text = SkillInfo.skillNameText[characterNum_player, skillSet_player[i]];

        //SkillButtonImage
        for (int i = 0; i < 6; i++)
            skillButtonImages[i].sprite = gm.GetSkillSprite(characterNum_player, skillSet_player[i]);

        RefreshUI();
        //Canvases Off
        SkillCanvasOff();
        DiceCanvasOff();
        checkCM.SkillDescOff();
        checkCM.CheckButtonOff();
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

        //Reset Underlines
        for(int i = 0; i < skillUnderlines.Length; i++)
        {
            skillUnderlines[i].SetActive(false);
            diceUnderlines[i].SetActive(false);
        }

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
        SkillCanvasOn();
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
        skillCanvas.gameObject.SetActive(false);
        diceCanvas.gameObject.SetActive(false);
        checkCanvas.gameObject.SetActive(false);
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

        //diceNum
        diceArray = diceManager_player.GetDiceArray();
        for (int i = 0; i < 6; i++)
        {
            if (diceArray[i] == 0) diceButtons[i].enabled = false;
            diceAmountTexts[i].text = diceArray[i].ToString();
        }

        //Underlines
        for (int i = 0; i < skillUnderlines.Length; i++)
        {
            skillUnderlines[i].SetActive(false);
            diceUnderlines[i].SetActive(false);
        }

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

    public void EndCharacterCM(bool isPlayer)
    {
        if (isPlayer) playerCM.coroutineEnd = true;
        else enemyCM.coroutineEnd = true;
    }

    //Canvas ON/OFF
    public void SkillCanvasOn()
    {
        skillCanvas.gameObject.SetActive(true);
    }

    public void SkillCanvasOff()
    {
        skillCanvas.gameObject.SetActive(false);
    }

    public void DiceCanvasOn()
    {
        diceCanvas.gameObject.SetActive(true);
    }

    public void DiceCanvasOff()
    {
        diceCanvas.gameObject.SetActive(false);
    }

    int playerSkillNum = -1, playerDiceNum = -1;

    public void SelectSkillNum(int num)
    {
        //Skill Cancel
        if (playerSkillNum == num)
        {
            skillUnderlines[playerSkillNum].SetActive(false);

            playerSkillNum = -1;
            DiceCanvasOff();
            checkCM.SkillDescOff();
            checkCM.CheckButtonOff();
            return;
        }

        if(playerSkillNum != -1) skillUnderlines[playerSkillNum].SetActive(false);
        playerSkillNum = num;
        skillUnderlines[playerSkillNum].SetActive(true);
        DiceCanvasOn();

        bm.SelectSkillNum(num);

        checkCM.OnClickSkill(playerSkillNum);
        checkCM.SkillDescOn();
    }

    public void SelectDiceNum(int num)
    {
        if (playerDiceNum == num)
        {
            diceUnderlines[playerDiceNum].SetActive(false);

            playerDiceNum = -1;
            checkCM.CheckButtonOff();
            checkCM.OnClickSkill(playerSkillNum, playerDiceNum);
            return;
        }

        if (playerDiceNum != -1) diceUnderlines[playerDiceNum].SetActive(false);
        playerDiceNum = num;
        diceUnderlines[playerDiceNum].SetActive(true);
        checkCM.CheckButtonOn();

        bm.SelectDiceNum(num);

        checkCM.OnClickSkill(playerSkillNum, playerDiceNum);
    }
}
