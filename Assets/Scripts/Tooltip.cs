using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] Text textName, textDesc;

    private void Awake()
    {
        Approach.tooltip = this;
    }

    private void OnDestroy()
    {
        Approach.tooltip = null;
    }

    public void SetText(BuffType type, int[] value)
    {
        textName.text = SkillDeliveryInfo.buffName[(int)type];
        textDesc.text = Desc.GetSkillDeliveryDescString(SkillDeliveryInfo.buffDesc[(int)type], value);
        this.gameObject.SetActive(true);
    }

    public void SetText(DebuffType type, int[] value)
    {
        textName.text = SkillDeliveryInfo.debuffName[(int)type];
        textDesc.text = Desc.GetSkillDeliveryDescString(SkillDeliveryInfo.debuffDesc[(int)type], value);
        this.gameObject.SetActive(true);
    }

    public void SetText(int accessoryNum)
    {
        textName.text = AccessoryInfo.accessoryName[accessoryNum];
        textDesc.text = Desc.GetAccessoryDescString(AccessoryInfo.desc[accessoryNum], new int[]{ AccessoryInfo.value[accessoryNum]});
        this.gameObject.SetActive(true);
    }
}
