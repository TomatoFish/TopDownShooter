using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Logic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.UI
{
    public class UIManager : IInitializable, IDisposable
    {
        private const GameUIState InitialState = GameUIState.None;

        private DiContainer _container;
        private SignalBus _signalBus;
        private UIState _uiState;
        private GameObjectDictionaryPool _pool; // widget pool
        private Stack<System.Type> _order; // opened widgets order

        private GameUIState State => _uiState.State;

        [Inject]
        private void Construct(DiContainer container, SignalBus signalBus)
        {
            _container = container;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _uiState = new UIState
            {
                State = InitialState
            };
            
            _signalBus.Subscribe<ChangeUIStateSignal>(OnChangeUIStateSignal);
            _signalBus.Subscribe<ShowWidgetSignal>(OnShowWidgetSignal);
            _signalBus.Subscribe<HideWidgetSignal>(OnHideWidgetSignal);
            _signalBus.Subscribe<UIStepBackSignal>(OnUIStepBackSignal);
            _signalBus.Subscribe<HideAllWidgetsSignal>(OnHideAllWidgetsSignal);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<ChangeUIStateSignal>(OnChangeUIStateSignal);
            _signalBus.Unsubscribe<ShowWidgetSignal>(OnShowWidgetSignal);
            _signalBus.Unsubscribe<HideWidgetSignal>(OnHideWidgetSignal);
            _signalBus.Unsubscribe<UIStepBackSignal>(OnUIStepBackSignal);
            _signalBus.Unsubscribe<HideAllWidgetsSignal>(OnHideAllWidgetsSignal);
            _pool?.Dispose();
        }

        public async Task InitializePool(Transform parentTransform)
        {
            var objects = new Dictionary<System.Type, GameObject>();
            var types = ReflectionHelper.FindDerivedTypes<UIWidget>(Assembly.GetAssembly(GetType()));
            var loadTasks = new List<Task>();
            foreach (var type in types)
            {
                var attributes = type.GetTypeInfo().GetCustomAttributes();
                var widgetAttribute = (UIWidgetAttribute)attributes.FirstOrDefault(attribute => attribute is UIWidgetAttribute);
                if (widgetAttribute != null)
                {
                    var asyncLoad = Addressables.LoadAssetAsync<GameObject>(widgetAttribute.Path);
                    asyncLoad.Completed += handle =>
                    {
                        var newGameObject = _container.InstantiatePrefab(handle.Result, parentTransform);
                        objects.Add(type, newGameObject);
                    };
                    loadTasks.Add(asyncLoad.Task);
                }
            }

            await Task.WhenAll(loadTasks);
            _pool = new GameObjectDictionaryPool(objects);
            _order = new Stack<System.Type>();
        }

        private void OnChangeUIStateSignal(ChangeUIStateSignal signal)
        {
            _uiState.State = signal.State;
        }
        
        private void OnShowWidgetSignal(ShowWidgetSignal signal)
        {
            _order.Push(signal.WidgetType);
            _pool.Get(signal.WidgetType);
        }
        
        private void OnHideWidgetSignal(HideWidgetSignal signal)
        {
            if (!_order.TryPeek(out var lastWidgetType) || lastWidgetType != signal.WidgetType) return;
            HideWidget();
        }

        private void OnUIStepBackSignal(UIStepBackSignal signal)
        {
            HideWidget();
        }

        private void HideWidget()
        {
            if (_order.TryPop(out var typeToHide))
            {
                _pool.Release(typeToHide);
            }
        }

        private void OnHideAllWidgetsSignal(HideAllWidgetsSignal signal)
        {
            for (int i = 0; i < _order.Count; i++)
            {
                HideWidget();
            }
        }
    }
}