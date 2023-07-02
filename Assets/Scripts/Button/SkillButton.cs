using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Image skillImage;
    public GameObject activate;
    public Text skillText;
    public void Set(int characterNum, int skillNum)
    {
        skillImage.sprite = Approach.gm.GetSkillSprite(characterNum, skillNum);
        skillText.text = SkillInfo.skillNameText[characterNum][skillNum];
        activate.SetActive(false);
    }
    
    public void Enable()
    {
        this.GetComponent<Button>().enabled = true;
        this.GetComponent<Image>().color = Color.white;
    }

    public void Disable()
    {
        this.GetComponent<Button>().enabled = false;
        this.GetComponent<Image>().color = Color.gray;
    }

    public void Activate()
    {
        activate.SetActive(true);
    }

    public void Deactivate()
    {
        activate.SetActive(false);
    }
}
