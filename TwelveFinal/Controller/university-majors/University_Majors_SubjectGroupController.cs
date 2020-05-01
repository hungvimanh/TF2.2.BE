using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Services.MUniversity_Majors;
using TwelveFinal.Entities;
using TwelveFinal.Services.MUniversity_Majors_Majors;

namespace TwelveFinal.Controller.university_majors
{
    public class University_Majors_SubjectGroupController : ApiController
    {
        private IUniversity_Majors_SubjectGroupService University_Majors_SubjectGroupService;
        private IUniversity_MajorsService University_MajorsService;
        public University_Majors_SubjectGroupController(IUniversity_Majors_SubjectGroupService University_Majors_SubjectGroupService, IUniversity_MajorsService University_MajorsService)
        {
            this.University_Majors_SubjectGroupService = University_Majors_SubjectGroupService;
            this.University_MajorsService = University_MajorsService;
        }

        #region Create
        [Route(AdminRoute.CreateUniversity_Majors_SubjectGroup), HttpPost]
        public async Task<ActionResult<University_Majors_SubjectGroupDTO>> Create([FromBody] University_Majors_SubjectGroupDTO university_Majors_SubjectGroupDTO)
        {
            if (university_Majors_SubjectGroupDTO == null) university_Majors_SubjectGroupDTO = new University_Majors_SubjectGroupDTO();

            University_Majors university_Majors = new University_Majors
            {
                MajorsId = university_Majors_SubjectGroupDTO.MajorsId,
                UniversityId = university_Majors_SubjectGroupDTO.UniversityId,
                Year = university_Majors_SubjectGroupDTO.Year
            };
            university_Majors = await University_MajorsService.Create(university_Majors);
            if(university_Majors.Id == Guid.Empty)
            {
                var university_Majors_ = await University_MajorsService.List(new University_MajorsFilter
                {
                    MajorsId = university_Majors.MajorsId,
                    UniversityId = university_Majors.UniversityId,
                    Year = new StringFilter { Equal = university_Majors.Year }
                });
                university_Majors = university_Majors_.FirstOrDefault();
            }

            University_Majors_SubjectGroup university_Majors_SubjectGroup = new University_Majors_SubjectGroup
            {
                SubjectGroupId = university_Majors_SubjectGroupDTO.SubjectGroupId,
                University_MajorsId = university_Majors.Id,
                Benchmark = university_Majors_SubjectGroupDTO.Benchmark,
                Quantity = university_Majors_SubjectGroupDTO.Quantity,
                Note = university_Majors_SubjectGroupDTO.Note
            };
            university_Majors_SubjectGroup = await University_Majors_SubjectGroupService.Create(university_Majors_SubjectGroup);

            university_Majors_SubjectGroupDTO = new University_Majors_SubjectGroupDTO
            {
                MajorsId = university_Majors_SubjectGroup.MajorsId,
                MajorsCode = university_Majors_SubjectGroup.MajorsCode,
                MajorsName = university_Majors_SubjectGroup.MajorsName,
                UniversityId = university_Majors_SubjectGroup.UniversityId,
                UniversityCode = university_Majors_SubjectGroup.UniversityCode,
                UniversityName = university_Majors_SubjectGroup.UniversityName,
                SubjectGroupId = university_Majors_SubjectGroup.SubjectGroupId,
                SubjectGroupCode = university_Majors_SubjectGroup.SubjectGroupCode,  
                SubjectGroupName = university_Majors_SubjectGroup.SubjectGroupName,
                University_MajorsId = university_Majors_SubjectGroup.University_MajorsId,
                Quantity = university_Majors_SubjectGroup.Quantity,
                Note = university_Majors_SubjectGroup.Note,
                Benchmark = university_Majors_SubjectGroup.Benchmark,
                Id = university_Majors_SubjectGroup.Id,
                Year = university_Majors_SubjectGroup.Year,
                Errors = university_Majors_SubjectGroup.Errors
            };
            if (university_Majors_SubjectGroup.HasError)
                return BadRequest(university_Majors_SubjectGroupDTO);
            return Ok(university_Majors_SubjectGroupDTO);
        }
        #endregion

