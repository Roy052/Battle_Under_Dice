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
    public virtual void SetPlayer(int characterNum, int[] skillSet)
    {
        characterManager.SetCharacter(characterNum);
        diceManager.DiceReset();

        this.characterNum = characterNum;
        this.skillSet = skillSet;


    }

    public virtual Skill UseSkill(int skillNum, int diceNum)
    {
        Skill retSkill = skillManager.UseSkill(characterNum, skillSet[skillNum], diceNum);
        diceManager.UseDice(diceNum);

        return retSkill;
    }
}
