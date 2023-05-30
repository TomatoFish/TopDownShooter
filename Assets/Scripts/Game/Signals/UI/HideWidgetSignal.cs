namespace Game.UI
{
    public class HideWidgetSignal
    {
        public readonly System.Type WidgetType;

        public HideWidgetSignal(System.Type type)
        {
            WidgetType = type;
        }
    }
}