using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class ProvinceInit : CommonInit
    {
        public ProvinceInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<ProvinceDAO> provinceDAOs = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(provinceDAOs);
        }
        private List<ProvinceDAO> LoadFromExcel(string path)
        {
            List<ProvinceDAO> excelTemplates = new List<ProvinceDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[6];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    string provinceCode = worksheet.Cells[i, 1].Value?.ToString();
                    string provinceName = worksheet.Cells[i, 2].Value?.ToString();
                    ProvinceDAO excelTemplate = new ProvinceDAO()
                    {
                        Id = CreateGuid("Province" + provinceCode),
                        Code = provinceCode,
                        Name = provinceName
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}
