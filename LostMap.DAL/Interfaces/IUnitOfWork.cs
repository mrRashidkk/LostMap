using LostMap.DAL.Models;

namespace LostMap.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<Permission> PermissionRepository { get; }
        IRepository<Loss> LossRepository { get; }
        IRepository<Finding> FindingRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
