using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace ServiceOverblik
{
    class ExcelLoad
    {
        public DataSet GetExcel(string fileName)
        {
            Application oXL;
            Workbook oWB;
            Worksheet oSheet;
            Range oRng;

                //  creat a Application object
                oXL = new Application();
                //   get   WorkBook  object
                oWB = oXL.Workbooks.Open(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value);


                //   get   WorkSheet object 
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Sheets[1];
                System.Data.DataTable dt = new System.Data.DataTable("dtExcel");
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                DataRow dr;


                StringBuilder sb = new StringBuilder();
                int jValue = oSheet.UsedRange.Cells.Columns.Count;
                int iValue = oSheet.UsedRange.Cells.Rows.Count;
                //  get data columns
                for (int j = 1; j <= jValue; j++)
                {
                    dt.Columns.Add("column" + j, System.Type.GetType("System.String"));
                }


                //string colString = sb.ToString().Trim();
                //string[] colArray = colString.Split(':');


                //  get data in cell
                for (int i = 1; i <= iValue; i++)
                {
                    dr = ds.Tables["dtExcel"].NewRow();
                    for (int j = 1; j <= jValue; j++)
                    {
                        oRng = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[i, j];
                        string strValue = oRng.Text.ToString();
                        dr["column" + j] = strValue;
                    }
                    ds.Tables["dtExcel"].Rows.Add(dr);
                }
                return ds;
          
          
        }
    }
}
