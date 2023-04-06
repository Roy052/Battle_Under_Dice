using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
            Approach.gm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int characterNum_player;
    public int[] skillSet_player;

    public int characterNum_enemy;
    public int[] skillSet_enemy;

    //Infos
    public SkillInfo skillInfo;
    public GameInfo gameInfo;
    public CharacterInfo characterInfo;

    //SkillImages
    Sprite[,] skillSprites;

    //EnemyStatus
    public int isAI = 1;


    void Start()
    {
        skillInfo = new SkillInfo();
        gameInfo = new GameInfo();
        characterInfo = new CharacterInfo();
        SkillSpritesSetup();
    }

    private void SkillSpritesSetup()
    {
        int characterAmount = GameInfo.characterAmount;

        skillSprites = new Sprite[characterAmount, 10];

        for(int characterNum = 0; characterNum < characterAmount; characterNum++)
        {
            Sprite[] tempSprites = Resources.LoadAll<Sprite>("Arts/Skill/" + characterNum);
            for (int spriteNum = 0; spriteNum < tempSprites.Length; spriteNum++)
            {
                skillSprites[characterNum, spriteNum] = tempSprites[spriteNum];
            }
        }
    }

    public Sprite GetSkillSprite(int characterNum, int skillNum)
    {
        return skillSprites[characterNum, skillNum];
    }

    public void SetPlayerSkill(int[] skillSet)
    {
        skillSet_player = skillSet;
    }

    public void SetEnemyInfo(int characterNum, int[] skillSet)
    {
        characterNum_enemy = characterNum;
        skillSet_enemy = skillSet;
    }

    public void SceneLoad_Match()
    {
        SceneManager.LoadScene("Match");
    }

    public void SceneLoad_CharacterSetUp()
    {
        SceneManager.LoadScene("CharacterSetUp");
    }
    
    public void SceneLoad_Battle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void SceneLoad_Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
