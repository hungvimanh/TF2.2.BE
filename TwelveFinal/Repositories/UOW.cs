using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Common;
using TwelveFinal.Repositories.Models;
using Z.EntityFramework.Extensions;

namespace TwelveFinal.Repositories
{
    public interface IUOW : IServiceScoped
    {
        Task Begin();
        Task Commit();
        Task Rollback();
        IDistrictRepository DistrictRepository { get; }
        IEthnicRepository EthnicRepository { get; }
        IFormRepository FormRepository { get; }
        IHighSchoolRepository HighSchoolRepository { get; }
        IMajorsRepository MajorsRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        ITownRepository TownRepository { get; }
        ISubjectGroupRepository SubjectGroupRepository { get; }
        IStudentRepository StudentRepository { get; }
        IUniversity_MajorsRepository University_MajorsRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        IUniversity_Majors_SubjectGroupRepository University_Majors_SubjectGroupRepository { get; }
        IUserRepository UserRepository { get; }
    }
    public class UOW : IUOW
    { 
        private TFContext tFContext;
        public IDistrictRepository DistrictRepository { get; private set; }
        public IEthnicRepository EthnicRepository { get; private set; }
        public IFormRepository FormRepository { get; private set; }
        public IHighSchoolRepository HighSchoolRepository { get; private set; }
        public IMajorsRepository MajorsRepository { get; private set; }
        public IProvinceRepository ProvinceRepository { get; private set; }
        public ITownRepository TownRepository { get; private set; }
        public ISubjectGroupRepository SubjectGroupRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public IUniversity_MajorsRepository University_MajorsRepository { get; private set; }
        public IUniversity_Majors_SubjectGroupRepository University_Majors_SubjectGroupRepository { get; private set; }
        public IUniversityRepository UniversityRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public UOW(TFContext _tFContext, ICurrentContext currentContext)
        {
            tFContext = _tFContext;
            DistrictRepository = new DistrictRepository(tFContext);
            EthnicRepository = new EthnicRepository(tFContext);
            FormRepository = new FormRepository(tFContext, currentContext);
            HighSchoolRepository = new HighSchoolRepository(tFContext);
            MajorsRepository = new MajorsRepository(tFContext);
            ProvinceRepository = new ProvinceRepository(tFContext);
            TownRepository = new TownRepository(tFContext);
            SubjectGroupRepository = new SubjectGroupRepository(tFContext);
            StudentRepository = new StudentRepository(tFContext, currentContext);
            University_MajorsRepository = new University_MajorsRepository(tFContext);
            University_Majors_SubjectGroupRepository = new University_Majors_SubjectGroupRepository(tFContext);
            UniversityRepository = new UniversityRepository(tFContext);
            UserRepository = new UserRepository(tFContext);
            EntityFrameworkManager.ContextFactory = DbContext => tFContext;
        }

        public async Task Begin()
        {
            await tFContext.Database.BeginTransactionAsync();
        }

        public Task Commit()
        {
            tFContext.Database.CommitTransaction();
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            tFContext.Database.RollbackTransaction();
            return Task.CompletedTask;
        }
    }
}
