namespace Logic
{
    public static class GuidGenerator
    {
        private static int _lastGuid = 0;

        public static GUID GenerateGuid()
        {
            return new GUID(_lastGuid++);
        }
    }
}