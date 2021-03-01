using System;
using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player
    {
        public float WalkSpeed;
        public float WalkAcceleration;
        public float RotationSpeed;
        public float BodyRotationAngle;
        public float Height;

        public Action<int, int> OnItemChanged;
        public Action<bool> OnAim;
        public Action<bool> OnFire;
        public Action<bool> OnCrouch;
        public Action OnMelee;
        
        public Vector3 TranslateMov { get; private set; }
        public bool IsAiming { get; private set; }
        public bool IsFiring { get; private set; }
        public bool IsMoving { get; private set; }
        public bool IsCrouching { get; private set; }
        public int CurrentItem { get; private set; }

        private Vector2 moveVector;
        private Vector2 lookVector;
        private PlayerWeaponController weaponController;
        private bool aimSwitched = false;
        
        private readonly Camera camera;
        private readonly InputController input;

        public Player(InputController input)
        {
            this.input = input;
            
            input.OnMove += MoveHandler;
            input.OnLook += LookHandler;
            input.OnAim += AimHandler;
            input.OnFire += FireHandler;
            input.OnCrouch += CrouchHandler;
            input.OnMelee += MeleeHandler;
            input.OnItemChange += ItemChange;
            camera = Camera.main;
        }

        public void SetConfig(float walkSpeed, float walkAcceleration, float rotationSpeed, float bodyRotationAngle, float height)
        {
            WalkSpeed = walkSpeed;
            WalkAcceleration = walkAcceleration;
            RotationSpeed = rotationSpeed;
            BodyRotationAngle = bodyRotationAngle;
            Height = height;
        }

        public void EnableInput()
        {
            input?.InputActions.Enable();
        }

        public void DisableInput()
        {
            input?.InputActions.Disable();
        }

        private void OnDestroy()
        {
            if (input != null)
            {
                input.OnMove -= MoveHandler;
                input.OnLook -= LookHandler;
                input.OnAim -= AimHandler;
                input.OnFire -= FireHandler;
            }
        }

        private void MoveHandler(Vector2 moveVector)
        {
            this.moveVector = moveVector;
            IsMoving = moveVector != Vector2.zero;
        }

        private void LookHandler(Vector2 lookVector)
        {
            this.lookVector = lookVector;
        }

        private void AimHandler(bool isAiming)
        {
            if (GameEnterPoint.Instance.DebugSettings.AimSwitch)
            {
                if (isAiming && !aimSwitched)
                {
                    IsAiming = !IsAiming;
                    aimSwitched = true;
                    OnAim.Invoke(IsAiming);
                }
                else if (!isAiming && aimSwitched)
                {
                    aimSwitched = false;
                }
            }
            else
            {
                if (IsAiming != isAiming) OnAim.Invoke(isAiming);
                IsAiming = isAiming;
            }
        }

        private void FireHandler(bool isFiring)
        {
            if (IsFiring != isFiring) OnFire.Invoke(isFiring);
            IsFiring = isFiring;
        }

        private void CrouchHandler(bool isCrouching)
        {
            if (IsCrouching != isCrouching) OnCrouch.Invoke(isCrouching);
            IsCrouching = isCrouching;
        }

        private void MeleeHandler()
        {
            OnMelee.Invoke();
        }

        public void ItemChange(int itemIndex)
        {
            OnItemChanged?.Invoke(CurrentItem, itemIndex);
            CurrentItem = itemIndex;
        }

        public Vector3 GetMouseLookDirectionWeapon(Transform rotationTransform)
        {
            var weaponScreenPos =
                camera.WorldToScreenPoint(rotationTransform.position +
                                          Vector3.up * weaponController.transform.position.y);
            var playerScreenPosV3 = new Vector3(weaponScreenPos.x, 0, weaponScreenPos.y);
            var lookVectorV3 = new Vector3(lookVector.x, 0, lookVector.y);

            return (lookVectorV3 - playerScreenPosV3);
        }

        public Vector3 GetMouseLookDirection(Transform rotationTransform)
        {
            var playerScreenPos = camera.WorldToScreenPoint(rotationTransform.position + Vector3.up * Height);
            var playerScreenPosV3 = new Vector3(playerScreenPos.x, 0, playerScreenPos.y);
            var lookVectorV3 = new Vector3(lookVector.x, 0, lookVector.y);

            return (lookVectorV3 - playerScreenPosV3).normalized;
        }

        public Vector3 GetMovementTranslate(float deltaTime)
        {
            var v3_mov = new Vector3(moveVector.x, 0, moveVector.y);
            TranslateMov = Vector3.Lerp(TranslateMov, v3_mov, GetSpeed(deltaTime) * WalkAcceleration / Vector3.Distance(TranslateMov, v3_mov));
                
            return TranslateMov;
        }

        public float GetSpeed(float deltaTime) => WalkSpeed * deltaTime;

        public Vector3 GetMousePositionRelativeHeight()
        {
            var ray = camera.ScreenPointToRay(lookVector);
            Vector3 position = Vector3.zero;
            if (Physics.Raycast(ray, out var hitInfo, 100, Tools.LayerHelper.PointerHitLayer))
            {
                var hitPointToCameraV3 = camera.transform.position - hitInfo.point;
                var hitPointToHeightLength = (Height * hitPointToCameraV3.magnitude / camera.transform.position.y);
                position = hitInfo.point + hitPointToCameraV3.normalized * hitPointToHeightLength;
            }

            return position;
        }

        public Vector3 GetMousePositionRelativeFloor()
        {
            var relativeHeightPos = GetMousePositionRelativeHeight();
            relativeHeightPos.y -= Height;
            return relativeHeightPos;
        }
        
        public Vector3 GetCameraTargetPosition(Transform positionTransform, float maxRadiusSqr)
        {
            var direction = GetMousePositionRelativeHeight() - positionTransform.position;
            var distance = direction.magnitude / 2;
            var length = Mathf.Min(maxRadiusSqr, distance);
            var targetPosition = direction.normalized * length;
            
            //translatePosition = Vector3.Lerp(transform.position, targetPosition, TranslateSpeed * Vector3.Distance(translatePosition, targetPosition));
            return targetPosition;
            
            //transform.position = PlayerController.transform.position + direction.normalized * length;
        }
    }
}