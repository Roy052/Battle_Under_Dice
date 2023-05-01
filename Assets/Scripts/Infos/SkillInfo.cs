/*class Skill
{
    public int type;
    public int value;
    public double speed;
    public int stiff;
    public int effect;
}*/

public enum ConditionType
{
    None = -1,
    PlayerHpUpper = 0,
    PlayerHpLower = 1,
    EnemyHpUpper = 2,
    EnemyHpLower = 3,
    PassiveValue = 4,

    MyBuff = 10,
    MyDebuff = 11,
    EnemyBuff = 12,
    EnemyDebuff = 13,

    HaveDice = 20,
    FlameCount = 21,
}

public enum TargetType
{
    Player = 0,
    Enemy = 1
}

public class SkillInfo
{
    public static string[,] skillNameText = new string[1, 10]
    {
        {
            "Attack0", "Attack1", "Defence0", "Defence1", "Evade0", "Evade1", "Buff0", "Buff1","Debuff0", "Debuff1"
        }
    };

    public static string[] skillTypeText = new string[5] { "Attack", "Defence", "Evade", "Buff", "Debuff" };
    //[Character, Skill, Dice]
    public static string[,,] skillDescriptionText = new string[1, 10, 6]
    {
        {
            { "Attack0 Desc", "Attack0 Desc", "Attack0 Desc", "Attack0 Desc", "Attack0 Desc", "Attack0 Desc"},
            { "Attack1 Desc", "Attack1 Desc", "Attack1 Desc", "Attack1 Desc", "Attack1 Desc", "Attack1 Desc"},
            { "Defence0 Desc", "Defence0 Desc", "Defence0 Desc", "Defence0 Desc", "Defence0 Desc", "Defence0 Desc"},
            { "Defence1 Desc", "Defence1 Desc", "Defence1 Desc", "Defence1 Desc", "Defence1 Desc", "Defence1 Desc"},
            { "Evade0 Desc", "Evade0 Desc", "Evade0 Desc", "Evade0 Desc", "Evade0 Desc", "Evade0 Desc"},
            { "Evade1 Desc", "Evade1 Desc", "Evade1 Desc", "Evade1 Desc", "Evade1 Desc", "Evade1 Desc"},
            { "Buff0 Desc", "Buff0 Desc", "Buff0 Desc", "Buff0 Desc", "Buff0 Desc", "Buff0 Desc"},
            { "Buff1 Desc", "Buff1 Desc", "Buff1 Desc", "Buff1 Desc", "Buff1 Desc", "Buff1 Desc"},
            { "Debuff0 Desc", "Debuff0 Desc", "Debuff0 Desc", "Debuff0 Desc", "Debuff0 Desc", "Debuff0 Desc"},
            { "Debuff1 Desc", "Debuff1 Desc", "Debuff1 Desc", "Debuff1 Desc", "Debuff1 Desc", "Debuff1 Desc"}
        }
    };

    public static string[] skillCondTypeText = new string[10]
    {
        "Player Hp is more than [c]", 
        "Player Hp is less than [c]", 
        "Enemy Hp is more than [c]",
        "Enemy Hp is less than [c]",
        "If I have passive count [c]",
        "[c]",
        "[c]",
        "[c]",
        "[c]",
        "[c]"
    };

    //(character, skillnum)
    public static int[,] types = new int[1, 10]
    {
        {
            0, 0, 1, 1, 2, 2, 3, 3, 4, 4
        }
    };

    public static int[,,] condTypes = new int[1, 10, 3]
    {
        
        {
            { 0, -1, -1 },
            { 0, -1, -1 },
            { 1, -1, -1 },
            { 1, -1, -1 },
            { 2, -1, -1 },
            { 2, -1, -1 },
            { 3, -1, -1 },
            { 3, -1, -1 },
            { 4, -1, -1 },
            { 4, -1, -1 }
        }
    };

    public static int[,,] condValues = new int[1, 10, 3]
    {
        {
            { 1, -1, -1 },
            { 1, -1, -1 },
            { 1, -1, -1 },
            { 1, -1, -1 },
            { 1, -1, -1 },
            { 1, -1, -1 },
            { 1, -1, -1 },
            { 1, -1, -1 },
            { 1, -1, -1 },
            { 1, -1, -1 }
        }
    };

    public static int[,,] values = new int[1, 10, 6]{
        {
            { 1, 1, 1, 1, 1, 1 },
            { 5, 6, 7, 8, 9, 10 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 }
        }
    };

    public static int[,,] speeds = new int[1, 10, 6]{
        {
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 10, 10, 10, 10, 10, 10 },
            { 10, 10, 10, 10, 10, 10 },
            { 10, 10, 10, 10, 10, 10 },
            { 10, 10, 10, 10, 10, 10 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 }
        }
    };

    public static int[,,] endurances = new int[1, 10, 6]{
        {
            { 5, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 5 },
            { 10, 10, 10, 10, 10, 10 },
            { 10, 10, 10, 10, 10, 10 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 }
        }
    };

    public static int[,,] skillDelivery = new int[1, 10, 6]{
        {
            { -1, -1, -1, -1, -1, -1 },
            { 0, 0, 0, 0, 0, 0 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 }
        }
    };
}
