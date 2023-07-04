using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillInfo : MonoBehaviour
{
    public static Dictionary<int, string[]> skillNameText = new Dictionary<int, string[]>()
    {
        {100, new string[] { "잽", "안면강타", "걷어차기", "돌려차기", "비겁한 수", "핵펀치"} }
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
            { "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다.", "분노를 통해 강하게 펀치를 날려 [s] 피해를 입힌다."} }
        }
    };

    //(character, skillnum)
    public static Dictionary<int, int[]> types = new Dictionary<int, int[]>()
    {
        {100, new int[]{0, 0, 0, 0, 0, 0} },
    };

    public static Dictionary<int, int[,]> condTypes = new Dictionary<int, int[,]>()
    {

        {100, new int[,]{
            { -1, -1, -1 },
            { 0, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 }}
        }
    };

    public static Dictionary<int, int[,]> condValues = new Dictionary<int, int[,]>()
    {
        {100, new int[,]{
            { -1, -1, -1 },
            { 50, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 },
            { -1, -1, -1 }}
        }
    };

    public static Dictionary<int, int[,]> values = new Dictionary<int, int[,]>()
    {
        {100, new int[,]{
            { 1, 1, 1, 1, 1, 1 },
            { 5, 6, 7, 8, 9, 10 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 },
            { 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2 } }
        }
    };

    public static Dictionary<int, int[,]> speeds = new Dictionary<int, int[,]>()
    {
        {100, new int[,]{
                { 1, 1, 1, 1, 1, 1 },
                { 2, 2, 2, 2, 2, 2 },
                { 10, 10, 10, 10, 10, 10 },
                { 10, 10, 10, 10, 10, 10 },
                { 10, 10, 10, 10, 10, 10 },
                { 10, 10, 10, 10, 10, 10 }}
        }
    };

    public static Dictionary<int, int[,]> endurances = new Dictionary<int, int[,]>()
    {
        {100, new int[,]{
                { 5, 5, 5, 5, 5, 5 },
                { 5, 5, 5, 5, 5, 5 },
                { 10, 10, 10, 10, 10, 10 },
                { 10, 10, 10, 10, 10, 10 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 }}
        }
    };

    public static Dictionary<int, int[,]> skillDelivery = new Dictionary<int, int[,]>()
    {
        {100, new int[,]{
                { -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }}
        }
    };
}
