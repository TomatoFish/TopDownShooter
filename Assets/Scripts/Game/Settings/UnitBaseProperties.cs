using System;
using Logic.Settings;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class UnitBaseProperties : IUnitBaseProperties
    {
        [SerializeField] private float _maxShield;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _minHealth;

        public float MaxShield => _maxShield;
        public float MaxHealth => _maxHealth;
        public float MinHealth => _minHealth;
    }
}