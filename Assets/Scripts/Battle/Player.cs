using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] BattleManager bm;
    [SerializeField] public CharacterManager characterManager;
    [SerializeField] public SkillManager skillManager;
    [SerializeField] public DiceManager diceManager;
    [SerializeField] PlayerCM playerCM;

    int characterNum;
    int[] skillSet;

    protected List<Buff> buffList = new List<Buff>();
    protected List<Debuff> debuffList = new List<Debuff>();

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

    public void AddBuffs(List<Buff> buffs)
    {
        buffList.AddRange(buffs);
    }

    public void AddDebuffs(List<Debuff> debuffs)
    {
        debuffList.AddRange(debuffs);
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
                case BuffType.Anger:
                    if (retSkill.type == 0)
                    {
                        retSkill.value += buff.value;
                        retSkill.speed += buff.value;
                    }
                    break;
            }
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
        }
        return retSkill;
    }

    public void ActivateNotSkillBuffAndDebuff(GameStatus currentStatus)
    {
        foreach (Buff buff in buffList)
        {
            switch (buff.buffType)
            {
                case BuffType.AddHp:
                    characterManager.ChangeHp(buff.value);
                    break;
            }
        }

        foreach (Debuff debuff in debuffList)
        {
            switch (debuff.debuffType)
            {
                case DebuffType.ReduceHp:
                    characterManager.ChangeHp(-debuff.value);
                    break;
            }
        }
    }

    public bool IsConditionFulfilled(int conditionNum, int value)
    {
        if(conditionNum < 100)
        {
            return characterManager.IsConditionFulfilled(conditionNum, value);
        }
        else
        {
            switch((ConditionType)conditionNum)
            {
                case ConditionType.PlayerBuffAnger:
                    return BuffAmount(BuffType.Anger) >= value;
            }

            return false;
        }
    }

    int BuffAmount(BuffType buffType)
    {
        foreach (Buff buff in buffList)
        {
            if (buff.buffType == buffType) return buff.value;
        }

        return 0;
    }

    public int GetCharacterNum() { return characterNum; }
}
