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
    [SerializeField] Text characterNameText, hpText, skillText;

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
}
