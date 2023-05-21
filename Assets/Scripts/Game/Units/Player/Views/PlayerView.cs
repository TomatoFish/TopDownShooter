using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Game.Level
{
    public class PlayerView : UnitView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _movementTransform;
        [SerializeField] private Transform _rotationTransform;
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private Transform _aimTarget;
        [SerializeField] private CharacterController _controller;
        
        public Animator Animator => _animator;
        public Transform MovementTransform => _movementTransform;
        public Transform RotationTransform => _rotationTransform;
        public Transform CameraTarget => _cameraTarget;
        public Transform AimTarget => _aimTarget;
        public CharacterController Controller => _controller;
    }
}
