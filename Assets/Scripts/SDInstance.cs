using UnityEngine;
using UnityEngine.UI;

public class SDInstance : MonoBehaviour
{
    Buff buff;
    Debuff debuff;
    bool isBuff = true;

    [SerializeField] Image sdImage;
    [SerializeField] Text sdValue, sdCount;

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
}