        #region Read
        [AllowAnonymous]
        [Route(CommonRoute.GetUniversity_Majors_SubjectGroup), HttpPost]
        public async Task<University_Majors_SubjectGroupDTO> Get([FromBody] University_Majors_SubjectGroupDTO University_Majors_SubjectGroupDTO)
        {
            if (University_Majors_SubjectGroupDTO == null) University_Majors_SubjectGroupDTO = new University_Majors_SubjectGroupDTO();

            University_Majors_SubjectGroup University_Majors_Subject = new University_Majors_SubjectGroup { Id = University_Majors_SubjectGroupDTO.Id };
            University_Majors_Subject = await University_Majors_SubjectGroupService.Get(University_Majors_Subject.Id);

            return University_Majors_Subject == null ? null : new University_Majors_SubjectGroupDTO()
            {
                Id = University_Majors_Subject.Id,
                MajorsId = University_Majors_Subject.MajorsId,
                MajorsCode = University_Majors_Subject.MajorsCode,
                MajorsName = University_Majors_Subject.MajorsName,
                UniversityId = University_Majors_Subject.UniversityId,
                UniversityCode = University_Majors_Subject.UniversityCode,
                UniversityName = University_Majors_Subject.UniversityName,
                Year = University_Majors_Subject.Year,
                SubjectGroupId = University_Majors_Subject.SubjectGroupId,
                SubjectGroupCode = University_Majors_Subject.SubjectGroupCode,
                SubjectGroupName = University_Majors_Subject.SubjectGroupName,
                Benchmark = University_Majors_Subject.Benchmark,
                Quantity = University_Majors_Subject.Quantity,
                University_MajorsId = University_Majors_Subject.University_MajorsId,
                Note = University_Majors_Subject.Note
            };
        }

        [AllowAnonymous]
        [Route(CommonRoute.ListUniversity_Majors_SubjectGroup), HttpPost]
        public async Task<List<University_Majors_SubjectGroupDTO>> List([FromBody] University_Majors_SubjectGroupFilterDTO University_Majors_SubjectFilterDTO)
        {
            University_Majors_SubjectGroupFilter University_Majors_SubjectFilter = new University_Majors_SubjectGroupFilter
            {
                University_MajorsId = University_Majors_SubjectFilterDTO.University_MajorsId,
                UniversityId = University_Majors_SubjectFilterDTO.UniversityId,
                UniversityCode = new StringFilter { StartsWith = University_Majors_SubjectFilterDTO.UniversityCode },
                UniversityName = new StringFilter { Contains = University_Majors_SubjectFilterDTO.UniversityName },
                MajorsId = University_Majors_SubjectFilterDTO.MajorsId,
                MajorsCode = new StringFilter { StartsWith = University_Majors_SubjectFilterDTO.MajorsCode },
                MajorsName = new StringFilter { Contains = University_Majors_SubjectFilterDTO.MajorsName },
                SubjectGroupId = University_Majors_SubjectFilterDTO.SubjectGroupId,
                SubjectGroupCode = new StringFilter { StartsWith = University_Majors_SubjectFilterDTO .SubjectGroupCode },
                Benchmark = new DoubleFilter { LessEqual = University_Majors_SubjectFilterDTO.Benchmark},
                Year = new StringFilter { Equal = University_Majors_SubjectFilterDTO.Year },
                Skip = University_Majors_SubjectFilterDTO.Skip,
                Take = int.MaxValue,
                OrderType = OrderType.DESC,
                OrderBy = University_Majors_SubjectGroupOrder.Benchmark
            };

            List<University_Majors_SubjectGroup> University_Majors_Subjects = await University_Majors_SubjectGroupService.List(University_Majors_SubjectFilter);

            List<University_Majors_SubjectGroupDTO> University_Majors_SubjectDTOs = University_Majors_Subjects.Select(u => new University_Majors_SubjectGroupDTO
            {
                Id = u.Id,
                MajorsId = u.MajorsId,
                MajorsCode = u.MajorsCode,
                MajorsName = u.MajorsName,
                UniversityId = u.UniversityId,
                UniversityCode = u.UniversityCode,
                UniversityName = u.UniversityName,
                University_MajorsId = u.University_MajorsId,
                SubjectGroupId = u.SubjectGroupId,
                SubjectGroupCode = u.SubjectGroupCode,
                SubjectGroupName = u.SubjectGroupName,
                Benchmark = u.Benchmark,
                Quantity = u.Quantity,
                Note = u.Note,
                Year = u.Year
            }).ToList();

            return University_Majors_SubjectDTOs;
        }
        #endregion

