namespace Game.UI
{
    public class ShowWidgetSignal
    {
        public readonly System.Type WidgetType;

        public ShowWidgetSignal(System.Type type)
        {
            WidgetType = type;
        }
    }
}