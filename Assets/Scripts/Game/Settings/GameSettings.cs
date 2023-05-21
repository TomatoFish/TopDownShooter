using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class GameSettings
    {
        [SerializeField] private bool _aimSwitch;

        public bool AimSwitch => _aimSwitch;
    }
}
