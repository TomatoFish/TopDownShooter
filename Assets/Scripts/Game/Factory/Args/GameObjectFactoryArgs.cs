using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Level
{
    public class GameObjectFactoryArgs
    {
        public readonly GameObject Prefab;
        public readonly Transform Root;
        public readonly bool IsOutOfBounds;
        public readonly IEnumerable<object> ExtraArgs;

        public GameObjectFactoryArgs(GameObject prefab, Transform root, bool isOutOfBounds, IEnumerable<object> extraArgs = null)
        {
            Prefab = prefab;
            Root = root;
            IsOutOfBounds = isOutOfBounds;
            ExtraArgs = extraArgs ?? Array.Empty<object>();
        }
    }
}