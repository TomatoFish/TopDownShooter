using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Tools.Editor
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>.KeyValuePairSerializable))]
    public class KeyValuePairSerializableDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var keyProperty = property.FindPropertyRelative("_key");
            var valueProperty = property.FindPropertyRelative("_value");
            
            var keyElement = new PropertyField(keyProperty, "");
            keyElement.style.width = new StyleLength(Length.Percent(30));
            
            var viewElement = new PropertyField(valueProperty, "");
            viewElement.style.width = new StyleLength(Length.Percent(70));
            
            var root = new VisualElement();
            root.style.flexDirection = FlexDirection.Row;
            
            root.Add(keyElement);
            root.Add(viewElement);
            
            return root;
        }
    }
}