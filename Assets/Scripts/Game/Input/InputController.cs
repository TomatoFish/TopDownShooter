using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Game.Input
{
    public class InputController : IDisposable
    {
        public Action<Vector2> Move;
        public Action<Vector2> Look;
        public Action<bool> Aim;
        public Action<bool> Fire;
        public Action<bool> Crouch;
        public Action<bool> Slowdown;
        public Action Melee;
        public Action<int> ItemChange;

        public PlayerInputActions InputActions { get; private set; }

        public InputController()
        {
            InputActions = new PlayerInputActions();
            InputActions.Character.Movement.performed += MovementHandler;
            InputActions.Character.Look.performed += LookHandler;
            InputActions.Character.Aim.performed += AimHandler;
            InputActions.Character.Aim.canceled += AimCancelHandler;
            InputActions.Character.Fire.performed += FireHandler;
            InputActions.Character.Fire.canceled += FireCancelHandler;
            InputActions.Character.Melee.performed += MeleeHandler;
            InputActions.Character.Crouch.performed += CrouchHandler;
            InputActions.Character.Crouch.canceled += CrouchCancelHandler;
            InputActions.Character.Slowdown.performed += SlowdownHandler;
            InputActions.Character.Slowdown.canceled += SlowdownCancelHandler;
            InputActions.Character.Item0.performed += Item0Handler;
            InputActions.Character.Item1.performed += Item1Handler;
        }
        
        public void Dispose()
        {
            InputActions.Dispose();
        }

        private void MovementHandler(InputAction.CallbackContext context) =>
            Move?.Invoke(context.ReadValue<Vector2>());

        private void LookHandler(InputAction.CallbackContext context) =>
            Look?.Invoke(context.ReadValue<Vector2>());

        private void AimHandler(InputAction.CallbackContext context) =>
            Aim?.Invoke(true);

        private void AimCancelHandler(InputAction.CallbackContext context) =>
            Aim?.Invoke(false);

        private void FireHandler(InputAction.CallbackContext context) =>
            Fire?.Invoke(true);

        private void FireCancelHandler(InputAction.CallbackContext context) =>
            Fire?.Invoke(false);

        private void MeleeHandler(InputAction.CallbackContext context) =>
            Melee?.Invoke();

        private void CrouchHandler(InputAction.CallbackContext context) =>
            Crouch?.Invoke(true);

        private void CrouchCancelHandler(InputAction.CallbackContext context) =>
            Crouch?.Invoke(false);

        private void SlowdownHandler(InputAction.CallbackContext context) =>
            Slowdown?.Invoke(true);

        private void SlowdownCancelHandler(InputAction.CallbackContext context) =>
            Slowdown?.Invoke(false);

        private void Item0Handler(InputAction.CallbackContext context) =>
            ItemChange?.Invoke(0);

        private void Item1Handler(InputAction.CallbackContext context) =>
            ItemChange?.Invoke(1);
    }
}
