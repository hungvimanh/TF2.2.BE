using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class EthnicInit : CommonInit
    {
        public EthnicInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<EthnicDAO> ethnics = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(ethnics);
        }
        private List<EthnicDAO> LoadFromExcel(string path)
        {
            List<EthnicDAO> excelTemplates = new List<EthnicDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    EthnicDAO excelTemplate = new EthnicDAO()
                    {
                        Id = CreateGuid("Ethnic" + worksheet.Cells[i, 1].Value?.ToString()),
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
