using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Party,
    NPC,
    Enemy
}
[CreateAssetMenu(fileName = "Unit", menuName = "Unit")]



public class Unit : ScriptableObject
{
    public string unitName;
    public GameObject model;
    public Type Type;

    public int lvl;
    public float exp;
    public float maxExp;
    public int hitPoints;
    public int maxHitPoints;
    public int skillPoints;
    public int maxSkillPoints;

    public int offense;
    public int defense;
    public int agility;

    public bool alive = true;


}
