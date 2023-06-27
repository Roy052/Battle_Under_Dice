using UnityEngine;
using UnityEngine.UI;

public class Accessory : MonoBehaviour
{
    public Image image;
    public int accessoryNum;

    public void Set(int num)
    {
        accessoryNum = num;

    }

    private void OnMouseEnter()
    {
        Approach.tooltip.SetText(accessoryNum);
        Approach.tooltip.GetComponent<RectTransform>().position = Approach.posRevise + this.GetComponent<RectTransform>().position;
    }

    private void OnMouseExit()
    {
        Approach.tooltip.gameObject.SetActive(false);
    }
}
