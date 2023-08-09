<<<<<<< HEAD
﻿using System.Data;
using System.IO;
using ExcelDataReader;
=======
﻿using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

namespace SSCMS.Utils
{
    public static class ExcelUtils
    {
<<<<<<< HEAD
        public static DataTable GetDataTable(string filePath)
=======
        public static DataTable Read(string filePath)
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            DataTable table;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
<<<<<<< HEAD
                
=======

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    table = result.Tables[0];
                    // The result of each spreadsheet is in result.Tables
                }
            }

            return table;
        }
<<<<<<< HEAD
=======

        public static void Write(string filePath, List<string> head, List<List<string>> rows)
        {
            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Sheet1");

                List<string> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);
                int columnIndex = 0;

                foreach (string column in head)
                {
                    columns.Add(column);
                    row.CreateCell(columnIndex).SetCellValue(column);
                    columnIndex++;
                }

                int rowIndex = 1;
                foreach (List<string> dsrow in rows)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    int cellIndex = 0;
                    for (int i = 0; i < head.Count; i++)
                    {
                        var val = dsrow[i];
                        row.CreateCell(cellIndex).SetCellValue(val);
                        cellIndex++;
                    }

                    rowIndex++;
                }
                workbook.Write(fs, false);
            }
        }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }
}