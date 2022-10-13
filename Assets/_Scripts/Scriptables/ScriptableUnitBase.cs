using System;
using UnityEngine;

public abstract class ScriptableExampleUnitBase : ScriptableObject {
    public Faction Faction;

    [SerializeField] private Stats _stats;
    public Stats BaseStats => _stats;

    // Used in game
    public HeroUnitBase Prefab;
    
    // Used in menus
    public string Description;
    public Sprite MenuSprite;
}

[Serializable]
public struct Stats {
    public int Health;
    public int AttackPower;
    public int TravelDistance;
}

[Serializable]
public enum Faction {
    Heroes = 0,
    Enemies = 1
}