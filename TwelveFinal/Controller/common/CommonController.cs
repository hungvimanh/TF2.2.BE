using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MDistrict;
using TwelveFinal.Services.MEthnic;
using TwelveFinal.Services.MHighSchool;
using TwelveFinal.Services.MProvince;
using TwelveFinal.Services.MTown;

namespace TwelveFinal.Controller.common
{
    [AllowAnonymous]
    public class CommonController : ApiController
    {
        private IProvinceService ProvinceService;
        private IDistrictService DistrictService;
        private ITownService TownService;
        private IHighSchoolService HighSchoolService;
        private IEthnicService EthnicService;

        public CommonController(IProvinceService provinceService,
            IDistrictService districtService,
            ITownService townService,
            IHighSchoolService highSchoolService,
            IEthnicService ethnicService)
        {
            ProvinceService = provinceService;
            DistrictService = districtService;
            TownService = townService;
            HighSchoolService = highSchoolService;
            EthnicService = ethnicService;
        }
        #region Province
        [Route(CommonRoute.ListProvince), HttpPost]
        public async Task<List<ProvinceDTO>> ListProvince([FromBody] ProvinceFilterDTO provinceFilterDTO)
        {
            ProvinceFilter provinceFilter = new ProvinceFilter
            {
                Code = new StringFilter { StartsWith = provinceFilterDTO.Code },
                Name = new StringFilter { StartsWith = provinceFilterDTO.Name },
                Skip = provinceFilterDTO.Skip,
                Take = int.MaxValue,
                OrderBy = ProvinceOrder.Name,
                OrderType = OrderType.ASC
            };

            var listProvince = await ProvinceService.List(provinceFilter);
            if (listProvince == null) return null;
            return listProvince.Select(p => new ProvinceDTO
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name
            }).ToList();
        }

        [Route(CommonRoute.GetProvince), HttpPost]
        public async Task<ProvinceDTO> GetProvince([FromBody] ProvinceDTO provinceDTO)
        {
            if (provinceDTO == null) provinceDTO = new ProvinceDTO();
            var province = new Province { Id = provinceDTO.Id };
            province = await ProvinceService.Get(province.Id);

            return province == null ? null : new ProvinceDTO
            {
                Id = province.Id,
                Code = province.Code,
                Name = province.Name
            };
        } 
        #endregion

