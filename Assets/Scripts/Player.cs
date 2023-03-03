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

    //Animation
    [SerializeField] Animator characterAnimator;
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

        //0 : Attack, 1 : Defense, 2 : Evade
        if (retSkill.type == 0)
            retSkill.value += characterManager.character.damage;
        if (retSkill.type == 1)
            retSkill.value += characterManager.character.defense;
        if (retSkill.type == 2)
            retSkill.value += characterManager.character.evade;

        return retSkill;
    }
}
