/*class Skill
{
    public int type;
    public int value;
    public double speed;
    public int stiff;
    public int effect;
}*/

public class SkillInfo
{
    public static string[,] skillNameText =
    {
        {
            "Attack0", "Attack1", "Defence0", "Defence1", "Evade0", "Evade1", "Buff0", "Buff1","Debuff0", "Debuff1"
        }
    };

    public static string[] skillTypeText = { "Attack", "Defence", "Evade", "Buff", "Debuff" };
    //[Character, Skill, Dice]
    public static string[,,] skillDescriptionText =
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

    //(character, skillnum)
    public static int[,] types =
    {
        {
            0, 0, 1, 1, 2, 2, 3, 3, 4, 4
        }
    };

    public static int[,,] values = { 
        {
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
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

    public static int[,,] speeds = {
        {
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 }
        }
    };

    public static int[,,] endurances = {
        {
            { 5, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 5 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 }
        }
    };

    public static int[,,] effects = {
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