        #region District
        [Route(CommonRoute.ListDistrict), HttpPost]
        public async Task<List<DistrictDTO>> ListDistrict([FromBody] DistrictFilterDTO districtFilterDTO)
        {
            DistrictFilter districtFilter = new DistrictFilter
            {
                Code = new StringFilter { StartsWith = districtFilterDTO.Code },
                Name = new StringFilter { StartsWith = districtFilterDTO.Name },
                ProvinceId = districtFilterDTO.ProvinceId,
                Skip = districtFilterDTO.Skip,
                Take = int.MaxValue,
                OrderBy = DistrictOrder.Name,
                OrderType = OrderType.ASC
            };

            var listDistrict = await DistrictService.List(districtFilter);
            if (listDistrict == null) return null;
            return listDistrict.Select(p => new DistrictDTO
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                ProvinceId = p.ProvinceId
            }).ToList();
        }

        [Route(CommonRoute.GetDistrict), HttpPost]
        public async Task<DistrictDTO> GetDistrict([FromBody] DistrictDTO districtDTO)
        {
            if (districtDTO == null) districtDTO = new DistrictDTO();
            var district = new District { Id = districtDTO.Id };
            district = await DistrictService.Get(district.Id);

            return district == null ? null : new DistrictDTO
            {
                Id = district.Id,
                Code = district.Code,
                Name = district.Name,
                ProvinceId = district.ProvinceId
            };
        }
        #endregion

        #region Town
        [Route(CommonRoute.ListTown), HttpPost]
        public async Task<List<TownDTO>> ListTown([FromBody] TownFilterDTO townFilterDTO)
        {
            TownFilter townFilter = new TownFilter
            {
                Code = new StringFilter { StartsWith = townFilterDTO.Code },
                Name = new StringFilter { StartsWith = townFilterDTO.Name },
                DistrictId = townFilterDTO.DistrictId,
                Skip = townFilterDTO.Skip,
                Take = int.MaxValue,
                OrderBy = TownOrder.Name,
                OrderType = OrderType.ASC
            };

            var listTown = await TownService.List(townFilter);
            if (listTown == null) return null;
            return listTown.Select(t => new TownDTO
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name,
                DistrictId = t.DistrictId
            }).ToList();
        }

        [Route(CommonRoute.GetTown), HttpPost]
        public async Task<TownDTO> GetTown([FromBody] TownDTO townDTO)
        {
            if (townDTO == null) townDTO = new TownDTO();
            var town = new Town { Id = townDTO.Id };
            town = await TownService.Get(town.Id);

            return town == null ? null : new TownDTO
            {
                Id = town.Id,
                Code = town.Code,
                Name = town.Name,
                DistrictId = town.DistrictId,
            };
        }
        #endregion

        #region HighSchool
        [Route(CommonRoute.ListHighSchool), HttpPost]
        public async Task<List<HighSchoolDTO>> ListHighSchool([FromBody] HighSchoolFilterDTO highSchoolFilterDTO)
        {
            HighSchoolFilter highSchoolFilter = new HighSchoolFilter
            {
                Code = new StringFilter { StartsWith = highSchoolFilterDTO.Code },
                Name = new StringFilter { Contains = highSchoolFilterDTO.Name },
                ProvinceId = highSchoolFilterDTO.ProvinceId,
                Skip = highSchoolFilterDTO.Skip,
                Take = int.MaxValue,
                OrderBy = HighSchoolOrder.Name,
                OrderType = OrderType.ASC
            };

            var listHighSchool = await HighSchoolService.List(highSchoolFilter);
            if (listHighSchool == null) return null;
            return listHighSchool.Select(t => new HighSchoolDTO
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name,
                ProvinceId = t.ProvinceId
            }).ToList();
        }

        [Route(CommonRoute.GetHighSchool), HttpPost]
        public async Task<HighSchoolDTO> GetHighSchool([FromBody] HighSchoolDTO highSchoolDTO)
        {
            if (highSchoolDTO == null) highSchoolDTO = new HighSchoolDTO();
            var highSchool = new HighSchool { Id = highSchoolDTO.Id };
            highSchool = await HighSchoolService.Get(highSchool.Id);

            return highSchool == null ? null : new HighSchoolDTO
            {
                Id = highSchool.Id,
                Code = highSchool.Code,
                Name = highSchool.Name,
                ProvinceId = highSchool.ProvinceId,
            };
        }
        #endregion

        #region DropListEthnic
        [Route(CommonRoute.ListEthnic), HttpPost]
        public async Task<List<EthnicDTO>> ListEthnic([FromBody] EthnicFilterDTO ethnicFilterDTO)
        {
            EthnicFilter ethnicFilter = new EthnicFilter
            {
                Code = new StringFilter { StartsWith = ethnicFilterDTO.Code },
                Name = new StringFilter { StartsWith = ethnicFilterDTO.Name },
                Skip = ethnicFilterDTO.Skip,
                Take = int.MaxValue,
                OrderBy = EthnicOrder.Name,
                OrderType = OrderType.ASC
            };

            var listEthnic = await EthnicService.List(ethnicFilter);
            if (listEthnic == null) return null;
            return listEthnic.Select(e => new EthnicDTO
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name
            }).ToList();
        }

        [Route(CommonRoute.GetEthnic), HttpPost]
        public async Task<EthnicDTO> GetEthnic([FromBody] EthnicDTO ethnicDTO)
        {
            if (ethnicDTO == null) ethnicDTO = new EthnicDTO();
            var ethnic = new Ethnic { Id = ethnicDTO.Id };
            ethnic = await EthnicService.Get(ethnic.Id);

            return ethnic == null ? null : new EthnicDTO
            {
                Id = ethnic.Id,
                Code = ethnic.Code,
                Name = ethnic.Name
            };
        }
        #endregion

    }
}
