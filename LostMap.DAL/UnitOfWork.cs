using LostMap.DAL.Interfaces;
using LostMap.DAL.Models;
using LostMap.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMap.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            UserRepository = new BaseRepository<User>(context);
            RoleRepository = new BaseRepository<Role>(context);
            PermissionRepository = new BaseRepository<Permission>(context);
            LossRepository = new BaseRepository<Loss>(context);
            FindingRepository = new BaseRepository<Finding>(context);
        }       

        public IRepository<User> UserRepository { get; private set; }
        public IRepository<Role> RoleRepository { get; private set; }
        public IRepository<Permission> PermissionRepository { get; private set; }
        public IRepository<Loss> LossRepository { get; private set; }
        public IRepository<Finding> FindingRepository { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
