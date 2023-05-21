using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class PlayerCameraFollow : IFollowComponent
    {
        [Inject] private PlayerView _view;

        private bool _needToFollow = true;
        
        public bool NeedToFollow => _needToFollow;

        public Transform FollowTransform => NeedToFollow ? _view.CameraTarget : null;
    }
}