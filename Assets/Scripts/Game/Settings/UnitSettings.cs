using Logic.Settings;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Settings/UnitSettings")]
    public class UnitSettings : ScriptableObject, IUnitSettings
    {
        [SerializeField] private UnitBaseProperties _baseProperties;
        [SerializeField] private UnitMoveProperties _moveProperties;

        public IUnitBaseProperties BaseProperties => _baseProperties;
        public IUnitMoveProperties MoveProperties => _moveProperties;
    }
}