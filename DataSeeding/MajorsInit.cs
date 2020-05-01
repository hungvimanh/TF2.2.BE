using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;
using System.Linq;

namespace DataSeeding
{
    public class MajorsInit : CommonInit
    {
        public MajorsInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<MajorsDAO> majorsDAOs = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(majorsDAOs);
        }
        private List<MajorsDAO> LoadFromExcel(string path)
        {
            List<MajorsDAO> excelTemplates = new List<MajorsDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[3];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i, 1].Value?.ToString()))
                    {
                        continue;
                    }
                    MajorsDAO excelTemplate = new MajorsDAO()
                    {
                        Id = CreateGuid("Majors" + worksheet.Cells[i, 1].Value?.ToString()),
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
