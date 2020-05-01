using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Services.MMajors;
using Microsoft.AspNetCore.Authorization;

namespace TwelveFinal.Controller.majors
{
    public class MajorsController : ApiController
    {
        private IMajorsService MajorsService;

        public MajorsController(IMajorsService majorsService)
        {
            this.MajorsService = majorsService;
        }

        #region Create
        [Route(AdminRoute.CreateMajors), HttpPost]
        public async Task<ActionResult<MajorsDTO>> Create([FromBody] MajorsDTO majorsDTO)
        {
            if (majorsDTO == null) majorsDTO = new MajorsDTO();

            Majors majors = ConvertDTOtoBO(majorsDTO);
            majors = await MajorsService.Create(majors);

            majorsDTO = new MajorsDTO
            {
                Id = majors.Id,
                Code = majors.Code,
                Name = majors.Name,
                Errors = majors.Errors
            };
            if (majors.HasError)
                return BadRequest(majorsDTO);
            return Ok(majorsDTO);
        }
        #endregion

        #region Update
        [Route(AdminRoute.UpdateMajors), HttpPost]
        public async Task<ActionResult<MajorsDTO>> Update([FromBody] MajorsDTO majorsDTO)
        {
            if (majorsDTO == null) majorsDTO = new MajorsDTO();

            Majors majors = ConvertDTOtoBO(majorsDTO);
            majors = await MajorsService.Update(majors);

            majorsDTO = new MajorsDTO
            {
                Id = majors.Id,
                Code = majors.Code,
                Name = majors.Name,
                Errors = majors.Errors
            };
            if (majors.HasError)
                return BadRequest(majorsDTO);
            return Ok(majorsDTO);
        }
        #endregion

        #region Read
        [AllowAnonymous]
        [Route(CommonRoute.GetMajors), HttpPost]
        public async Task<MajorsDTO> Get([FromBody] MajorsDTO MajorsDTO)
        {
            if (MajorsDTO == null) MajorsDTO = new MajorsDTO();

            Majors majors = ConvertDTOtoBO(MajorsDTO);
            majors = await MajorsService.Get(majors.Id);

            return majors == null ? null : new MajorsDTO()
            {
                Id = majors.Id,
                Code = majors.Code,
                Name = majors.Name,
                Errors = majors.Errors
            };
        }

        [AllowAnonymous]
        [Route(CommonRoute.ListMajors), HttpPost]
        public async Task<List<MajorsDTO>> List([FromBody] MajorsFilterDTO majorsFilterDTO)
        {
            MajorsFilter majorsFilter = new MajorsFilter
            {
                Code = new StringFilter { StartsWith = majorsFilterDTO.Code },
                Name = new StringFilter { Contains = majorsFilterDTO.Name },
                Skip = majorsFilterDTO.Skip,
                Take = int.MaxValue,
                OrderBy = MajorsOrder.Name,
                OrderType = OrderType.ASC
            };

            List<Majors> universities = await MajorsService.List(majorsFilter);

            List<MajorsDTO> majorsDTOs = universities.Select(u => new MajorsDTO
            {
                Id = u.Id,
                Code = u.Code,
                Name = u.Name
            }).ToList();

            return majorsDTOs;
        }
        #endregion

        #region Delete
        [Route(AdminRoute.DeleteMajors), HttpPost]
        public async Task<ActionResult<MajorsDTO>> Delete([FromBody] MajorsDTO majorsDTO)
        {
            if (majorsDTO == null) majorsDTO = new MajorsDTO();

            Majors majors = ConvertDTOtoBO(majorsDTO);
            majors = await MajorsService.Delete(majors);

            majorsDTO = new MajorsDTO
            {
                Id = majors.Id,
                Code = majors.Code,
                Name = majors.Name,
                Errors = majors.Errors
            };
            if (majors.HasError)
                return BadRequest(majorsDTO);
            return Ok(majorsDTO);
        }
        #endregion

        private Majors ConvertDTOtoBO(MajorsDTO majorsDTO)
        {
            Majors majors = new Majors
            {
                Id = majorsDTO.Id ?? Guid.Empty,
                Code = majorsDTO.Code,
                Name = majorsDTO.Name,
            };
            return majors;
        }
    }
}
