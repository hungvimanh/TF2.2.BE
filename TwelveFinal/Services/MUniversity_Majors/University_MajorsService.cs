using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversity_Majors_Majors
{
    public interface IUniversity_MajorsService : IServiceScoped
    {
        Task<University_Majors> Create(University_Majors university_Majors);
        Task<University_Majors> Get(Guid Id);
        Task<List<University_Majors>> List(University_MajorsFilter university_MajorsFilter);
        Task<University_Majors> Update(University_Majors university_Majors);
        Task<University_Majors> Delete(University_Majors university_Majors);
    }
    public class University_MajorsService : IUniversity_MajorsService
    {
        private readonly IUOW UOW;
        private readonly IUniversity_MajorsValidator university_MajorsValidator;

        public University_MajorsService(
            IUOW UOW,
            IUniversity_MajorsValidator university_MajorsValidator
            )
        {
            this.UOW = UOW;
            this.university_MajorsValidator = university_MajorsValidator;
        }

        public async Task<University_Majors> Create(University_Majors university_Majors)
        {
            if (!await university_MajorsValidator.Create(university_Majors))
                return university_Majors;

            try
            {
                university_Majors.Id = Guid.NewGuid();
                await UOW.Begin();
                await UOW.University_MajorsRepository.Create(university_Majors);
                await UOW.Commit();
                return await Get(university_Majors.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<University_Majors> Delete(University_Majors university_Majors)
        {
            if (!await university_MajorsValidator.Delete(university_Majors))
                return university_Majors;

            try
            {
                await UOW.Begin();
                await UOW.University_MajorsRepository.Delete(university_Majors.Id);
                await UOW.Commit();
                return university_Majors;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<University_Majors> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            University_Majors University_Majors = await UOW.University_MajorsRepository.Get(Id);
            return University_Majors;
        }

        public async Task<List<University_Majors>> List(University_MajorsFilter university_MajorsFilter)
        {
            return await UOW.University_MajorsRepository.List(university_MajorsFilter);
        }

        public async Task<University_Majors> Update(University_Majors university_Majors)
        {
            if (!await university_MajorsValidator.Update(university_Majors))
                return university_Majors;

            try
            {
                await UOW.Begin();
                await UOW.University_MajorsRepository.Update(university_Majors);
                await UOW.Commit();
                return await Get(university_Majors.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
