using System.Collections.Generic;
using UnityEngine;

namespace Game.Level
{
    public class PlayerInventoryView : MonoBehaviour
    {
        [SerializeField] private Transform _weaponContainer;
        [SerializeField] private LineRenderer _laserAim;

        public Transform WeaponContainer => _weaponContainer;
        public LineRenderer LaserAim => _laserAim;
    }
}
