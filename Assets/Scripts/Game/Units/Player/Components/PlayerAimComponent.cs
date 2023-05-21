﻿using Game.Camera;
using Game.Input;
using System;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class PlayerAimComponent : IInitializable, IDisposable, ITickable
    {
        [Inject] private InputController _input;
        [Inject] private Player _model;
        [Inject] private PlayerView _view;
        [Inject] private CameraManager _cameraManager;
        [Inject] private SignalBus _signalBus;
        [Inject] private PlayerInventoryComponent _inventory;

        private Vector2 _lookVector;
        private Vector3 _mousePositionCache;

        public bool IsAiming { get; private set; }

        
        public void Initialize()
        {
            _input.Aim += AimHandler;
            _input.Look += LookHandler;
        }

        public void Dispose()
        {
            _input.Aim -= AimHandler;
            _input.Look -= LookHandler;
        }

        public void Tick()
        {

            //var targetLook = view.MovementTransform.position + view.RotationTransform.forward + Vector3.up * model.Height;
            //if (IsAiming && (targetLook - view.MovementTransform.position).magnitude > 2f)
            //{
            //    targetLook = GetMousePositionRelativeHeight(model.Height, view.MovementTransform.position.y);
            //}

            var targetLook = GetMousePositionRelativeHeight(_inventory.WeaponHeight, _view.MovementTransform.position.y);
            if (IsAiming && (targetLook - _view.MovementTransform.position).magnitude < 1.8)
            {
                targetLook = _view.MovementTransform.position + _view.RotationTransform.forward + Vector3.up * _inventory.WeaponHeight;
            }

            //targetLook = Vector3.Lerp(_view.AimTarget.position, targetLook, 1 / (_view.AimTarget.position - targetLook).magnitude * _model.AimSpeed * Time.deltaTime);
            _view.AimTarget.position = targetLook;
        }

        public Vector3 GetMousePositionRelativeHeight(float height, float floorHeight = 0)
        {
            var cam = _cameraManager.Camera;
            var ray = cam.ScreenPointToRay(_lookVector);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100, Tools.LayerHelper.PointerEnemyHitLayer))
            {
                var hittable = hitInfo.transform.GetComponent<IHittable>();
                if (hittable != null)
                {
                    height = hittable.Position.y - floorHeight;
                }
            }

            if (Physics.Raycast(ray, out hitInfo, 100, Tools.LayerHelper.PointerFloorHitLayer))
            {
                var hitPointToCameraV3 = cam.transform.position - hitInfo.point;
                var hitPointToHeightLength = (height * hitPointToCameraV3.magnitude / (cam.transform.position.y - hitInfo.point.y));
                _mousePositionCache = hitInfo.point + hitPointToCameraV3.normalized * hitPointToHeightLength;
            }

            return _mousePositionCache;
        }

        public Vector3 GetMousePositionRelativeFloor()
        {
            var ray = _cameraManager.Camera.ScreenPointToRay(_lookVector);
            if (Physics.Raycast(ray, out var hitInfo, 100, Tools.LayerHelper.PointerFloorHitLayer))
            {
                return hitInfo.point;
            }

            var relativeHeightPos = GetMousePositionRelativeHeight(_inventory.WeaponHeight);
            relativeHeightPos.y = 0;
            return relativeHeightPos;
        }

        private void AimHandler(bool isAiming)
        {
            //if (GameEnterPoint.Instance.DebugSettings.AimSwitch)
            //{
            //    if (isAiming && !aimSwitched)
            //    {
            //        IsAiming = !IsAiming;
            //        aimSwitched = true;
            //        OnAim.Invoke(IsAiming);
            //    }
            //    else if (!isAiming && aimSwitched)
            //    {
            //        aimSwitched = false;
            //    }
            //}
            //else
            //{
            if (IsAiming != isAiming) _signalBus.TryFire(new PlayerAimSignal(isAiming));
            IsAiming = isAiming;
            //}
        }

        private void LookHandler(Vector2 lookVector)
        {
            _lookVector = lookVector;
        }
    }
}