using System.Threading.Tasks;

namespace AmecFWUPI.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}