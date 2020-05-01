using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MEthnic
{
    public interface IEthnicService : IServiceScoped
    {
        Task<Ethnic> Get(Guid Id);
        Task<List<Ethnic>> List(EthnicFilter ethnicFilter);
    }
    public class EthnicService : IEthnicService
    {
        private IUOW UOW;
        public EthnicService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<Ethnic> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            var ethnic = await UOW.EthnicRepository.Get(Id);
            return ethnic;
        }

        public async Task<List<Ethnic>> List(EthnicFilter ethnicFilter)
        {
            return await UOW.EthnicRepository.List(ethnicFilter);
        }
    }
}
