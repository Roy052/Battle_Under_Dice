using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public int hp;
    public int damage;
    public int speed;
    public int defense;
    public int evade;
    public int endurance;
}
public class CharacterManager : MonoBehaviour
{
    int characterNum;
    Character[] upgrade;
    public Character character;

    public void SetUpgrade(Character[] upgrade)
    {
        this.upgrade = upgrade;
    }
    public void SetCharacter(int num)
    {
        characterNum = num;
        character = new Character();

        
        character.hp = CharacterInfo.hps[num];
        character.damage = CharacterInfo.damages[num];
        character.speed = CharacterInfo.speeds[num];
        character.defense = CharacterInfo.defenses[num];
        character.evade = CharacterInfo.evades[num];
        character.endurance = CharacterInfo.endurances[num];

        if (upgrade != null)
        {
            character.hp += upgrade[characterNum].hp;
            character.damage += upgrade[characterNum].damage;
            character.speed += upgrade[characterNum].speed;
            character.defense += upgrade[characterNum].defense;
            character.evade += upgrade[characterNum].evade;
            character.endurance += upgrade[characterNum].endurance;
        }
    }

    public void ChangeHp(int changeValue)
    {
        character.hp += changeValue;
    }
}