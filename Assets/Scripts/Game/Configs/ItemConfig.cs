using System;
using Logic.Configs;
using UnityEngine;

namespace Game.Config
{
    [Serializable]
    public class ItemConfig : IItemConfig
    {
        [SerializeField] private int _animatorLayer;
        [SerializeField] private float _useCooldown;

        public int AnimatorLayer => _animatorLayer;
        public float UseCooldown => _useCooldown;
    }
}
