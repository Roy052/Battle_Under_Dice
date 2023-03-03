using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSetUpSM : MonoBehaviour
{
    int characterNum = 0;
    int maxCharacterNum;
    [SerializeField] GameObject[] characterBtn;
    [SerializeField] Image beforeChar, currentChar, nextChar;
    [SerializeField] Sprite[] characterSprites;
    [SerializeField] Sprite nullSprite;

    //Skill Select
    [SerializeField] Sprite[] characterSkill;
    [SerializeField] Text[] skilltexts;
    [SerializeField] GameObject[] skillSelectedImage;
    [SerializeField] Image[] skillImages;
    [SerializeField] Sprite selectSprite, unselectSprite;
    bool[] skillSelected = new bool[10];
    int selectedNum = 6;

    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        maxCharacterNum = GameInfo.characterAmount;

        ChangeCharacter(gm.characterNum_player);
        SkillSetUp(gm.skillSet_player);
    }

    public void SkillSetUp(int[] skillSet)
    {
        int playerNum = gm.characterNum_player;
        //Init
        for (int i = 0; i < skillSelectedImage.Length; i++)
        {
            skillSelectedImage[i].SetActive(false);
            skillImages[i].sprite = gm.GetSkillSprite(playerNum, i);
            skilltexts[i].text = SkillInfo.skillNameText[playerNum, i];
        }

        //SetUp
        for (int i = 0; i < skillSet.Length; i++)
        {
            skillSelected[skillSet[i]] = true;
            skillSelectedImage[skillSet[i]].SetActive(true);
        }
        selectedNum = 6;
    }

    public void SkillSelect(int num)
    {
        if(skillSelected[num] == true)
        {
            skillSelected[num] = false;
            skillSelectedImage[num].SetActive(false);
            selectedNum--;
        }
        else
        {
            skillSelected[num] = true;
            skillSelectedImage[num].SetActive(true);
            selectedNum++;
        }

    }

    public void ChangeCharacter(int num)
    {
        characterNum = num;

        if (characterNum == 0) characterBtn[0].SetActive(false);
        if (characterNum == maxCharacterNum - 1) characterBtn[1].SetActive(false);

        //CharacterSprite
        if (characterNum != 0) beforeChar.sprite = characterSprites[characterNum - 1];
        else beforeChar.sprite = nullSprite;
        nextChar.sprite = characterSprites[characterNum + 1];
        if (characterNum != maxCharacterNum - 1) nextChar.sprite = characterSprites[characterNum + 1];
        else nextChar.sprite = nullSprite;
    }

    public void NextCharacter()
    {
        ChangeCharacter(characterNum + 1);
    }

    public void BeforeCharacter()
    {
        ChangeCharacter(characterNum - 1);
    }

    public void SaveAndExit()
    {
        if(selectedNum == 6)
        {
            int[] temp = new int[6];
            int count = 0;
            for (int i = 0; i < skillSelected.Length; i++)
            {
                if (skillSelected[i] == true) temp[count++] = i;
                if (count == 6) break;
            }
            gm.skillSet_player = temp;

            gm.SceneLoad_Menu();
        }
    }
}
