using Game.Settings;
using Logic.Level;

namespace Game.Level
{
    public class Enemy : Unit<EnemyView, UnitSettings>
    {
        public Enemy(EnemyView view, UnitSettings settings) : base(view, settings)
        {
        }
    }
}
