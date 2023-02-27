using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator playerAnimator, enemyAnimator;
    [SerializeField] RuntimeAnimatorController[] characterSkillMotions;

    public IEnumerator AnimationOn(bool isPlayer, string skillName)
    {
        if (isPlayer)
            playerAnimator.SetBool(skillName, true);
        else
            enemyAnimator.SetBool(skillName, true);

        
        yield return new WaitForSeconds(GameInfo.battleAnimationDelay);

        if (isPlayer)
            playerAnimator.SetBool(skillName, false);
        else
            enemyAnimator.SetBool(skillName, false);
    }


    public void AnimationSet(int playerCharacterNum ,int enemyCharacterNum)
    {
        playerAnimator.runtimeAnimatorController = characterSkillMotions[playerCharacterNum];
        enemyAnimator.runtimeAnimatorController = characterSkillMotions[enemyCharacterNum];
    }
}
