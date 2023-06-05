using UnityEngine;
using UnityEngine.UI;

public class SDInstance : MonoBehaviour
{
    public bool isBuff = true;

    Buff buff;
    Debuff debuff;

    [SerializeField] Image sdImage;
    [SerializeField] Text sdValue, sdCount;
    [SerializeField] RectTransform rect;

    public void Set(Buff buff)
    {
        this.buff = buff;
        sdValue.text = buff.value.ToString();
        sdCount.text = buff.count.ToString();
        sdImage.sprite = Approach.gm.GetSkillDeliverySprite(true, (int)buff.buffType);

        isBuff = true;
    }

    public void Set(Debuff debuff)
    {
        this.debuff = debuff;
        sdValue.text = debuff.value.ToString();
        sdCount.text = debuff.count.ToString();
        isBuff = false;
    }

    public BuffType GetBuffType() { return buff.buffType; }
    public DebuffType GetDebuffType() { return debuff.debuffType; }
}
