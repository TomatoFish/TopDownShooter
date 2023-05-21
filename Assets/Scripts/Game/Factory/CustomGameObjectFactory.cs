using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class CustomGameObjectFactory<TP, T> : IFactory<TP, T> where TP : GameObjectFactoryArgs where T : UnityEngine.Component
    {
        protected readonly DiContainer _container;

        public CustomGameObjectFactory(DiContainer container)
        {
            _container = container;
        }
        
        public virtual T Create(TP args)
        {
            if (args.IsOutOfBounds)
            {
                return _container.InstantiatePrefabForComponent<T>(args.Prefab, Vector3.up * 100000, Quaternion.identity, args.Root, args.ExtraArgs);
            }

            return _container.InstantiatePrefabForComponent<T>(args.Prefab, args.Root, args.ExtraArgs);
        }
    }
    
    public class GameObjectFactory<TP, T> : PlaceholderFactory<TP, T> where TP : GameObjectFactoryArgs where T : UnityEngine.Component
    {
    }
}