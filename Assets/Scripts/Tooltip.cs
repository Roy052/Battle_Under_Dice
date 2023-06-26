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
        textDesc.text = SkillDesc.GetSkillDeliveryDescString(SkillDeliveryInfo.buffDesc[(int)type], value);
        this.gameObject.SetActive(true);
    }

    public void SetText(DebuffType type, int[] value)
    {
        textName.text = SkillDeliveryInfo.debuffName[(int)type];
        textDesc.text = SkillDesc.GetSkillDeliveryDescString(SkillDeliveryInfo.debuffDesc[(int)type], value);
        this.gameObject.SetActive(true);
    }
}
