using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] BattleManager bm;
    [SerializeField] CharacterManager characterManager;
    [SerializeField] SkillManager skillManager;
    [SerializeField] DiceManager diceManager;

    int characterNum;
    int[] skillSet;
    public void SetPlayer(int characterNum, int[] skillSet)
    {
        this.characterNum = characterNum;
        characterManager.SetCharacter(characterNum);
        this.skillSet = skillSet;
    }

    public void UseSkill(int num)
    {
        skillManager.UseSkill(characterNum, skillSet[num]);
    }
}
