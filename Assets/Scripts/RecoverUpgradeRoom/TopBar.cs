using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TopBar : MonoBehaviour
{
    [SerializeField] Text textName, textHp, textGold;

    public void Set(string name, int hp, int maxHp, int gold)
    {
        textName.text = name;
        textHp.text = $"{hp} / {maxHp}";
        textGold.text = gold.ToString();
    }
}
