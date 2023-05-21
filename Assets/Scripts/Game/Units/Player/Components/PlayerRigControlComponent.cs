using System;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class PlayerRigControlComponent : IInitializable, IDisposable, ILateTickable
    {
        [Inject] private PlayerRigView _rigView;
        //[Inject] private PlayerInventoryComponent _inventory;
        [Inject] private SignalBus _signalBus;

        private PlayerInventoryComponent _inventory;

        public PlayerRigControlComponent(PlayerInventoryComponent inventory)
        {
            _inventory = inventory;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerAimSignal>(PrepareItem);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<PlayerAimSignal>(PrepareItem);
        }

        public void LateTick()
        {
            SetLeftHandPosition(_inventory.CurrentItem.LeftHandTarget);
        }

        public void SetLeftHandConstraintWeight(float weight)
        {
            weight = Mathf.Clamp01(weight);
            _rigView.LeftHandConstraint.weight = weight;
        }
        
        public void SeHeadConstraintWeight(float weight)
        {
            weight = Mathf.Clamp01(weight);
            _rigView.HeadConstraint.weight = weight;
        }

        public void SetLeftHandPosition(Transform target)
        {
            _rigView.LeftHandTarget.position = target.position;
            _rigView.LeftHandTarget.rotation = target.rotation;
        }

        private void PrepareItem(PlayerAimSignal s)
        {
            SetLeftHandConstraintWeight(s.IsAiming ? 1 : 0);
        }
    }
}
