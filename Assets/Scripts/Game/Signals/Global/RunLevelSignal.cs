namespace Game
{
    public class RunLevelSignal
    {
        public readonly LevelType Type;
        public readonly string LevelId;
        
        public RunLevelSignal(LevelType type, string levelId = null)
        {
            Type = type;
            LevelId = levelId;
        }
    }
}