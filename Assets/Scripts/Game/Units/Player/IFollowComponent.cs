using UnityEngine;

namespace Game.Level
{
    public interface IFollowComponent
    {
        bool NeedToFollow { get; }
        Transform FollowTransform { get; }
    }
}