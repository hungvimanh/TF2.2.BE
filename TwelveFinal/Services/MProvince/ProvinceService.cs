using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MProvince
{
    public interface IProvinceService : IServiceScoped
    {
        Task<Province> Get(Guid Id);
        Task<List<Province>> List(ProvinceFilter provinceFilter);
    }
    public class ProvinceService : IProvinceService
    {
        private readonly IUOW UOW;

        public ProvinceService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<List<Province>> List(ProvinceFilter provinceFilter)
        {
            return await UOW.ProvinceRepository.List(provinceFilter);
        }

        public async Task<Province> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            Province Province = await UOW.ProvinceRepository.Get(Id);
            return Province;
        }
    }
}
