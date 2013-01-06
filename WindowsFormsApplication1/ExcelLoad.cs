using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Marshal = System.Runtime.InteropServices.Marshal;

using System.Reflection;

namespace ServiceOverblik
{
    class ExcelLoad
    {
        public DataSet GetExcel(string fileName)
        {
           
                Excel.Application oXL = null;
                Excel.Workbook oWB = null;
                Excel.Worksheet oSheet = null;
                Excel.Range oRng = null;
            try
            {
                //  creat a Application object
                oXL = new Excel.Application();
                //   get   WorkBook  object
                oWB = oXL.Workbooks.Open(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value);


                //   get   WorkSheet object 
                oSheet = (Excel.Worksheet)oWB.Sheets[1];
                DataTable dt = new DataTable("dtExcel");
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
            finally
            {
                // Release all COM RCWs.
                // The "releaseObject" will just "do nothing" if null is passed,
                // so no need to check to find out which need to be released.
                // The "finally" is run in all cases, even if there was an exception
                // in the "try". 
                // Note: passing "by ref" so afterwords "xlWorkSheet" will
                // evaluate to null. See "releaseObject".
                releaseObject(ref oSheet);
                releaseObject(ref oWB);
                // The Quit is done in the finally because we always
                // want to quit. It is no different than releasing RCWs.
                if (oXL != null)
                {
                    oXL.Quit();
                }
                releaseObject(ref oXL);
            }
          
        }

        private void releaseObject(ref Excel.Application obj)
        {
            // Do not catch an exception from this.
            // You may want to remove these guards depending on
            // what you think the semantics should be.
            if (obj != null && Marshal.IsComObject(obj))
            {
                Marshal.ReleaseComObject(obj);
            }
            // Since passed "by ref" this assingment will be useful
            // (It was not useful in the original, and neither was the
            //  GC.Collect.)
            obj = null;
        }

        private void releaseObject(ref Excel.Workbook obj)
        {
            // Do not catch an exception from this.
            // You may want to remove these guards depending on
            // what you think the semantics should be.
            if (obj != null && Marshal.IsComObject(obj))
            {
                Marshal.ReleaseComObject(obj);
            }
            // Since passed "by ref" this assingment will be useful
            // (It was not useful in the original, and neither was the
            //  GC.Collect.)
            obj = null;
        }

        private void releaseObject(ref Excel.Worksheet obj)
        {
            // Do not catch an exception from this.
            // You may want to remove these guards depending on
            // what you think the semantics should be.
            if (obj != null && Marshal.IsComObject(obj))
            {
                Marshal.ReleaseComObject(obj);
            }
            // Since passed "by ref" this assingment will be useful
            // (It was not useful in the original, and neither was the
            //  GC.Collect.)
            obj = null;
        }
    }
}
