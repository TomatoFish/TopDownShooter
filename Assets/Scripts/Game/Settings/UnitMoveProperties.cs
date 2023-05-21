using System;
using Logic.Settings;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class UnitMoveProperties : IUnitMoveProperties
    {
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _walkAcceleration;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _bodyRotationAngle;
        [SerializeField] private float _height;
        [SerializeField] private float _aimSpeed;

        public float WalkSpeed => _walkSpeed;
        public float WalkAcceleration => _walkAcceleration;
        public float RotationSpeed => _rotationSpeed;
        public float BodyRotationAngle => _bodyRotationAngle;
        public float Height => _height;
        public float AimSpeed => _aimSpeed;
    }
}