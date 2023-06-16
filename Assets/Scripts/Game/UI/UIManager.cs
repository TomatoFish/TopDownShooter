using System;
using System.Collections.Generic;
using System.Reflection;
using Logic;
using UnityEngine.UIElements;
using Zenject;

namespace Game.UI
{
    public class UIManager : IInitializable, IDisposable
    {
        private const GameUIState InitialState = GameUIState.None;

        private DiContainer _container;
        private SignalBus _signalBus;
        private UIDocument _uiDocument;
        private UIState _uiState;
        private Stack<System.Type> _order; // opened widgets order
        private Dictionary<System.Type, UIWidget> _widgets; // awailable

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
        }

        public void SetUIDocument(UIDocument uiDocument)
        {
            _uiDocument = uiDocument;

            _order = new Stack<System.Type>();
            _widgets = new Dictionary<Type, UIWidget>();
            var types = ReflectionHelper.FindDerivedTypes<UIWidget>(Assembly.GetAssembly(GetType()));
            foreach (var type in types)
            {
                var widget = (UIWidget)_container.Instantiate(type, new object[] { _uiDocument });
                _widgets.Add(type, widget);
            }
        }
        
        private void OnChangeUIStateSignal(ChangeUIStateSignal signal)
        {
            _uiState.State = signal.State;
        }
        
        private void OnShowWidgetSignal(ShowWidgetSignal signal)
        {
            if (_widgets.ContainsKey(signal.WidgetType))
            {
                var widgetToShow = _widgets[signal.WidgetType];
                if (!widgetToShow.IsAdditive) HideWidget();
                _widgets[signal.WidgetType].Show();
                _order.Push(signal.WidgetType);
            }
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
                _widgets[typeToHide].Hide();
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