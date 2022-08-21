using LostMap.BLL.Interfaces;
using LostMap.BLL.Models;
using LostMap.DAL.InitData;
using LostMap.DAL.Interfaces;
using LostMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMap.BLL.Services
{
    public class LossService : ILossService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LossService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(LossDto lossDto, User creator)
        {
            var loss = new Loss
            {
                Title = lossDto.Title,
                Description = lossDto.Description,
                Lost = lossDto.Lost,
                Latitude = lossDto.Latitude,
                Longitude = lossDto.Longitude,
                Created = DateTime.Now,
                CreatorId = creator.Id,
                StatusId = StatusData.Ids.Active
            };

            _unitOfWork.LossRepository.Create(loss);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<LossDto>> GetAll()
        {
            var allLosses = await _unitOfWork.LossRepository.GetAsync(useTracking: false);
            return allLosses.Select(loss => new LossDto 
            {
                Id = loss.Id,
                Title = loss.Title,
                Latitude = loss.Latitude,
                Longitude = loss.Longitude
            }).ToList();
        }

        public async Task<LossDto> Get(Guid id)
        {
            var loss = await _unitOfWork.LossRepository.GetSingleAsync(loss => loss.Id == id);
            return LossDto.Create(loss);
        }
    }
}
