using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class University_Majors_SubjectGroupInit : CommonInit
    {
        public University_Majors_SubjectGroupInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<University_Majors_SubjectGroupDAO> university_Majors_SubjectGroupDAOs = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(university_Majors_SubjectGroupDAOs);
        }

        private List<University_Majors_SubjectGroupDAO> LoadFromExcel(string path)
        {
            List<University_Majors_SubjectGroupDAO> excelTemplates = new List<University_Majors_SubjectGroupDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[5];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    string universityCode = worksheet.Cells[i, 1].Value?.ToString();
                    string majorsCode = worksheet.Cells[i, 2].Value?.ToString();
                    string subjectGroupCode = worksheet.Cells[i, 3].Value?.ToString();
                    string year = worksheet.Cells[i, 4].Value?.ToString();
                    double benchmark = Convert.ToDouble(worksheet.Cells[i, 5].Value?.ToString().Replace(".", ","));
                    string note = worksheet.Cells[i, 6].Value?.ToString();
                    int quantity = Convert.ToInt32(worksheet.Cells[i, 7].Value?.ToString());

                    University_Majors_SubjectGroupDAO excelTemplate = new University_Majors_SubjectGroupDAO()
                    {
                        Id = CreateGuid(universityCode + majorsCode + year + subjectGroupCode),
                        University_MajorsId = CreateGuid(universityCode + majorsCode + year),
                        SubjectGroupId = CreateGuid("SubjectGroup" + subjectGroupCode),
                        Benchmark = benchmark,
                        Note = note,
                        Quantity = quantity
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}
