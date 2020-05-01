using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MDistrict
{
    public interface IDistrictService : IServiceScoped
    {
        Task<District> Get(Guid Id);
        Task<List<District>> List(DistrictFilter districtFilter);
    }
    public class DistrictService : IDistrictService
    {
        private readonly IUOW UOW;

        public DistrictService(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<List<District>> List(DistrictFilter districtFilter)
        {
            return await UOW.DistrictRepository.List(districtFilter);
        }

        public async Task<District> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            District District = await UOW.DistrictRepository.Get(Id);
            return District;
        }
    }
}
