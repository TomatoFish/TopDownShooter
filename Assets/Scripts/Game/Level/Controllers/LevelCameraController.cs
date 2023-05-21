using Game.Camera;
using System;
using Zenject;

namespace Game.Level
{
    public class LevelCameraController : IInitializable, IDisposable
    {
        [Inject] private CameraManager _cameraManager;
        [Inject] private SignalBus _signalBus;

        private IFollowComponent _playerFollowComponent;

        public void Initialize()
        {
            _signalBus.Subscribe<UnitSpawnedSignal>(PlayerSpawnedHandler);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<UnitSpawnedSignal>(PlayerSpawnedHandler);
        }

        private void PlayerSpawnedHandler(UnitSpawnedSignal unitSpawned)
        {
            if (unitSpawned.Unit.UnitView is PlayerView playerView)
                _cameraManager.SetFollowTarget(playerView.CameraTarget);
        }
    }
}