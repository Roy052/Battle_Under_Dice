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
    [SerializeField] Text[] skillTexts, diceAmountTexts;
    //PlyaerUI
    [SerializeField] Text characterNameText_player, hpText_player, skillText_player;
    //EnemyUI
    [SerializeField] Text characterNameText_enemy, hpText_enemy, skillText_enemy;

    int characterNum_player;
    int[] skillSet_player;
    int[] diceArray = new int[6];

    int characterNum_enemy;
    int[] skillSet_enemy;



    public bool uiEnd = false;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Preset -> Network
        gm.SetEnemyInfo(0, new int[6] { 0, 1, 2, 3, 4, 5 });
        characterNum_player = gm.characterNum_player;
        skillSet_player = gm.skillSet_player;
        characterNum_enemy = gm.characterNum_enemy;
        skillSet_enemy = gm.skillSet_enemy;
        Debug.Log(skillSet_player[1]);

        SetUp();
    }
    public void SetUp()
    {
        bm.SetBattle();

        uiEnd = false;
        phaseText.text = "SetUp";

        //Name
        characterNameText_player.text = CharacterInfo.characterName[characterNum_player];
        characterNameText_enemy.text = CharacterInfo.characterName[characterNum_enemy];

        //DiceArray
        for (int i = 0; i < 6; i++)
            diceArray[i] = GameInfo.diceResetArray[i];

        //SkillButtonText
        for (int i = 0; i < 6; i++)
            skillTexts[i].text = SkillInfo.skillNameText[characterNum_player, skillSet_player[i]];

        RefreshUI();
        //Canvases Off
        SkillCanvasOff();
        DiceCanvasOff();
        CheckCanvasOff();

        uiEnd = true;
    }

    public IEnumerator BeforeStart()
    {
        uiEnd = false;
        phaseText.text = "BeforeStart";
        turnText.text = "Turn " + bm.turnNum;
        RefreshUI();
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
        yield return new WaitForSeconds(3);
        uiEnd = true;
    }

    public IEnumerator EndTurn()
    {
        uiEnd = false;
        phaseText.text = "EndTurn";
        RefreshUI();
        yield return new WaitForSeconds(3);
        uiEnd = true;

    }

    void RefreshUI()
    {
        //hp
        hpText_player.text = characterManager_player.character.hp.ToString();
        hpText_enemy.text = characterManager_enemy.character.hp.ToString();

        //diceNum
        diceArray = diceManager_player.GetDiceArray();
        for (int i = 0; i < 6; i++)
            diceAmountTexts[i].text = diceArray[i].ToString();
    }

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

    public void CheckCanvasOn()
    {
        checkCanvas.gameObject.SetActive(true);
    }

    public void CheckCanvasOff()
    {
        checkCanvas.gameObject.SetActive(false);
    }
}
