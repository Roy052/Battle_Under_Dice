using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approach
{
    //GameObjects
    public static BattleManager battleManager;
    public static BattleSM battleSM;
    public static Player player;
    public static Player enmey;
    public static GameManager gm;
    public static Tooltip tooltip;

    //Infos
    public static CharacterInfo characterInfo = new CharacterInfo();
    public static GameInfo gameInfo = new GameInfo();
    public static SkillInfo skillInfo = new SkillInfo();

    //Static Value
    public static Vector3 posRevise = new Vector3(1.5f, -1.3f, 0);
}
