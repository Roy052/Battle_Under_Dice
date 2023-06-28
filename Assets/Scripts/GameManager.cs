using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
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
    Sprite[] diceSprite;
    Dictionary<int, Sprite> buffSprite;
    List<Sprite> debuffSprite;


    //EnemyStatus
    public int isAI = 1;


    void Start()
    {
        skillInfo = new SkillInfo();
        gameInfo = new GameInfo();
        characterInfo = new CharacterInfo();

        SkillSpritesSetup();
        DiceSpritesSetup();
        SkillDeliverySpritesSetup();
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

    private void DiceSpritesSetup()
    {
        diceSprite = new Sprite[6];
        diceSprite = Resources.LoadAll<Sprite>("Arts/Dice/");
    }

    private void SkillDeliverySpritesSetup()
    {
        buffSprite =  new List<Sprite>(Resources.LoadAll<Sprite>("Arts/SkillDelivery/Buff"))
            .ToDictionary(x => int.Parse(x.name.Substring("buff".Length)), x => x);
        debuffSprite = new List<Sprite>(Resources.LoadAll<Sprite>("Arts/SkillDelivery/Debuff"));
    }

    public Sprite GetSkillSprite(int characterNum, int skillNum)
    {
        return skillSprites[characterNum, skillNum];
    }

    public Sprite GetDiceSprite(int diceNum)
    {
        if (diceNum == -1)
            return diceSprite[6];
        return diceSprite[diceNum];
    }

    public Sprite GetSkillDeliverySprite(bool isBuff, int id)
    {
        if (isBuff)
            return buffSprite[id];
        else
            return debuffSprite[id];
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
