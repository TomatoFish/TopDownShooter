using Game.Camera;
using Game.Input;
using System;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class PlayerMoveComponent : IInitializable, IDisposable, IFixedTickable
    {
        [Inject] private InputController _input;
        [Inject] private Player _model;
        [Inject] private PlayerView _view;
        [Inject] private CameraManager _cameraManager;

        private Vector3 moveVector;
        private Vector2 lookVector;
        private Vector3 mousePositionCache;
        private float speedMultiplier;
        private bool isSprinting;

        public Vector3 TranslateMov { get; private set; }
        public bool IsMoving { get; private set; }

        public PlayerMoveComponent()
        {
            speedMultiplier = 1f;
        }
        
        public void Initialize()
        {
            _input.Move += MoveHandler;
            _input.Look += LookHandler;
            _input.Slowdown += SlowdownHandler;
            _input.InputActions.Character.Enable();
        }
        
        public void Dispose()
        {
            _input.Move -= MoveHandler;
            _input.Look -= LookHandler;
            _input.Slowdown -= SlowdownHandler;
            _input.InputActions.Character.Disable();
        }

        public void FixedTick()
        {
            UpdateLook();
            UpdateMovement();
        }


        private void UpdateLook()
        {
            var lookDir = GetMouseLookDirection(_view.RotationTransform);
            var lookDirRelativeCamera = Quaternion.Euler(0, _cameraManager.Camera.transform.rotation.eulerAngles.y, 0) * lookDir;
            var angle = Vector3.Lerp(_view.RotationTransform.forward, lookDirRelativeCamera, _model.RotationSpeed);

#if UNITY_EDITOR
            Debug.DrawLine(_view.RotationTransform.position, _view.RotationTransform.position + lookDirRelativeCamera, Color.red);
#endif

            _view.RotationTransform.LookAt(_view.RotationTransform.position + angle);
        }

        public void UpdateMovement()
        {
            var movTranslate = GetMovementTranslate();
            _view.Controller.SimpleMove(movTranslate);
        }

        private void MoveHandler(Vector2 moveVector)
        {
            this.moveVector = new Vector3(moveVector.x, 0, moveVector.y);
            IsMoving = moveVector != Vector2.zero;
        }

        private void LookHandler(Vector2 lookVector)
        {
            this.lookVector = lookVector;
        }

        private void SlowdownHandler(bool isSlow)
        {
            speedMultiplier = isSlow ? 0.5f : 1f;
        }

        public Vector3 GetMovementTranslate()
        {
            var multiplier = moveVector.magnitude * speedMultiplier;
            var movV3 = moveVector;
            movV3 = Quaternion.Euler(0, _cameraManager.Camera.transform.rotation.eulerAngles.y, 0) * movV3;

            TranslateMov = Vector3.Lerp(TranslateMov, movV3 * _model.WalkSpeed * multiplier, Time.fixedDeltaTime * _model.WalkAcceleration);

            return TranslateMov;
        }

        public Vector3 GetMouseLookDirection(Transform rotationTransform)
        {
            var playerScreenPos = _cameraManager.Camera.WorldToScreenPoint(rotationTransform.position + Vector3.up * _model.Height);
            var playerScreenPosV3 = new Vector3(playerScreenPos.x, 0, playerScreenPos.y);
            var lookVectorV3 = new Vector3(lookVector.x, 0, lookVector.y);

            return (lookVectorV3 - playerScreenPosV3).normalized;
        }
    }
}
