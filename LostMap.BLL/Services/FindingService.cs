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
    public class FindingService : IFindingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FindingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(FindingDto findingDto, User creator)
        {
            var finding = new Finding
            {
                Title = findingDto.Title,
                Description = findingDto.Description,
                Found = findingDto.Found,
                Latitude = findingDto.Latitude,
                Longitude = findingDto.Longitude,
                Created = DateTime.Now,
                CreatorId = creator.Id
            };

            _unitOfWork.FindingRepository.Create(finding);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<FindingDto>> GetAll()
        {
            var allFindings = await _unitOfWork.FindingRepository.GetAsync(useTracking: false);
            return allFindings.Select(f => new FindingDto 
            {
                Id = f.Id,
                Title = f.Title,
                Latitude = f.Latitude,
                Longitude = f.Longitude
            }).ToList();
        }

        public async Task<FindingDto> Get(Guid id)
        {
            var finding = await _unitOfWork.FindingRepository.GetSingleAsync(f => f.Id == id);
            return FindingDto.Create(finding);
        }
    }
}
