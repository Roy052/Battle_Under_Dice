using UnityEngine;
using UnityEngine.UI;

public class DiceButton : MonoBehaviour
{
    public Image diceImage;
    public GameObject activate;
    public Text diceAmountText;

    public void Set(int diceAmount)
    {
        //Dice Image()

        diceAmountText.text = diceAmount.ToString();
        activate.SetActive(false);
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
