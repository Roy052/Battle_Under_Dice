using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCM : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text hpText;

    //DamageBox
    [SerializeField] Image damageBox;
    [SerializeField] Text damageText;

    //Shield
    [SerializeField] Image shieldBox;
    [SerializeField] Text shieldText;
    [SerializeField] Sprite shieldSprite, shieldBrokeSprite;

    public void SetNameText(string name)
    {
        nameText.text = name;
    }

    public void SetHpText(int hp)
    {
        hpText.text = hp.ToString();
    }

    public IEnumerator TakeDamage(bool shieldBroke, int shieldDamage,int damage)
    {
        //Shield Damage
        shieldBox.gameObject.SetActive(true);
        shieldText.text = shieldDamage.ToString();

        StartCoroutine(FadeManager.FadeIn(shieldBox, 0.5f));
        StartCoroutine(FadeManager.FadeIn(shieldText, 0.5f));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeManager.FadeOut(shieldText, 1));
        if (shieldBroke)
        {
            shieldBox.sprite = shieldBrokeSprite;
            StartCoroutine(FadeManager.FadeIn(shieldBox, 0.5f));
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(FadeManager.FadeOut(shieldBox, 1));
        yield return new WaitForSeconds(1);

        shieldBox.sprite = shieldSprite;
        shieldBox.gameObject.SetActive(false);
        
        //Damage
        damageBox.gameObject.SetActive(true);
        damageText.text = damage.ToString();

        StartCoroutine(FadeManager.FadeIn(damageBox, 0.5f));
        StartCoroutine(FadeManager.FadeIn(damageText, 0.5f));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(1);

        StartCoroutine(FadeManager.FadeOut(damageBox, 1));
        StartCoroutine(FadeManager.FadeOut(damageText, 1));
        yield return new WaitForSeconds(1);

        damageBox.gameObject.SetActive(false);
    }
}
