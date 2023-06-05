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

    public void ReduceBuffDeBuffCount(GameStatus currentStatus)
    {
        for(int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].reduceCountStatus == currentStatus)
                buffList[i].count--;

            Buff temp = buffList[i];
            bool isRemove = false;

            if (buffList[i].count <= 0)
            {
                isRemove = true;
                buffList.Remove(buffList[i]);
                i--;
            }

            playerCM.RefreshSDInstance(isRemove, temp);
        }

        for (int i = 0; i < debuffList.Count; i++)
        {
            if (debuffList[i].reduceCountStatus == currentStatus)
                debuffList[i].count--;

            Debuff temp = debuffList[i];
            bool isRemove = false;

            if (debuffList[i].count <= 0)
            {
                isRemove = true;
                debuffList.Remove(debuffList[i]);
                i--;
            }

            playerCM.RefreshSDInstance(isRemove, temp);
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
}
