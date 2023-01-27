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
    [SerializeField] CharacterManager playerCharacterManager;
    [SerializeField] DiceManager diceManager;

    //UI Components
    [SerializeField] Text characterNameText, hpText, skillText, turnText, phaseText;
    [SerializeField] Image turnImage;
    [SerializeField] Canvas skillCanvas, diceCanvas, checkCanvas;
    [SerializeField] Text[] skillTexts, diceAmountTexts;

    int characterNum;
    int[] skillSet;

    public bool uiEnd = false;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterNum = gm.characterNum;
        skillSet = gm.skillSet;
        Debug.Log(skillSet[1]);

        SetUp();
    }
    public void SetUp()
    {
        bm.SetBattle();

        uiEnd = false;
        phaseText.text = "SetUp";
        characterNameText.text = CharacterInfo.characterName[characterNum];
        hpText.text = playerCharacterManager.character.hp.ToString();
        skillText.text = SkillInfo.skillNameText[characterNum, skillSet[0]];

        for (int i = 0; i < 6; i++)
            skillTexts[i].text = SkillInfo.skillNameText[characterNum, skillSet[i]];

        for (int i = 0; i < 6; i++)
            diceAmountTexts[i].text = GameInfo.diceResetArray[i].ToString();
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
        yield return new WaitForSeconds(3);
        uiEnd = true;
    }

    public IEnumerator InTurn()
    {
        uiEnd = false;
        phaseText.text = "InTurn";
        SkillCanvasOn();
        yield return new WaitForSeconds(1);
    }

    public IEnumerator InTurnEnd()
    {
        uiEnd = false;
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

        yield return new WaitForSeconds(3);
        uiEnd = true;
    }

    public IEnumerator EndTurn()
    {
        uiEnd = false;
        phaseText.text = "EndTurn";
        yield return new WaitForSeconds(3);
        uiEnd = true;

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
