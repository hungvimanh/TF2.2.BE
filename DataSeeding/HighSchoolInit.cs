using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;
using System.Linq;

namespace DataSeeding
{
    public class HighSchoolInit : CommonInit
    {
        public HighSchoolInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<HighSchoolDAO> highSchoolDAOs = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(highSchoolDAOs);
        }

        private List<HighSchoolDAO> LoadFromExcel(string path)
        {
            List<HighSchoolDAO> excelTemplates = new List<HighSchoolDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[9];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    string provinceCode = worksheet.Cells[i, 1].Value?.ToString();

                    if (string.IsNullOrEmpty(provinceCode))
                    {
                        continue;
                    }
                    string highSchoolCode = worksheet.Cells[i, 2].Value?.ToString();
                    string highSchoolName = worksheet.Cells[i, 3].Value?.ToString();
                    string address = worksheet.Cells[i, 4].Value?.ToString();

                    if (provinceCode.Length < 2)
                    {
                        provinceCode = "0" + provinceCode;
                    }
                    if (highSchoolCode.Length < 3)
                    {
                        if (highSchoolCode.Length < 2)
                            highSchoolCode = "00" + highSchoolCode;
                        else highSchoolCode = "0" + highSchoolCode;
                    }

                    HighSchoolDAO excelTemplate = new HighSchoolDAO()
                    {
                        Id = CreateGuid("HighSchool" + provinceCode + highSchoolCode + highSchoolName),
                        ProvinceId = CreateGuid("Province" + provinceCode),
                        Code = highSchoolCode,
                        Name = highSchoolName,
                        Address = address
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}
