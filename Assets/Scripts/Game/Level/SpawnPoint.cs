using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class SpawnPoint : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        
        [SerializeField] private string _unitId;
        [SerializeField] private string _eventId;
        [SerializeField] private bool _spawnOnStart;

        private Vector3 _spawnPosition;
        private Quaternion _spawnRotation;
        
        private void Awake()
        {
            _spawnPosition = transform.position;
            _spawnRotation = transform.rotation;
            _signalBus.Subscribe<LevelTriggerSignal>(LevelTriggerSignalHandler);
        }

        private void Start()
        {
            if (_spawnOnStart)
                SendSpawnSignal();
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<LevelTriggerSignal>(LevelTriggerSignalHandler);
        }

        private void LevelTriggerSignalHandler(LevelTriggerSignal signal)
        {
            if (_eventId == signal.TriggerId)
                SendSpawnSignal();
        }
        
        private void SendSpawnSignal()
        {
            _signalBus.Fire(new SpawnUnitSignal(_unitId, _spawnPosition, _spawnRotation));
        }
    }
}