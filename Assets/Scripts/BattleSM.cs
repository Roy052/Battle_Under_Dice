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
    [SerializeField] Text characterNameText, hpText, skillText, turnText;
    [SerializeField] Canvas skillCanvas, diceCanvas, checkCanvas;

    int characterNum;
    int[] skillSet;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterNum = gm.characterNum;
        skillSet = gm.skillSet;

        SetUp();
    }
    public void SetUp()
    {
        bm.SetBattle();

        characterNameText.text = CharacterInfo.characterName[characterNum];
        hpText.text = playerCharacterManager.character.hp.ToString();
        skillText.text = SkillInfo.skillNameText[characterNum, skillSet[0]];
    }

    public void BeforeStart()
    {
        turnText.text = "Turn " + bm.turnNum;
    }

    public void TurnStart()
    {

    }

    public void InTurn()
    {
        skillCanvas.gameObject.SetActive(true);
    }

    public void Check()
    {
        skillCanvas.gameObject.SetActive(false);
        diceCanvas.gameObject.SetActive(false);
        checkCanvas.gameObject.SetActive(false);
    }

    public void EndTurn()
    {

    }

    public void DiceCanvasOn()
    {
        diceCanvas.gameObject.SetActive(true);
    }

    public void CheckCanvasOn()
    {
        checkCanvas.gameObject.SetActive(true);
    }
}
