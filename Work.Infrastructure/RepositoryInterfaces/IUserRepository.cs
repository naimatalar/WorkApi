#nullable enable
using Work.Core;
using Work.Infrastructure.ServicesInterface;

namespace Work.Infrastructure.RepositoryInterfaces
{
    public interface IUserRepository:IRepositoryBase<User>
    {
        User? LoginControl(string key,string password,ICryptoService c);
    }
}
