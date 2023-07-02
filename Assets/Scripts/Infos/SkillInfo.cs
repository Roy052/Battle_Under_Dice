using System.Collections.Generic;
//class Skill
//{
//    public int type;
//    public int value;
//    public double speed;
//    public int stiff;
//    public int effect;
//}

public enum ConditionType
{
    None = -1,
    PlayerHpUpper = 0,
    PlayerHpLower = 1,
    EnemyHpUpper = 2,
    EnemyHpLower = 3,
    PassiveValue = 4,

    //Player Buffs
    PlayerBuffAnger = 100,

    MyDebuff = 11,
    //Player Debuffs
    PlayerDebuff = 200,
    
    //Enemy Buffs
    EnemyBuff = 300,

    //Enemy Debuffs
    EnemyDebuff = 400,

    HaveDice = 20,
    FlameCount = 21,
}

public enum TargetType
{
    Player = 0,
    Enemy = 1,
    Both = 2
}

public class SkillInfo
{
    public static Dictionary<int, string[]> skillNameText = new Dictionary<int, string[]>()
    {
        {0, new string[] { "잽", "안면강타", "걷어차기", "돌려차기", "비겁한 수", "핵펀치", "막기", "필사적인 막기","분노", "위협"} }
    };

    public static string[] skillTypeText = new string[5] { "Attack", "Defence", "Evade", "Buff", "Debuff" };
    //[Character, Skill, Dice]
    public static Dictionary<int, string[,]> skillDescriptionText = new Dictionary<int, string[,]>()
    {
        {0, new string[,]{
            { "잽을 날려 [s] 피해를 준다.", "잽을 날려 [s] 피해를 준다.", "잽을 날려 [s] 피해를 준다.", "잽을 날려 [s] 피해를 준다.", "잽을 날려 [s] 피해를 준다.", "잽을 날려 [s] 피해를 준다."},
            { "상대 얼굴을 가격해 [s] 피해를 준다.", "상대 얼굴을 가격해 [s] 피해를 준다.", "상대 얼굴을 가격해 [s] 피해를 준다.", "상대 얼굴을 가격해 [s] 피해를 준다.", "상대 얼굴을 가격해 [s] 피해를 준다.", "상대 얼굴을 가격해 [s] 피해를 준다."},
            { "상대 다리를 걷어차 [s] 피해를 준다.", "상대 다리를 걷어차 [s] 피해를 준다.", "상대 다리를 걷어차 [s] 피해를 준다.", "상대 다리를 걷어차 [s] 피해를 준다.", "상대 다리를 걷어차 [s] 피해를 준다.", "상대 다리를 걷어차 [s] 피해를 준다."},
            { "강하게 돌려차 [s] 피해를 준다.", "강하게 돌려차 [s] 피해를 준다.", "강하게 돌려차 [s] 피해를 준다.", "강하게 돌려차 [s] 피해를 준다.", "강하게 돌려차 [s] 피해를 준다.", "강하게 돌려차 [s] 피해를 준다."},
            { "상대 눈에 모래를 뿌린다. ", "상대 눈에 모래를 뿌린다. ", "상대 눈에 모래를 뿌린다. ", "상대 눈에 모래를 뿌린다. ", "상대 눈에 모래를 뿌린다. ]", "상대 눈에 모래를 뿌린다. ]"},
            { "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다."},
            { "팔을 들어 공격을 [s] 만큼 막는다.", "팔을 들어 공격을 [s] 만큼 막는다.", "팔을 들어 공격을 [s] 만큼 막는다.", "팔을 들어 공격을 [s] 만큼 막는다.", "팔을 들어 공격을 [s] 만큼 막는다.", "팔을 들어 공격을 [s] 만큼 막는다."},
            { "필사적으로 팔을 올려 공격을 [s] 만큼 막는다.", "필사적으로 팔을 올려 공격을 [s] 만큼 막는다.", "필사적으로 팔을 올려 공격을 [s] 만큼 막는다.", "필사적으로 팔을 올려 공격을 [s] 만큼 막는다.", "필사적으로 팔을 올려 공격을 [s] 만큼 막는다.", "필사적으로 팔을 올려 공격을 [s] 만큼 막는다."},
            { "분노를 끌어올려 자신의 공격을 강화한다.", "분노를 끌어올려 자신의 공격을 강화한다.", "분노를 끌어올려 자신의 공격을 강화한다.", "분노를 끌어올려 자신의 공격을 강화한다.", "분노를 끌어올려 자신의 공격을 강화한다.", "분노를 끌어올려 자신의 공격을 강화한다."},
            { "상대에게 위협을 가해 ", "DeBuff is [d]", "DeBuff is [d]", "DeBuff is [d]", "Buff is [d]", "Buff is [d]"} } }

    };

    public static Dictionary<int, string> skillCondTypeText = new Dictionary<int, string>
    {
        {0, "내 체력이 [c] 이상일 때" },
        {1, "내 체력이 [c] 이하일 때" },
        {2, "Enemy Hp is more than [c]" },
        {3, "Enemy Hp is less than [c]" },
        {4, "If I have passive count [c]" },
        {5, "[c]" },
    };

    //(character, skillnum)
    public static Dictionary<int, int[]> types = new Dictionary<int, int[]>()
    {
        {0, new int[]{0, 0, 0, 0, 0, 0, 1, 1, 3, 4} }
    };

    public static Dictionary<int, int[,]> condTypes = new Dictionary<int, int[,]>()
    {

        {0, new int[,]{
            { -1, -1, -1 },
            { 0, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 }}
        }
    };

    public static Dictionary<int, int[,]> condValues = new Dictionary<int, int[,]>()
    {
        {0, new int[,]{
            { -1, -1, -1 },
            { 50, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 }}
        }
    };

    public static Dictionary<int, int[,]> values = new Dictionary<int, int[,]>() 
    { 
        {0, new int[,]{
            { 1, 1, 1, 1, 1, 1 },
            { 5, 6, 7, 8, 9, 10 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 }, }
        }
    };

    public static Dictionary<int, int[,]> speeds = new Dictionary<int, int[,]>()
    {
            {0, new int[,]{{ 1, 1, 1, 1, 1, 1 },
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
        }
    };

    public static Dictionary<int, int[,]> endurances = new Dictionary<int, int[,]>()
    {
            {0, new int[,]{{ 5, 5, 5, 5, 5, 5 },
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
        }
    };

    public static Dictionary<int, int[,]> skillDelivery = new Dictionary<int, int[,]>()
    {
            {0, new int[,]{{ -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 },
                { 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1 },
            }
        }
    };
}
