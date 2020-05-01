using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class University_MajorsInit : CommonInit
    {
        public University_MajorsInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<University_MajorsDAO> university_MajorsDAOs = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(university_MajorsDAOs);
        }

        private List<University_MajorsDAO> LoadFromExcel(string path)
        {
            List<University_MajorsDAO> excelTemplates = new List<University_MajorsDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[4];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    string universityCode = worksheet.Cells[i, 1].Value?.ToString();
                    string majorsCode = worksheet.Cells[i, 2].Value?.ToString();
                    string year = worksheet.Cells[i, 3].Value?.ToString();

                    University_MajorsDAO excelTemplate = new University_MajorsDAO()
                    {
                        Id = CreateGuid(universityCode + majorsCode + year),
                        UniversityId = CreateGuid("University" + universityCode),
                        MajorsId = CreateGuid("Majors" + majorsCode),
                        Year = year
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}
