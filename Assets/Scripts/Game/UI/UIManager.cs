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
        public GameObjectDictionaryPool _pool;

        private Transform _parentTransform;

        [Inject]
        public void Construct(Transform parentTransform)
        {
            _parentTransform = parentTransform;
        }
        
        public async void Initialize()
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
                        var newGameObject = UnityEngine.Object.Instantiate(handle.Result, _parentTransform);
                        objects.Add(type, newGameObject);
                    };
                    loadTasks.Add(asyncLoad.Task);
                }
            }

            await Task.WhenAll(loadTasks);
            _pool = new GameObjectDictionaryPool(objects);
        }

        public void Dispose()
        {
            _pool?.Dispose();
        }
    }
}