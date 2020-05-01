using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MHighSchool
{
    public interface IHighSchoolService : IServiceScoped
    {
        Task<HighSchool> Get(Guid Id);
        Task<List<HighSchool>> List(HighSchoolFilter highSchoolFilter);
    }
    public class HighSchoolService : IHighSchoolService
    {
        private readonly IUOW UOW;

        public HighSchoolService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<List<HighSchool>> List(HighSchoolFilter highSchoolFilter)
        {
            return await UOW.HighSchoolRepository.List(highSchoolFilter);
        }

        public async Task<HighSchool> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            HighSchool HighSchool = await UOW.HighSchoolRepository.Get(Id);
            return HighSchool;
        }
    }
}
