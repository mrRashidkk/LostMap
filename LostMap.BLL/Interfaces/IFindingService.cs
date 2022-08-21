using LostMap.BLL.Models;
using LostMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMap.BLL.Interfaces
{
    public interface IFindingService
    {
        Task Create(FindingDto findingDto, User creator);
        Task<List<FindingDto>> GetAll();
        Task<FindingDto> Get(Guid id);
    }
}
