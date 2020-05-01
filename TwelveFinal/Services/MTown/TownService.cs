using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MTown
{
    public interface ITownService : IServiceScoped
    {
        Task<Town> Get(Guid Id);
        Task<List<Town>> List(TownFilter townFilter);
    }
    public class TownService : ITownService
    {
        private readonly IUOW UOW;

        public TownService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<List<Town>> List(TownFilter townFilter)
        {
            return await UOW.TownRepository.List(townFilter);
        }

        public async Task<Town> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            Town Town = await UOW.TownRepository.Get(Id);
            return Town;
        }
    }
}
