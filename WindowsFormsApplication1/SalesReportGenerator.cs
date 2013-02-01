using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel; 

namespace ServiceOverblik
{
    public partial class SalesReportGenerator: Form
    {
        ServiceManager mainApp = new ServiceManager();
        String fileName;

        public SalesReportGenerator()
        {
            InitializeComponent();
            if (!checkLastReportDate())
            {
                getSalesNumbers();
                sendEmail(Properties.Settings.Default.salgsRapportEmail);
            }
        }

        private bool checkLastReportDate()
        {
            bool reportFound = false;
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = from c in sdb.salesreports
                                where c.lastGenereated.Month.Equals(DateTime.Now.Month) && c.lastGenereated.Year.Equals(DateTime.Now.Year)
                                select c;
                    if (query.Count() > 0)
                    {
                        reportFound = true;
                    }
                }
                catch (Exception ex)
                {
                    mainApp.eventlog.writeError(ex.Message, ex.StackTrace);
                }
                finally
                {
                    sdb.Dispose();
                }
            }
            return reportFound;
        }

        private List<servicecontracts> getSalesNumbers()
        {
            List<servicecontracts> stm = new List<servicecontracts>();
            int lastMonth = (DateTime.Now.AddMonths(-1)).Month;
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = from c in sdb.servicecontracts
                                where c.timestamp.Month.Equals(lastMonth) && c.timestamp.Year.Equals(DateTime.Now.Year) && !c.soldby.Equals("JJ")
                                //where c.timestamp.Month.Equals(DateTime.Now.Month) && c.timestamp.Year.Equals(DateTime.Now.Year) && !c.soldby.Equals("JJ")
                                select c;
                    stm = query.ToList();

                    salesreports d = new salesreports();
                    d.lastGenereated = DateTime.Now;
                    d.generatedBy = Form1.ActiveSalesRep.ToString();
                    d.machineName = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

                    sdb.salesreports.Add(d);
                    sdb.SaveChanges();
                    createExcelSheet(stm);
                }
                catch(Exception ex)
                {
                    mainApp.eventlog.writeError(ex.Message, ex.StackTrace);
                }
                finally
                {
                    sdb.Dispose();
                }
                return stm;
            }
        }

        private List<servicecontracts> getSalesNumbers(DateTime selectedMonth)
        {
            List<servicecontracts> stm = new List<servicecontracts>();
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = from c in sdb.servicecontracts
                                //where c.timestamp.Month.Equals(DateTime.Now.AddMonths(-1)) && c.timestamp.Year.Equals(DateTime.Now.Year)
                                where c.timestamp.Month.Equals(selectedMonth.Month) && c.timestamp.Year.Equals(selectedMonth.Year) && !c.soldby.Equals("JJ")
                                select c;
                    stm = query.ToList();

                    salesreports d = new salesreports();
                    d.lastGenereated = DateTime.Now;
                    d.generatedBy = Form1.ActiveSalesRep;
                    d.machineName = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

                    sdb.salesreports.Add(d);
                    sdb.SaveChanges();
                    createExcelSheet(stm);
                }
                catch (Exception ex)
                {
                    mainApp.eventlog.writeError(ex.Message, ex.StackTrace);
                }
                finally
                {
                    sdb.Dispose();
                }
                return stm;
            }
        }

        private void sendEmail(string salgsRapEmail)
        {
            EmailSender newMail = new EmailSender();
            newMail.sendSalesReport(salgsRapEmail, fileName);

        }

        private void sendEmail(string salgsRapEmail, DateTime selDate)
        {
            EmailSender newMail = new EmailSender();
            newMail.sendSalesReport(salgsRapEmail, fileName, selDate);

        }

        private void lukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void createExcelSheet(List<servicecontracts> stm)
        {
           
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //insert data to excel
            xlWorkSheet.Cells[1, 1] = "Service Aftale:";
            xlWorkSheet.Cells[1, 2] = "Salgs dato:";
            xlWorkSheet.Cells[1, 3] = "Solgt af:";
            xlWorkSheet.Cells[1, 4] = "Pris: (ekskl opstart & moms)";

            for (int i = 0; i < stm.Count(); i++)
            {
                xlWorkSheet.Cells[i + 2, 1] = stm[i].servicetypes.sname;
                xlWorkSheet.Cells[i + 2, 4] = stm[i].servicetypes.price;
                xlWorkSheet.Cells[i + 2, 2] = stm[i].timestamp;
                xlWorkSheet.Cells[i + 2, 3] = stm[i].soldby;
            }
            string savePath = Environment.GetEnvironmentVariable("ProgramFiles(x86)") + "\\" + "ServiceOverblik" + "\\" + Properties.Settings.Default.pdfSavePath;
            fileName = savePath + "salgsrapport_" + DateTime.Now.ToShortDateString() + ".xls";
            xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }

        }

        private void lukToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            getSalesNumbers(this.dateTimePicker1.Value);
            sendEmail(Properties.Settings.Default.salgsRapportEmail, this.dateTimePicker1.Value);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Rapport sendt!", "Mail sendt", MessageBoxButtons.OK);
            this.Close();
        }
    }
}
