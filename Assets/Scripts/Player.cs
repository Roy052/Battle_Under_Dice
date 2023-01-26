using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] BattleManager bm;
    [SerializeField] public CharacterManager characterManager;
    [SerializeField] public SkillManager skillManager;
    [SerializeField] public DiceManager diceManager;

    int characterNum;
    int[] skillSet;
    public void SetPlayer(int characterNum, int[] skillSet)
    {
        this.characterNum = characterNum;
        characterManager.SetCharacter(characterNum);
        this.skillSet = skillSet;
    }

    public Skill UseSkill(int skillNum, int diceNum)
    {
        Skill retSkill = skillManager.UseSkill(characterNum, skillSet[skillNum], diceNum);
        diceManager.UseDice(diceNum);

        return retSkill;
    }
}
