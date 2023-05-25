using System;

namespace Game
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIWidgetAttribute : Attribute
    {
        public string Path { get; private set; }

        public UIWidgetAttribute(string path)
        {
            Path = path;
        }
    }
}