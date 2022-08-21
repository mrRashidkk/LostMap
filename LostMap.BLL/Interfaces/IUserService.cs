using LostMap.BLL.Models;
using LostMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMap.BLL.Interfaces
{
    public interface IUserService
    {
        Task UpdateAccount(AccountDto accountDto, User currentUser);
    }
}
