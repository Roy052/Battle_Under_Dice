using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillInfo : MonoBehaviour
{
    public static Dictionary<int, string[]> skillNameText = new Dictionary<int, string[]>()
    {
        {100, new string[] { "��", "�ȸ鰭Ÿ", "�Ⱦ�����", "��������", "����� ��", "����ġ"} }
    };

    public static string[] skillTypeText = new string[5] { "Attack", "Defence", "Evade", "Buff", "Debuff" };
    //[Character, Skill, Dice]
    public static Dictionary<int, string[,]> skillDescriptionText = new Dictionary<int, string[,]>()
    {
        {0, new string[,]{
            { "���� ���� [s] ���ظ� �ش�.", "���� ���� [s] ���ظ� �ش�.", "���� ���� [s] ���ظ� �ش�.", "���� ���� [s] ���ظ� �ش�.", "���� ���� [s] ���ظ� �ش�.", "���� ���� [s] ���ظ� �ش�."},
            { "��� ���� ������ [s] ���ظ� �ش�.", "��� ���� ������ [s] ���ظ� �ش�.", "��� ���� ������ [s] ���ظ� �ش�.", "��� ���� ������ [s] ���ظ� �ش�.", "��� ���� ������ [s] ���ظ� �ش�.", "��� ���� ������ [s] ���ظ� �ش�."},
            { "��� �ٸ��� �Ⱦ��� [s] ���ظ� �ش�.", "��� �ٸ��� �Ⱦ��� [s] ���ظ� �ش�.", "��� �ٸ��� �Ⱦ��� [s] ���ظ� �ش�.", "��� �ٸ��� �Ⱦ��� [s] ���ظ� �ش�.", "��� �ٸ��� �Ⱦ��� [s] ���ظ� �ش�.", "��� �ٸ��� �Ⱦ��� [s] ���ظ� �ش�."},
            { "���ϰ� ������ [s] ���ظ� �ش�.", "���ϰ� ������ [s] ���ظ� �ش�.", "���ϰ� ������ [s] ���ظ� �ش�.", "���ϰ� ������ [s] ���ظ� �ش�.", "���ϰ� ������ [s] ���ظ� �ش�.", "���ϰ� ������ [s] ���ظ� �ش�."},
            { "��� ���� �𷡸� �Ѹ���. ", "��� ���� �𷡸� �Ѹ���. ", "��� ���� �𷡸� �Ѹ���. ", "��� ���� �𷡸� �Ѹ���. ", "��� ���� �𷡸� �Ѹ���. ]", "��� ���� �𷡸� �Ѹ���. ]"},
            { "�г븦 ���� ���ϰ� ��ġ�� ���� [s] ���ظ� ������.", "�г븦 ���� ���ϰ� ��ġ�� ���� [s] ���ظ� ������.", "�г븦 ���� ���ϰ� ��ġ�� ���� [s] ���ظ� ������.", "�г븦 ���� ���ϰ� ��ġ�� ���� [s] ���ظ� ������.", "�г븦 ���� ���ϰ� ��ġ�� ���� [s] ���ظ� ������.", "�г븦 ���� ���ϰ� ��ġ�� ���� [s] ���ظ� ������."} }
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
