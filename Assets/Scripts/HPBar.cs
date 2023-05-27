using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Image hpBar_Red;

    public void Set(int hp, int maxHp)
    {
        hpText.text = hp + "/" + maxHp;
        hpBar_Red.fillAmount = hp / (float)maxHp;
    }

    public void EnableBar()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableBar()
    {
        this.gameObject.SetActive(false);
    }
}
