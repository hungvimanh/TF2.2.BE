using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MSubjectGroup;

namespace TwelveFinal.Controller.subject_group
{
    public class SubjectGroupController : ApiController
    {
        private ISubjectGroupService SubjectGrupService;
        public SubjectGroupController(ISubjectGroupService SubjectGrupService)
        {
            this.SubjectGrupService = SubjectGrupService;
        }

        #region Create
        [Route(AdminRoute.CreateSubjectGroup), HttpPost]
        public async Task<ActionResult<SubjectGroupDTO>> Create([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            SubjectGroup subjectGroup = ConvertDTOtoBO(subjectGroupDTO);

            subjectGroup = await SubjectGrupService.Create(subjectGroup);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name,
                Errors = subjectGroup.Errors
            };

            if (subjectGroup.HasError)
                return BadRequest(subjectGroupDTO);
            return Ok(subjectGroupDTO);
        }
        #endregion

        #region Read
        [AllowAnonymous]
        [Route(CommonRoute.GetSubjectGroup), HttpPost]
        public async Task<SubjectGroupDTO> Get([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            SubjectGroup subjectGroup = new SubjectGroup { Id = subjectGroupDTO.Id ?? default};

            subjectGroup = await SubjectGrupService.Get(subjectGroup.Id);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name,
                Errors = subjectGroup.Errors
            };
            return subjectGroupDTO;
        }

        [AllowAnonymous]
        [Route(CommonRoute.ListSubjectGroup), HttpPost]
        public async Task<List<SubjectGroupDTO>> List([FromBody] SubjectGroupFilterDTO subjectGroupFilterDTO)
        {
            SubjectGroupFilter filter = new SubjectGroupFilter
            {
                Code = new StringFilter { StartsWith = subjectGroupFilterDTO.Code },
                Name = new StringFilter { Contains = subjectGroupFilterDTO.Name },
                Skip = subjectGroupFilterDTO.Skip,
                Take = int.MaxValue,
                OrderBy = SubjectGroupOrder.Code,
                OrderType = OrderType.ASC
            };

            List<SubjectGroup> subjectGroups = await SubjectGrupService.List(filter);

            List<SubjectGroupDTO> subjectGroupDTOs = subjectGroups.Select(s => new SubjectGroupDTO
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name
            }).ToList();

            return subjectGroupDTOs;
        }
        #endregion

        #region Update
        [Route(AdminRoute.UpdateSubjectGroup), HttpPost]
        public async Task<ActionResult<SubjectGroupDTO>> Update([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            SubjectGroup subjectGroup = ConvertDTOtoBO(subjectGroupDTO);

            subjectGroup = await SubjectGrupService.Update(subjectGroup);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name,
                Errors = subjectGroup.Errors
            };

            if (subjectGroup.HasError)
                return BadRequest(subjectGroupDTO);
            return Ok(subjectGroupDTO);
        }
        #endregion

        #region Delete
        [Route(AdminRoute.DeleteSubjectGroup), HttpPost]
        public async Task<ActionResult<SubjectGroupDTO>> Delete([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            SubjectGroup subjectGroup = new SubjectGroup { Id = subjectGroupDTO.Id ?? default };

            subjectGroup = await SubjectGrupService.Delete(subjectGroup);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name,
                Errors = subjectGroup.Errors
            };

            if (subjectGroup.HasError)
                return BadRequest(subjectGroupDTO);
            return Ok(subjectGroupDTO);
        }
        #endregion

        private SubjectGroup ConvertDTOtoBO(SubjectGroupDTO subjectGroupDTO)
        {
            SubjectGroup subjectGroup = new SubjectGroup
            {
                Id = subjectGroupDTO.Id ?? Guid.Empty,
                Code = subjectGroupDTO.Code,
                Name = subjectGroupDTO.Name
            };

            return subjectGroup;
        }
    }
}
