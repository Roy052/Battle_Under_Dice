using UnityEngine;
using UnityEngine.UI;

public class SDInstance : MonoBehaviour
{
    public bool isBuff = true;

    Buff buff;
    Debuff debuff;

    [SerializeField] Image sdImage;
    [SerializeField] Text sdValue;

    public void Set(Buff buff)
    {
        this.buff = buff;
        sdValue.text = buff.value.ToString();
        sdImage.sprite = Approach.gm.GetSkillDeliverySprite(true, (int)buff.buffType);

        isBuff = true;
    }

    public void Set(Debuff debuff)
    {
        this.debuff = debuff;
        sdValue.text = debuff.value.ToString();
        isBuff = false;
    }

    public BuffType GetBuffType() { return buff == null ? BuffType.None : buff.buffType; }
    public DebuffType GetDebuffType() { return debuff == null ? DebuffType.None : debuff.debuffType; }

    
    private void OnMouseEnter()
    {
        if (debuff == null)
        {
            switch (GetBuffType())
            {
                case BuffType.Anger:
                    Approach.tooltip.SetText(GetBuffType(), new int[] { buff.value, buff.value });
                    break;
                default:
                    Approach.tooltip.SetText(GetBuffType(), new int[] { buff.value });
                    break;
            }
        }
        else
            Approach.tooltip.SetText(GetDebuffType(), new int[] { debuff.value });

        Approach.tooltip.GetComponent<RectTransform>().position = Approach.posRevise + this.GetComponent<RectTransform>().position;
    }

    private void OnMouseExit()
    {
        Approach.tooltip.gameObject.SetActive(false);
    }
}
