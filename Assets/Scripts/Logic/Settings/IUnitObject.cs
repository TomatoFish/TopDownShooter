using System.Threading.Tasks;

namespace Logic.Settings
{
    public interface IUnitObject
    {
        public Task<object> LoadObject();
    }
}