using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class DistrictInit : CommonInit
    {
        public DistrictInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<DistrictDAO> districts = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(districts);
        }
        private List<DistrictDAO> LoadFromExcel(string path)
        {
            List<DistrictDAO> excelTemplates = new List<DistrictDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[7];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    string provinceCode = worksheet.Cells[i, 3].Value?.ToString();
                    string districtCode = worksheet.Cells[i, 1].Value?.ToString();
                    string districtName = worksheet.Cells[i, 2].Value?.ToString();
                    DistrictDAO excelTemplate = new DistrictDAO()
                    {
                        Id = CreateGuid("District" + provinceCode + districtCode),
                        ProvinceId = CreateGuid("Province" + worksheet.Cells[i, 3].Value?.ToString()),
                        Code = districtCode,
                        Name = districtName
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}
