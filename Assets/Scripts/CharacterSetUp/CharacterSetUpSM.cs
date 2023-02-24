using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSetUpSM : MonoBehaviour
{
    int characterNum = 0;
    [SerializeField] int maxCharacterNum;
    [SerializeField] GameObject[] characterBtn;
    [SerializeField] Image beforeChar, currentChar, nextChar;
    [SerializeField] Sprite[] characterSprites;
    [SerializeField] Sprite nullSprite;

    //Skill Select
    [SerializeField] Sprite[] characterSkill;

    [SerializeField] GameObject[] skillSelectedImage;
    [SerializeField] Image[] skillImages;
    [SerializeField] Sprite selectSprite, unselectSprite;
    bool[] skillSelected = new bool[8];
    int selectedNum = 6;

    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ChangeCharacter(gm.characterNum_player);
        SkillSetUp(gm.skillSet_player);
    }

    public void SkillSetUp(int[] skillSet)
    {
        //Init
        for (int i = 0; i < skillSelectedImage.Length; i++)
        {
            skillSelectedImage[i].SetActive(false);
            skillImages[i].sprite = unselectSprite;
        }

        //SetUp
        for (int i = 0; i < skillSet.Length; i++)
        {
            skillSelected[skillSet[i]] = true;
            skillSelectedImage[skillSet[i]].SetActive(true);
            skillImages[skillSet[i]].sprite = selectSprite;
        }
        selectedNum = 6;
    }

    public void SkillSelect(int num)
    {
        if(skillSelected[num] == true)
        {
            skillSelected[num] = false;
            skillSelectedImage[num].SetActive(false);
            skillImages[num].sprite = unselectSprite;
            selectedNum--;
        }
        else
        {
            skillSelected[num] = true;
            skillSelectedImage[num].SetActive(true);
            skillImages[num].sprite = selectSprite;
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