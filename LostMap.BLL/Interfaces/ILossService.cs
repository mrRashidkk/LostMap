using LostMap.BLL.Models;
using LostMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMap.BLL.Interfaces
{
    public interface ILossService
    {
        Task Create(LossDto lossDto, User creator);
        Task<List<LossDto>> GetAll();
        Task<LossDto> Get(Guid id);
    }
}
