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

    protected List<Buff> buffList;
    protected List<Debuff> debuffList;

    //Animation
    [SerializeField] Animator characterAnimator;

    private void Awake()
    {
        Approach.player = this;
    }
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

        retSkill = ActivateSkillBuffDeBuff(retSkill, Approach.battleManager.gameStatus);

        return retSkill;
    }

    public void ReduceBuffDeBuffCount(GameStatus currentStatus)
    {
        foreach(Buff buff in buffList)
        {
            if (buff.reduceCountStatus == currentStatus)
                buff.turn--;

            if (buff.turn <= 0)
                buffList.Remove(buff);
        }

        foreach (Debuff debuff in debuffList)
        {
            if (debuff.reduceCountStatus == currentStatus)
                debuff.turn--;

            if (debuff.turn <= 0)
                debuffList.Remove(debuff);
        }
    }

    public Skill ActivateSkillBuffDeBuff(Skill retSkill, GameStatus currentStatus)
    {
        foreach (Buff buff in buffList)
        {
            switch(buff.buffType)
            {
                case BuffType.AddDamage:
                    if (retSkill.type == 0)
                        retSkill.value += buff.value;
                    break;
                case BuffType.AddSpeed:
                    retSkill.speed += buff.value;
                    break;
                case BuffType.AddDefense:
                    if (retSkill.type == 1)
                        retSkill.value += buff.value;
                    break;
                case BuffType.AddEvades:
                    if (retSkill.type == 2)
                        retSkill.value += buff.value;
                    break;
                case BuffType.AddEndurance:
                    retSkill.endurance += buff.value;
                    break;

            }

            ReduceBuffDeBuffCount(currentStatus);
        }

        foreach (Debuff debuff in debuffList)
        {
            switch (debuff.debuffType)
            {
                case DebuffType.ReduceDamage:
                    if (retSkill.type == 0)
                        retSkill.value -= debuff.value;
                    break;
                case DebuffType.ReduceSpeed:
                    retSkill.speed -= debuff.value;
                    break;
                case DebuffType.ReduceDefense:
                    if (retSkill.type == 1)
                        retSkill.value -= debuff.value;
                    break;
                case DebuffType.ReduceEvades:
                    if (retSkill.type == 2)
                        retSkill.value -= debuff.value;
                    break;
                case DebuffType.ReduceEndurance:
                    retSkill.endurance -= debuff.value;
                    break;

            }

            ReduceBuffDeBuffCount(currentStatus);
        }

        return retSkill;
    }
}
