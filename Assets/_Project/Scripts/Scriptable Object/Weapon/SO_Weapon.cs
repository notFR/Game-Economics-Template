using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
    [CreateAssetMenu(fileName = "so_weapon", menuName = "Scriptable Objects/Weapon/New Weapon")]
    public class SO_Weapon : ScriptableObject
    {
        public WeaponType weaponType;
        public int ID;
        public GameObject uiObject;
        public string weaponName;
        [TextArea(10, 10)]
        public string weaponDesc;

        [Header("Stat")] 
        public int damage;
        public int attackRange;

        [Header("Speciality")] 
        public Sprite[] specialityIcon;
        [Header("Level")] 
        public List<WeaponLevel> weaponLevel;

    }

    [System.Serializable]
    public class WeaponLevel
    {
        public int damagePoint;
        public int attackPoint;
        public int CoinToUpgrade;
    }

    public enum WeaponType
    {
        Arrow,
        Axe,
        Bomb,
        Hammer
    }
}


