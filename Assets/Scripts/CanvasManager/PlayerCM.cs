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
    [SerializeField] HPBar hpBarBig;
    [SerializeField] HPBar hpBarSmall;

    //Skill and Dice
    [SerializeField] Image skillImage;
    [SerializeField] Text skillText;
    [SerializeField] Image diceImage;
    [SerializeField] Sprite[] diceSprites;

    //DamageBox
    [SerializeField] Image damageBox;
    [SerializeField] Text damageText;

    //DefenseEavde Sprite
    [SerializeField] Sprite[] defenseEvadeSprites;
    [SerializeField] Sprite shieldSprite, shieldBrokeSprite;

    //Shield
    [SerializeField] Image defenseEvadeCharacterBox;
    [SerializeField] Text defenseEvadeCharacterText;

    //Stunned
    [SerializeField] Image stunnedImage;

    //defense beside hp
    [SerializeField] Image defenseEvadeHpBarBox;
    [SerializeField] Text defenseEvadeHpBarText;
    
    int defenseEvade = -1;

    //BuffDebuff
    [SerializeField] GameObject skillDeliveryPrefab;
    [SerializeField] List<SDInstance> skillDeliveries;

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
        hpBarBig.Set(hp, maxHp);
        hpBarSmall.Set(hp, maxHp);
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
            defenseEvadeCharacterBox.gameObject.SetActive(true);
            defenseEvadeCharacterText.text = "- " + shieldDamage.ToString();
            StartCoroutine(FadeManager.FadeIn(defenseEvadeCharacterBox, 0.5f));
            StartCoroutine(FadeManager.FadeIn(defenseEvadeCharacterText, 0.5f));
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
            defenseEvadeHpBarBox.sprite = shieldBrokeSprite;
            defenseEvadeHpBarText.text = "";
            StartCoroutine(FadeManager.FadeIn(defenseEvadeHpBarBox, 0.5f));
            yield return new WaitForSeconds(0.5f);
            Defense_EvadeOff();
        }

        if(shieldDamage > 0)
        {
            StartCoroutine(FadeManager.FadeOut(defenseEvadeCharacterBox, 1));
            StartCoroutine(FadeManager.FadeOut(defenseEvadeCharacterText, 1));
        }
        if(damage > 0)
        {
            StartCoroutine(FadeManager.FadeOut(damageBox, 1));
            StartCoroutine(FadeManager.FadeOut(damageText, 1));
        }
        
        yield return new WaitForSeconds(1);

        defenseEvadeCharacterBox.sprite = shieldSprite;
        defenseEvadeCharacterBox.gameObject.SetActive(false);
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
        if (value == 0)
        {
            Defense_EvadeOff();
        }
        else
        {
            //Character
            defenseEvadeCharacterBox.gameObject.SetActive(true);
            defenseEvadeCharacterBox.sprite = defenseEvadeSprites[defenseEvade];
            defenseEvadeCharacterText.text = value.ToString();
            
            //Hp Bar
            defenseEvadeHpBarBox.sprite = defenseEvadeSprites[defenseEvade];
            defenseEvadeHpBarText.text = value.ToString();

            this.defenseEvade = defenseEvade;
        }

        yield return new WaitForSeconds(1);

        //Ended
        coroutineEnd = true;
    }

    public IEnumerator SkillDeliveryOn(Buff buff)
    {
        GameObject tempObject = Instantiate(skillDeliveryPrefab, skillDeliveryPrefab.transform.parent);
        tempObject.transform.localScale = new Vector3(1, 1, 1);
        SDInstance tempInstance = tempObject.GetComponent<SDInstance>();
        tempInstance.Set(buff);
        tempInstance.gameObject.SetActive(true);

        skillDeliveries.Add(tempInstance);

        yield return new WaitForSeconds(1);

        //Ended
        coroutineEnd = true;
    }

    public IEnumerator SkillDeliveryOn(Debuff debuff)
    {
        GameObject tempObject = Instantiate(skillDeliveryPrefab, skillDeliveryPrefab.transform.parent);
        tempObject.transform.localScale = new Vector3(1, 1, 1);
        SDInstance tempInstance = tempObject.GetComponent<SDInstance>();
        tempInstance.Set(debuff);
        tempInstance.gameObject.SetActive(true);

        skillDeliveries.Add(tempInstance);

        yield return new WaitForSeconds(1);

        //Ended
        coroutineEnd = true;
    }

    public IEnumerator HpBarDisabledForAWhile(float time)
    {
        hpBarBig.DisableBar();
        if(defenseEvade > 0)
            defenseEvadeHpBarBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(time);

        hpBarSmall.EnableBar();
        if(defenseEvade > 0)
            defenseEvadeHpBarBox.gameObject.SetActive(true);

    }

    public void Defense_EvadeOff()
    {
        defenseEvadeHpBarBox.gameObject.SetActive(false);
    }

    public void BoxOff()
    {
        defenseEvadeCharacterBox.gameObject.SetActive(false);
        damageBox.gameObject.SetActive(false);
    }

    public void BoxOn()
    {
        defenseEvadeCharacterBox.gameObject.SetActive(true);
        damageBox.gameObject.SetActive(true);
    }

    public void ResetData()
    {
        defenseEvade = 0;
    }

    public void RefreshSDInstance(bool isRemove, Buff buff)
    {
        bool find = false;
        for(int i = 0; i < skillDeliveries.Count; i++)
        {
            if (skillDeliveries[i].isBuff && skillDeliveries[i].GetBuffType() == buff.buffType)
            {
                if (isRemove)
                {
                    Destroy(skillDeliveries[i].gameObject);
                    skillDeliveries.RemoveAt(i);
                    i--;
                }
                else
                    skillDeliveries[i].Set(buff);
                find = true;
                break;
            }
        }
        if (find == false) Debug.LogError("SD Buff Not Found : " + buff.buffType);
    }

    public void RefreshSDInstance(bool isRemove, Debuff debuff)
    {
        bool find = false;
        for (int i = 0; i < skillDeliveries.Count; i++)
        {
            if (skillDeliveries[i].isBuff == false && skillDeliveries[i].GetDebuffType() == debuff.debuffType)
            {
                if (isRemove)
                {
                    Destroy(skillDeliveries[i].gameObject);
                    skillDeliveries.RemoveAt(i);
                    i--;
                }
                else
                    skillDeliveries[i].Set(debuff);
                find = true;
                break;
            }
        }
        if (find == false) Debug.LogError("SD Debuff Not Found : " + debuff.debuffType);
    }
}
