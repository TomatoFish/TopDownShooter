using UnityEngine.UIElements;

namespace Game.UI
{
    public abstract class UIWidget
    {
        private readonly VisualElement _documentRoot;
        private VisualElement _widgetRoot;

        protected abstract string WidgetName { get; }
        public abstract bool IsAdditive { get; }

        public UIWidget(UIDocument uiDocument)
        {
            _documentRoot = uiDocument.rootVisualElement;
            
            _widgetRoot = _documentRoot.Q<VisualElement>(WidgetName);
            _widgetRoot = GetVisualElement<VisualElement>(WidgetName);
        }
        
        public bool IsEnabled()
        {
            if (_widgetRoot == null)
                return false;

            return (_widgetRoot.style.display == DisplayStyle.Flex);
        }

        public static void EnableVisualElement(VisualElement visualElement, bool state)
        {
            if (visualElement == null)
                return;

            visualElement.style.display = (state) ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public T GetVisualElement<T>(string elementName) where T : VisualElement
        {
            if (string.IsNullOrEmpty(elementName) || _widgetRoot == null)
                return null;

            return _widgetRoot.Q<T>(elementName);
        }

        public void Show()
        {
            EnableVisualElement(_widgetRoot, true);
            ShowInternal();
            // signal
        }

        public void Hide()
        {
            if (IsEnabled())
            {
                HideInternal();
                EnableVisualElement(_widgetRoot, false);
                // signal
            }
        }

        protected virtual void ShowInternal() { }
        
        protected virtual void HideInternal() { }
    }
}