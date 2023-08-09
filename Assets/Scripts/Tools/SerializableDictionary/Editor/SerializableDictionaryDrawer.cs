using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tools.Editor
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
    public class SerializableDictionaryDrawer : PropertyDrawer
    {
        private SerializedProperty _property;
        private SerializedProperty _pairs;

        private object _list;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _property = property.Copy();
            FindProperties(_property);
            
            var listViewElement = new ListView(_list as IList, EditorGUIUtility.singleLineHeight, AddItem, BindItem)
                {
                    reorderable = true,
                    reorderMode = ListViewReorderMode.Animated,
                    showAddRemoveFooter = true,
                    showBorder = true,
                    showFoldoutHeader = true,
                    headerTitle = property.name
                };
            listViewElement.fixedItemHeight = EditorGUIUtility.singleLineHeight;
            listViewElement.itemsAdded += OnItemsAdded;
            listViewElement.itemsRemoved += OnItemsRemoved;
            listViewElement.itemIndexChanged += OnIndexChanged;

            return listViewElement;
        }

        private void FindProperties(SerializedProperty property)
        {
            _pairs = property.FindPropertyRelative("_pairs");
            var pairsObject = GetTargetObjectOfProperty(_pairs);
            _list = pairsObject;
        }

        private VisualElement AddItem()
        {
            var elementSerializedProperty = _pairs.GetArrayElementAtIndex(0);
            var keyProperty = elementSerializedProperty.FindPropertyRelative("_key");
            var valueProperty = elementSerializedProperty.FindPropertyRelative("_value");
            
            var keyElement = new PropertyField(keyProperty, "");
            keyElement.style.width = new StyleLength(Length.Percent(30));
            keyElement.name = "key";
            
            var viewElement = new PropertyField(valueProperty, "");
            viewElement.style.width = new StyleLength(Length.Percent(70));
            viewElement.name = "value";

            var height = Mathf.Max(keyElement.style.height.value.value, viewElement.style.height.value.value, EditorGUIUtility.singleLineHeight);
            var root = new VisualElement();
            root.style.height = new StyleLength(height);
            root.style.flexDirection = FlexDirection.Row;
            
            root.Add(keyElement);
            root.Add(viewElement);
            
            return root;
        }

        private void BindItem(VisualElement element, int index)
        {
            if (_pairs.arraySize - 1 < index)
            {
                return;
            }
            
            var keyElement = element.Q<PropertyField>("key");
            keyElement.BindProperty(_pairs.GetArrayElementAtIndex(index).FindPropertyRelative("_key"));
            
            var valueElement = element.Q<PropertyField>("value");
            valueElement.BindProperty(_pairs.GetArrayElementAtIndex(index).FindPropertyRelative("_value"));
        }

        private void OnItemsAdded(IEnumerable<int> indexes)
        {
            var obj = GetTargetObjectOfProperty(_property);
            var methodInfo = obj.GetType().GetMethod("Add");
            var parametersArray = new object[] {default, default};
            methodInfo.Invoke(obj, parametersArray);
        }
        
        private void OnItemsRemoved(IEnumerable<int> indexes)
        {
            foreach (var index in indexes)
            {
                var keyValueSerializedProperty = _pairs.GetArrayElementAtIndex(index);
                var keySerializedProperty = keyValueSerializedProperty.FindPropertyRelative("_key");
                var key = GetTargetObjectOfProperty(keySerializedProperty);
                
                var obj = GetTargetObjectOfProperty(_property);
                var methodInfo = obj.GetType().GetMethod("Remove");
                var parametersArray = new object[] { key };
                methodInfo.Invoke(obj, parametersArray);
            }
        }

        private void OnIndexChanged(int from, int to)
        {
            
        }
        
        public static object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            if (prop == null) return null;

            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }
        
        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }
        
        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
        
            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }
    }
}