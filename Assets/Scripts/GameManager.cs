using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
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

    //EnemyStatus
    public int isAI = 1;
    void Start()
    {
        skillInfo = new SkillInfo();
        gameInfo = new GameInfo();
        characterInfo = new CharacterInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemyInfo(int characterNum, int[] skillSet)
    {
        characterNum_enemy = characterNum;
        skillSet_enemy = skillSet;
    }
}
