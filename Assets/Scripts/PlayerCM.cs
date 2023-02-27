using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCM : MonoBehaviour
{
    [SerializeField] Text nameText;

    //Hp
    [SerializeField] GameObject hpBar;
    [SerializeField] Text hpText;
    [SerializeField] Image hpBar_Red;

    //Skill and Dice
    [SerializeField] Image skillImage;
    [SerializeField] Text skillText;
    [SerializeField] Image diceImage;
    [SerializeField] Sprite[] diceSprites;

    //DamageBox
    [SerializeField] Image damageBox;
    [SerializeField] Text damageText;

    //Shield
    [SerializeField] Image shieldBox;
    [SerializeField] Text shieldText;
    [SerializeField] Sprite shieldSprite, shieldBrokeSprite;

    //Stunned
    [SerializeField] Image stunnedImage;

    //defense beside hp
    [SerializeField] Image defenseEvadeBox;
    [SerializeField] Text defenseEvadeText;
    [SerializeField] Sprite[] defenseEvadeSprites;
    int defenseEvade = -1;

    [SerializeField] PlayerCM otherPlayerCM;
    public bool coroutineEnd = false;
    private void Start()
    {
        BoxOff();
        Defense_EvadeOff();
        SKillDiceOff();
        PlayerStunnedOff();
    }

    public void SetNameText(string name)
    {
        nameText.text = name;
    }

    public void SetHpBar(int hp, int maxHp)
    {
        hpText.text = hp + "/" + maxHp;
        hpBar_Red.fillAmount = hp / (float) maxHp;
    }
    
    //Reveal
    public IEnumerator RevealSkillAndDice(string skillText, int diceNum)
    {
        skillImage.gameObject.SetActive(true);
        this.skillText.text = skillText;
        StartCoroutine(FadeManager.FadeIn(this.skillText, 1));
        yield return new WaitForSeconds(1);

        diceImage.gameObject.SetActive(true);
        diceImage.sprite = diceSprites[diceNum];
        StartCoroutine(FadeManager.FadeIn(diceImage, 1));
        yield return new WaitForSeconds(1);

        //Ended
        coroutineEnd = true;
    }


    public void SKillDiceOff()
    {
        skillImage.gameObject.SetActive(false);
        diceImage.gameObject.SetActive(false);
    }

    //Battle
    public IEnumerator TakeDamage(bool shieldBroke, int shieldDamage,int damage)
    {
        //Shield And Damage
        if (shieldDamage > 0)
        {
            shieldBox.gameObject.SetActive(true);
            shieldText.text = "- " + shieldDamage.ToString();
            StartCoroutine(FadeManager.FadeIn(shieldBox, 0.5f));
            StartCoroutine(FadeManager.FadeIn(shieldText, 0.5f));
        }
        if (damage > 0)
        {
            damageBox.gameObject.SetActive(true);
            damageText.text = "- " + damage.ToString();
            StartCoroutine(FadeManager.FadeIn(damageBox, 0.5f));
            StartCoroutine(FadeManager.FadeIn(damageText, 0.5f));
        }
        
        yield return new WaitForSeconds(0.5f);

        
        if (shieldBroke)
        {
            defenseEvadeBox.sprite = shieldBrokeSprite;
            defenseEvadeText.text = "";
            StartCoroutine(FadeManager.FadeIn(defenseEvadeBox, 0.5f));
            yield return new WaitForSeconds(0.5f);
            Defense_EvadeOff();
        }

        if(shieldDamage > 0)
        {
            StartCoroutine(FadeManager.FadeOut(shieldBox, 1));
            StartCoroutine(FadeManager.FadeOut(shieldText, 1));
        }
        if(damage > 0)
        {
            StartCoroutine(FadeManager.FadeOut(damageBox, 1));
            StartCoroutine(FadeManager.FadeOut(damageText, 1));
        }
        
        yield return new WaitForSeconds(1);

        shieldBox.sprite = shieldSprite;
        shieldBox.gameObject.SetActive(false);
        damageBox.gameObject.SetActive(false);


        //Ended
        otherPlayerCM.coroutineEnd = true;
    }

    public IEnumerator PlayerStunned()
    {
        stunnedImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        //Ended
        coroutineEnd = true;
    }

    public void PlayerStunnedOff()
    {
        stunnedImage.gameObject.SetActive(false);
    }

    public IEnumerator Defense_EvadeOn(int defenseEvade, int value)
    {
        if (value == 0) Defense_EvadeOff();

        defenseEvadeBox.gameObject.SetActive(true);
        defenseEvadeBox.sprite = defenseEvadeSprites[defenseEvade];
        defenseEvadeText.text = value.ToString();
        this.defenseEvade = defenseEvade;

        yield return new WaitForSeconds(1);

        //Ended
        coroutineEnd = true;
    }

    public IEnumerator HpBarDisabled(float time)
    {
        hpBar.SetActive(false);
        defenseEvadeBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(time);

        hpBar.SetActive(true);
        defenseEvadeBox.gameObject.SetActive(true);

    }

    public void Defense_EvadeOff()
    {
        defenseEvadeBox.gameObject.SetActive(false);
    }

    public void BoxOff()
    {
        shieldBox.gameObject.SetActive(false);
        damageBox.gameObject.SetActive(false);
    }

    public void BoxOn()
    {
        shieldBox.gameObject.SetActive(true);
        damageBox.gameObject.SetActive(true);
    }
}
