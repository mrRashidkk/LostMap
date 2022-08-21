using LostMap.BLL.Interfaces;
using LostMap.BLL.Models;
using LostMap.DAL.Interfaces;
using LostMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMap.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        

        public async Task UpdateAccount(AccountDto accountDto, User currentUser)
        {
            currentUser.Name = accountDto.Name;
            currentUser.LastName = accountDto.LastName;
            currentUser.Email = accountDto.Email;
            currentUser.Phone = accountDto.Phone;

            _unitOfWork.UserRepository.Update(currentUser);
            await _unitOfWork.SaveAsync();
        }
    }
}
