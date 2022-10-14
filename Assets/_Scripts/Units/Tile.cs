using System;
using UnityEngine;

namespace _Scripts.Units
{
    public class Tile : MonoBehaviour
    {
        public Faction faction;
        public Tile top;
        public Tile left;
        public Tile right;
        public Tile bottom;
    }

    [Serializable]
    public enum Faction
    {
        None,
        Blue,
        Red
    }
}