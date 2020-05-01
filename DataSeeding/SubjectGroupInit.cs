using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class SubjectGroupInit : CommonInit
    {
        public SubjectGroupInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<SubjectGroupDAO> subjectGroupDAOs = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(subjectGroupDAOs);
        }
        private List<SubjectGroupDAO> LoadFromExcel(string path)
        {
            List<SubjectGroupDAO> excelTemplates = new List<SubjectGroupDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[1];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    SubjectGroupDAO excelTemplate = new SubjectGroupDAO()
                    {
                        Id = CreateGuid("SubjectGroup" + worksheet.Cells[i, 1].Value?.ToString()),
                        Code = worksheet.Cells[i, 1].Value?.ToString(),
                        Name = worksheet.Cells[i, 2].Value?.ToString(),
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}
