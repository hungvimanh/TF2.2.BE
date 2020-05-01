using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Services.MUniversity_Majors_Majors;
using TwelveFinal.Services.MSubjectGroup;
using Microsoft.AspNetCore.Authorization;

namespace TwelveFinal.Controller.university_majors
{
    public class University_MajorsController : ApiController
    {
        private IUniversity_MajorsService university_MajorsService;
        public University_MajorsController(IUniversity_MajorsService university_MajorsService)
        {
            this.university_MajorsService = university_MajorsService;
        }

        #region Create
        [Route(AdminRoute.CreateUniversity_Majors), HttpPost]
        public async Task<ActionResult<University_MajorsDTO>> Create([FromBody] University_MajorsDTO university_MajorsDTO)
        {
            if (university_MajorsDTO == null) university_MajorsDTO = new University_MajorsDTO();

            University_Majors university_Majors = ConvertDTOtoBO(university_MajorsDTO);
            university_Majors = await university_MajorsService.Create(university_Majors);

            university_MajorsDTO = new University_MajorsDTO
            {
                MajorsId = university_Majors.MajorsId,
                MajorsCode = university_Majors.MajorsCode,
                MajorsName = university_Majors.MajorsName,
                UniversityId = university_Majors.UniversityId,
                UniversityCode = university_Majors.UniversityCode,
                UniversityName = university_Majors.UniversityName,
                UniversityAddress = university_Majors.UniversityAddress,
                Id = university_Majors.Id,
                Year = university_Majors.Year,
                Errors = university_Majors.Errors
            };
            if (university_Majors.HasError)
                return BadRequest(university_MajorsDTO);
            return Ok(university_MajorsDTO);
        }
        #endregion

        #region Update
        [Route(AdminRoute.UpdateUniversity_Majors), HttpPost]
        public async Task<ActionResult<University_MajorsDTO>> Update([FromBody] University_MajorsDTO university_MajorsDTO)
        {
            if (university_MajorsDTO == null) university_MajorsDTO = new University_MajorsDTO();

            University_Majors university_Majors = ConvertDTOtoBO(university_MajorsDTO);
            university_Majors = await university_MajorsService.Update(university_Majors);

            university_MajorsDTO = new University_MajorsDTO
            {
                Id = university_Majors.Id,
                MajorsId = university_Majors.MajorsId,
                MajorsCode = university_Majors.MajorsCode,
                MajorsName = university_Majors.MajorsName,
                UniversityId = university_Majors.UniversityId,
                UniversityCode = university_Majors.UniversityCode,
                UniversityName = university_Majors.UniversityName,
                UniversityAddress = university_Majors.UniversityAddress,
                Year = university_Majors.Year,
                Errors = university_Majors.Errors
            };
            if (university_Majors.HasError)
                return BadRequest(university_MajorsDTO);
            return Ok(university_MajorsDTO);
        }
        #endregion

        #region Read
        [AllowAnonymous]
        [Route(CommonRoute.GetUniversity_Majors), HttpPost]
        public async Task<University_MajorsDTO> Get([FromBody] University_MajorsDTO university_MajorsDTO)
        {
            if (university_MajorsDTO == null) university_MajorsDTO = new University_MajorsDTO();

            University_Majors university_Majors = ConvertDTOtoBO(university_MajorsDTO);
            university_Majors = await university_MajorsService.Get(university_Majors.Id);

            return university_Majors == null ? null : new University_MajorsDTO()
            {
                Id = university_Majors.Id,
                MajorsId = university_Majors.MajorsId,
                MajorsCode = university_Majors.MajorsCode,
                MajorsName = university_Majors.MajorsName,
                UniversityId = university_Majors.UniversityId,
                UniversityCode = university_Majors.UniversityCode,
                UniversityName = university_Majors.UniversityName,
                UniversityAddress = university_Majors.UniversityAddress,
                Year = university_Majors.Year,
            };
        }

        [AllowAnonymous]
        [Route(CommonRoute.ListUniversity_Majors), HttpPost]
        public async Task<List<University_MajorsDTO>> List([FromBody] University_MajorsFilterDTO university_MajorsFilterDTO)
        {
            University_MajorsFilter university_MajorsFilter = new University_MajorsFilter
            {
                UniversityId = university_MajorsFilterDTO.UniversityId,
                UniversityCode = new StringFilter { StartsWith = university_MajorsFilterDTO.UniversityCode },
                UniversityName = new StringFilter { Contains = university_MajorsFilterDTO.UniversityName },
                MajorsId = university_MajorsFilterDTO.MajorsId,
                MajorsCode = new StringFilter { StartsWith = university_MajorsFilterDTO.MajorsCode },
                MajorsName = new StringFilter { Contains = university_MajorsFilterDTO.MajorsName },
                Year = new StringFilter { Equal = university_MajorsFilterDTO .Year },
                Skip = university_MajorsFilterDTO.Skip,
                Take = int.MaxValue,
                OrderType = OrderType.DESC,
                OrderBy = University_MajorsOrder.MajorsName
            };

            List<University_Majors> universities = await university_MajorsService.List(university_MajorsFilter);

            List<University_MajorsDTO> university_MajorsDTOs = universities.Select(u => new University_MajorsDTO
            {                  
                Id = u.Id,
                MajorsId = u.MajorsId,
                MajorsCode = u.MajorsCode,
                MajorsName = u.MajorsName,
                UniversityId = u.UniversityId,
                UniversityCode = u.UniversityCode,
                UniversityName = u.UniversityName,
                UniversityAddress = u.UniversityAddress,
                Year = u.Year,
            }).ToList();

            return university_MajorsDTOs;
        }
        #endregion

        #region Delete
        [Route(AdminRoute.DeleteUniversity_Majors), HttpPost]
        public async Task<ActionResult<University_MajorsDTO>> Delete([FromBody] University_MajorsDTO university_MajorsDTO)
        {
            if (university_MajorsDTO == null) university_MajorsDTO = new University_MajorsDTO();

            University_Majors university_Majors = ConvertDTOtoBO(university_MajorsDTO);
            university_Majors = await university_MajorsService.Delete(university_Majors);

            university_MajorsDTO = new University_MajorsDTO
            {
                Id = university_Majors.Id,
                MajorsId = university_Majors.MajorsId,
                MajorsCode = university_Majors.MajorsCode,
                MajorsName = university_Majors.MajorsName,
                UniversityId = university_Majors.UniversityId,
                UniversityCode = university_Majors.UniversityCode,
                UniversityName = university_Majors.UniversityName,
                UniversityAddress = university_Majors.UniversityAddress,
                Errors = university_Majors.Errors
            };
            if (university_Majors.HasError)
                return BadRequest(university_MajorsDTO);
            return Ok(university_MajorsDTO);
        }
        #endregion

        private University_Majors ConvertDTOtoBO(University_MajorsDTO university_MajorsDTO)
        {
            University_Majors University_Majors = new University_Majors
            {
                Id = university_MajorsDTO.Id,
                MajorsId = university_MajorsDTO.MajorsId,
                MajorsCode = university_MajorsDTO.MajorsCode,
                MajorsName = university_MajorsDTO.MajorsName,
                UniversityId = university_MajorsDTO.UniversityId,
                UniversityCode = university_MajorsDTO.UniversityCode,
                UniversityName = university_MajorsDTO.UniversityName,
                UniversityAddress = university_MajorsDTO.UniversityAddress,
                Year = university_MajorsDTO.Year
            };
            return University_Majors;
        }
    }
}
