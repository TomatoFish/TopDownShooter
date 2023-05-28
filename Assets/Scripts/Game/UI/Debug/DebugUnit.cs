using Game.Level;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game.UI
{
    [UIWidget("DebugUnit")]
    public class DebugUnit : UIWidget
    {
        [Inject] private SignalBus _signalBus;
        //[Inject] private UnitRegistry _unitRegistry;
        
        private Button _spawnPlayerButton;
        private Button _destroyPlayerButton;

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _spawnPlayerButton = root.Q<Button>("SpawnPlayer");
            _destroyPlayerButton = root.Q<Button>("DestroyPlayer");
            
            _spawnPlayerButton.SetEnabled(false);
            _destroyPlayerButton.SetEnabled(false);

            _signalBus.Subscribe<UnitSpawnedSignal>(OnUnitSpawnedHandler);
            //_signalBus.Subscribe<UnitDestroyed>(OnUnitSpawnedHandler);
        }

        private void Start()
        {
            _spawnPlayerButton.clicked += SpawnPlayerButtonHandler;
            _destroyPlayerButton.clicked += DestroyPlayerButtonHandler;
        }

        private void OnDestroy()
        {
            _spawnPlayerButton.clicked -= SpawnPlayerButtonHandler;
            _destroyPlayerButton.clicked -= DestroyPlayerButtonHandler;
            
            _signalBus.Unsubscribe<UnitSpawnedSignal>(OnUnitSpawnedHandler);
        }

        private void OnUnitSpawnedHandler(UnitSpawnedSignal signal)
        {
            
        }

        private void SpawnPlayerButtonHandler()
        {
            _signalBus.Fire(new SpawnUnitSignal("player", new Vector3(0, 0, 0), Quaternion.identity));
            Debug.LogError($"SpawnPlayerButtonHandler");
        }
        
        private void DestroyPlayerButtonHandler()
        {
            /*
            //var registry = _container.Resolve<UnitRegistry>();
                var playerFieldInfo = _unitRegistry.GetType().GetField("_player", BindingFlags.Public | BindingFlags.NonPublic);
                // var a = typeof(UnitRegistry).Assembly;
                // var b = a.GetType("UnitRegistry.Unit");
                
                var player = playerFieldInfo?.GetValue(_unitRegistry);
                var keyFieldInfo = player?.GetType().GetField("key", BindingFlags.NonPublic);
                if (keyFieldInfo?.GetValue(player) is GUID guid)
                {
                    _signalBus.Fire(new DestroyUnitSignal(guid));
                }
            Debug.LogError($"DestroyPlayerButtonHandler - {_unitRegistry}");
            */
        }
    }
}