using System.Collections.Generic;
using UnityEngine;

namespace Game.Level
{
    public class ItemFactoryArgs : GameObjectFactoryArgs
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        
        public ItemFactoryArgs(GameObject prefab, Transform root, bool isOutOfBounds) : base(prefab, root, isOutOfBounds)
        {
        }
    }
}