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
        private DiContainer _container;
        private SignalBus _signalBus;
        private UIDocument _uiDocument;
        private Stack<Type> _order; // opened widgets order
        private Dictionary<Type, UIWidget> _widgets; // awailable
        private List<IDisposable> _states;

        [Inject]
        private void Construct(DiContainer container, SignalBus signalBus)
        {
            _container = container;
            _signalBus = signalBus;
            
            _states = new List<IDisposable>();
            ReflectionHelper.FindDerivedTypes<UIStateBase>(Assembly.GetAssembly(GetType()), _container, (_, widget) => _states.Add(widget));
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<ShowWidgetSignal>(OnShowWidgetSignal);
            _signalBus.Subscribe<HideWidgetSignal>(OnHideWidgetSignal);
            _signalBus.Subscribe<UIStepBackSignal>(OnUIStepBackSignal);
            _signalBus.Subscribe<HideAllWidgetsSignal>(OnHideAllWidgetsSignal);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<ShowWidgetSignal>(OnShowWidgetSignal);
            _signalBus.Unsubscribe<HideWidgetSignal>(OnHideWidgetSignal);
            _signalBus.Unsubscribe<UIStepBackSignal>(OnUIStepBackSignal);
            _signalBus.Unsubscribe<HideAllWidgetsSignal>(OnHideAllWidgetsSignal);
            
            _states.ForEach(state => state.Dispose());
        }

        public void SetUIDocument(UIDocument uiDocument)
        {
            _uiDocument = uiDocument;

            _order = new Stack<Type>();
            _widgets = new Dictionary<Type, UIWidget>();
            ReflectionHelper.FindDerivedTypes<UIWidget>(
                Assembly.GetAssembly(GetType()),
                _container,
                (type, widget) => _widgets.Add(type, widget),
                new object[] { _uiDocument } );
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