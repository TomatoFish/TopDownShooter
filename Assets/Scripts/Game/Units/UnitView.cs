using Logic.Level;
using UnityEngine;

namespace Game.Level
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        public object Object => gameObject;
    }
}