        #region Delete
        [Route(AdminRoute.DeleteUniversity_Majors_SubjectGroup), HttpPost]
        public async Task<ActionResult<University_Majors_SubjectGroupDTO>> Delete([FromBody] University_Majors_SubjectGroupDTO university_Majors_SubjectGroupDTO)
        {
            if (university_Majors_SubjectGroupDTO == null) university_Majors_SubjectGroupDTO = new University_Majors_SubjectGroupDTO();

            University_Majors_SubjectGroup university_Majors_SubjectGroup = ConvertDTOtoBO(university_Majors_SubjectGroupDTO);
            university_Majors_SubjectGroup = await University_Majors_SubjectGroupService.Delete(university_Majors_SubjectGroup);

            university_Majors_SubjectGroupDTO = new University_Majors_SubjectGroupDTO
            {
                MajorsId = university_Majors_SubjectGroup.MajorsId,
                MajorsCode = university_Majors_SubjectGroup.MajorsCode,
                MajorsName = university_Majors_SubjectGroup.MajorsName,
                UniversityId = university_Majors_SubjectGroup.UniversityId,
                UniversityCode = university_Majors_SubjectGroup.UniversityCode,
                UniversityName = university_Majors_SubjectGroup.UniversityName,
                SubjectGroupId = university_Majors_SubjectGroup.SubjectGroupId,
                SubjectGroupCode = university_Majors_SubjectGroup.SubjectGroupCode,
                SubjectGroupName = university_Majors_SubjectGroup.SubjectGroupName,
                University_MajorsId = university_Majors_SubjectGroup.University_MajorsId,
                Quantity = university_Majors_SubjectGroup.Quantity,
                Note = university_Majors_SubjectGroup.Note,
                Benchmark = university_Majors_SubjectGroup.Benchmark,
                Id = university_Majors_SubjectGroup.Id,
                Year = university_Majors_SubjectGroup.Year,
                Errors = university_Majors_SubjectGroup.Errors
            };
            if (university_Majors_SubjectGroup.HasError)
                return BadRequest(university_Majors_SubjectGroupDTO);
            return Ok(university_Majors_SubjectGroupDTO);
        }
        #endregion

        private University_Majors_SubjectGroup ConvertDTOtoBO(University_Majors_SubjectGroupDTO university_Majors_SubjectGroupDTO)
        {
            University_Majors_SubjectGroup university_Majors_SubjectGroup = new University_Majors_SubjectGroup
            {
                Id = university_Majors_SubjectGroupDTO.Id,
                MajorsId = university_Majors_SubjectGroupDTO.MajorsId,
                MajorsCode = university_Majors_SubjectGroupDTO.MajorsCode,
                MajorsName = university_Majors_SubjectGroupDTO.MajorsName,
                UniversityId = university_Majors_SubjectGroupDTO.UniversityId,
                UniversityCode = university_Majors_SubjectGroupDTO.UniversityCode,
                UniversityName = university_Majors_SubjectGroupDTO.UniversityName,
                SubjectGroupId = university_Majors_SubjectGroupDTO.SubjectGroupId,
                SubjectGroupCode = university_Majors_SubjectGroupDTO.SubjectGroupCode,
                SubjectGroupName = university_Majors_SubjectGroupDTO.SubjectGroupName,
                University_MajorsId = university_Majors_SubjectGroupDTO.University_MajorsId,
                Benchmark = university_Majors_SubjectGroupDTO.Benchmark,
                Note = university_Majors_SubjectGroupDTO.Note,
                Quantity = university_Majors_SubjectGroupDTO.Quantity,
                Year = university_Majors_SubjectGroupDTO.Year
            };
            return university_Majors_SubjectGroup;
        }
    }
}
