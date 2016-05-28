using medis.Api.Models;
using System.Threading.Tasks;

namespace medis.Api.Interfaces.Repositories
{
    public interface ISecondaryRepository<T>: IRepository<T> where T : SecondaryEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<bool> RemoveAsync(int id);
    }
}
