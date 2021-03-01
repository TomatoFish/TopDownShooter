using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    public class InputController : IDisposable
    {
        public Action<Vector2> OnMove;
        public Action<Vector2> OnLook;
        public Action<bool> OnAim;
        public Action<bool> OnFire;
        public Action<bool> OnCrouch;
        public Action OnMelee;
        public Action<int> OnItemChange;

        public PlayerInput InputActions { get; private set; }

        public InputController()
        {
            InputActions = new PlayerInput();
            InputActions.Character.Movement.performed += MovementHandler;
            InputActions.Character.Look.performed += LookHandler;
            InputActions.Character.Aim.performed += AimHandler;
            InputActions.Character.Aim.canceled += AimCancelHandler;
            InputActions.Character.Fire.performed += FireHandler;
            InputActions.Character.Fire.canceled += FireCancelHandler;
            InputActions.Character.Melee.performed += MeleeHandler;
            InputActions.Character.Crouch.performed += CrouchHandler;
            InputActions.Character.Crouch.canceled += CrouchCancelHandler;
            InputActions.Character.Item0.performed += Item0Handler;
            InputActions.Character.Item1.performed += Item1Handler;
        }

        public void Dispose()
        {
            InputActions.Dispose();
        }

        private void MovementHandler(InputAction.CallbackContext context) =>
            OnMove?.Invoke(context.ReadValue<Vector2>());

        private void LookHandler(InputAction.CallbackContext context) =>
            OnLook?.Invoke(context.ReadValue<Vector2>());

        private void AimHandler(InputAction.CallbackContext context) =>
            OnAim?.Invoke(true);

        private void AimCancelHandler(InputAction.CallbackContext context) =>
            OnAim?.Invoke(false);

        private void FireHandler(InputAction.CallbackContext context) =>
            OnFire?.Invoke(true);

        private void FireCancelHandler(InputAction.CallbackContext context) =>
            OnFire?.Invoke(false);

        private void MeleeHandler(InputAction.CallbackContext context) =>
            OnMelee?.Invoke();

        private void CrouchHandler(InputAction.CallbackContext context) =>
            OnCrouch?.Invoke(true);

        private void CrouchCancelHandler(InputAction.CallbackContext context) =>
            OnCrouch?.Invoke(false);

        private void Item0Handler(InputAction.CallbackContext context) =>
            OnItemChange?.Invoke(0);

        private void Item1Handler(InputAction.CallbackContext context) =>
            OnItemChange?.Invoke(1);
    }
}
