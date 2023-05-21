using System;
using Zenject;

namespace Game.Level
{
    public class PlayerAnimationComponent : IInitializable, IDisposable, ILateTickable
    {
        [Inject] private Player _model;
        [Inject] private PlayerView _view;
        [Inject] private SignalBus _signalBus;

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerAimSignal>(SetAim);
            _signalBus.Subscribe<PlayerItemChangedSignal>(ItemChanged);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<PlayerAimSignal>(SetAim);
            _signalBus.TryUnsubscribe<PlayerItemChangedSignal>(ItemChanged);
        }

        public void LateTick()
        {
            var relativeMov = _view.RotationTransform.InverseTransformVector(_view.Controller.velocity);

            //view.Animator.SetFloat("Speed", view.Controller.velocity.magnitude / model.WalkSpeed * 100);
            _view.Animator.SetFloat("MoveX", relativeMov.x / (_model.WalkSpeed));
            _view.Animator.SetFloat("MoveY", relativeMov.z / (_model.WalkSpeed));
        }

        private void SetAim(PlayerAimSignal s)
        {
            var isAiming = s.IsAiming;
            _view.Animator.SetBool("IsAiming", isAiming);
        }
        
        private void ItemChanged(PlayerItemChangedSignal s)
        {
            _view.Animator.SetLayerWeight(s.PrevItem.AnimatorLayer, 0f);
            _view.Animator.SetLayerWeight(s.NewItem.AnimatorLayer, 1f);
        }
    }
}