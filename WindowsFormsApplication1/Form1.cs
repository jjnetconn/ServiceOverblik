﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace ServiceOverblik
{
    public partial class Form1 : Form
    {
        private ServiceManager runstate;
        private DataTable CustomerView;
        //private List<prodcat> Inverters;
        private int selUserId;
        private string selectedService;
       
        public Form1()
        {
            InitializeComponent();

            runstate = new ServiceManager();
            CustomerView = new DataTable();

            CustomerView.Columns.Add("UID", typeof(int));
            CustomerView.Columns.Add("Post nr.:", typeof(int));
            CustomerView.Columns.Add("By", typeof(string));
            CustomerView.Columns.Add("Gade", typeof(string));
            CustomerView.Columns.Add("Navn", typeof(string));
            CustomerView.Columns.Add("E-mail", typeof(string));
            CustomerView.Columns.Add("Telefon nr.:", typeof(string));
            CustomerView.Columns.Add("Antal Paneler", typeof(string));
            CustomerView.Columns.Add("Farve", typeof(string));
            CustomerView.Columns.Add("Inverter", typeof(string));
            //CustomerView.Columns.Add("Varehus id", typeof(string));
            CustomerView.Columns.Add("Servicekontrakt nr.:", typeof(int));
            //CustomerView.Columns.Add("Service start dato:", typeof(DateTime));
            CustomerView.Columns.Add("Serviceaftale?", typeof(bool));
            //CustomerView.Columns.Add("Service type", typeof(string));
            //CustomerView.Columns.Add("Privat kunde?", typeof(bool));


            foreach (prodcat inv in runstate.listInverters())
            {
                editInverter.Items.Add(inv.cname_prod);
                createInverter.Items.Add(inv.cname_prod);
            }

            foreach (servicetypes stp in runstate.getServicetypes())
            {
                comboBox1.Items.Add(stp.sname);
                editService.Items.Add(stp.sname);
                createService.Items.Add(stp.sname);
            }
            foreach (salesreps srp in runstate.getSalesReps())
            {
                createSalesRep.Items.Add(srp.init);
            }
            foreach (paneltypes ptp in runstate.getPaneltypes())
            {
                createPaneltype.Items.Add(ptp.name);
            }

        }

        private void searchCustomer()
        {

            CustomerView.Clear();

            //Get filter input
            List<List<customers>> searchresults = new List<List<customers>>();

            if (textBox1.TextLength != 0)
            {
                searchresults.Add(runstate.searchCustomerByName(textBox1.Text));
            }

            if (textBox2.TextLength != 0)
            {
                searchresults.Add(runstate.searchCustomerByStreet(textBox2.Text));
            }

            if (textBox5.TextLength != 0)
            {
                searchresults.Add(runstate.searchCustomerByCity(textBox5.Text));
            }

            if (textBox4.TextLength != 0)
            {
                searchresults.Add(runstate.searchCustomerByPostcode(textBox4.Text));
            }

            var lengths = from element in searchresults
                          orderby element.Count
                          select element;

            foreach (List<customers> lst in lengths)
            {
                foreach (customers current in lst)
                {
                    CustomerView.Rows.Add(current.uId,
                                          current.postcode,
                                          current.city,
                                          current.street,
                                          current.cname,
                                          current.email,
                                          current.phone,
                                          current.panelcount,
                                          current.color,
                                          current.inverter,
                                          //current.warehouseid,
                                          current.serviceno,
                                          //current.servicecontracts.startdate,
                                          current.hasservice
                                          //current.servicecontracts.servicetypes.sname
                                          //current.isprivate
                                          );
                }
            }

            dataGridView1.DataSource = CustomerView;
            dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns[10].Visible = false;
            //dataGridView1.Columns[12].Visible = false;
            //dataGridView1.Columns[13].Visible = false;
            //dataGridView1.Columns[14].Visible = false;
            //dataGridView1.Columns[15].Visible = false;
            dataGridView1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchCustomer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox4.Clear();
            this.textBox5.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CustomerView.Clear();
            dataGridView1.Invalidate();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected == true)
                        row.Selected = false;
                }

                // Get the selected Row 
                DataGridView.HitTestInfo info = dataGridView1.HitTest(e.X, e.Y);

                // Set as selected 
                dataGridView1.Rows[info.RowIndex].Selected = true;

                // Call editCustomer
                this.selUserId = info.RowIndex;
                editCustomer(dataGridView1.Rows[info.RowIndex]);

            }
            catch
            {

            }
        }

        private void editCustomer(DataGridViewRow selectedRow)
        {
            selUserId = Int32.Parse(selectedRow.Cells[0].Value.ToString());
            tabControl1.SelectedTab = tabPage3;
            setFieldsRO();
            int servicetypeID = new int();

            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == selUserId
                                 select c).FirstOrDefault();

                    editName.Text = query.cname;
                    editStreet.Text = query.street;
                    editCity.Text = query.city;
                    editPostcode.Text = query.postcode.ToString();
                    editColor.Text = query.color;
                    editPanels.Text = query.panelcount;
                    editPhone.Text = query.phone;
                    editEmail.Text = query.email;
                    editSalesRep.Text = query.servicecontracts.soldby;
                    editPaneltype.Text = query.paneltype;
                    editServiceDate.Value = query.servicecontracts.startdate;
                    editServiceEndDate.Value = query.servicecontracts.enddate;
                    editInverter.SelectedIndex = runstate.customerInverter(runstate.listInverters(), query.inverter);
                    servicetypeID = (int)query.servicecontracts.servicetype;
                    editService.SelectedIndex = (int)query.servicecontracts.servicetype - 1;
                    selectedService = editService.SelectedItem.ToString();

                }
                catch
                {

                }
                finally
                {
                    sdb.Dispose();
                }
            }
        }

        private void editSave_Click(object sender, EventArgs e)
        {
            object[] updateData = new object[14];
            bool serviceChanged = false;

            updateData[0] = editName.Text;
            updateData[4] = editEmail.Text;
            updateData[5] = editPhone.Text;
            updateData[6] = editPanels.Text;
            updateData[8] = editInverter.SelectedItem;
            updateData[9] = editService.FindString(editService.SelectedItem.ToString(), 0);
            updateData[10] = editServiceDate.Value;

            /*
             * inData[0] = createName.Text;
                inData[1] = createStreet.Text;
                inData[2] = createCity.Text;
                inData[3] = Convert.ToInt32(createPostcode.Text);
                inData[4] = createEmail.Text;
                inData[5] = createPhone.Text;
                inData[6] = Convert.ToInt32(createPanelcount.Text);
                inData[7] = createColor.Text;
                inData[8] = (string)createInverter.SelectedItem;
                inData[9] = createService.SelectedIndex + 1;
                inData[10] = createServiceDate.Value.Date;
                inData[11] = "";
                inData[12] = "";
                //Implementeres i kommende version
                inData[11] = createInverterSerial;
                inData[12] = createInverterSw;
                inData[13] = createSalesRep.SelectedItem;
                inData[14] = createPaneltype.SelectedItem;
                */
            if( selectedService.Equals(this.editService.SelectedItem.ToString()))
            {
                serviceChanged = true;
            }

            if (runstate.updateCustomer(this.selUserId, updateData, serviceChanged))
            {
                MessageBox.Show("Kunde info opdateret");
            }

            searchCustomer();
            doEdit.Checked = false;
        }

        //Hidden to be implemented later
        private void cbInverter_SelectedValueChanged(object sender, EventArgs e)
        {
            label18.Visible = true;
            label19.Visible = true;
            editSerial.Visible = true;
            editVersion.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.createSalesRep.Text.Length < 2)
            {
                MessageBox.Show("Sælger initialer mangler!", "Manger data", MessageBoxButtons.OK);
            }
            else
            {
                object[] inData = new object[15];
                inData[0] = createName.Text;
                inData[1] = createStreet.Text;
                inData[2] = createCity.Text;
                inData[3] = Convert.ToInt32(createPostcode.Text);
                inData[4] = createEmail.Text;
                inData[5] = createPhone.Text;
                inData[6] = Convert.ToInt32(createPanelcount.Text);
                inData[7] = createColor.Text;
                inData[8] = (string)createInverter.SelectedItem;
                inData[9] = createService.SelectedIndex + 1;
                inData[10] = createServiceDate.Value.Date;
                inData[11] = "";
                inData[12] = "";
                /* Implementeres i kommende version
                 * inData[11] = createInverterSerial;
                 * inData[12] = createInverterSw;
                 * 
                 */
                inData[13] = createSalesRep.SelectedItem;
                inData[14] = createPaneltype.SelectedItem;


                bool blankFields = false;


                foreach (object obj in inData)
                {
                    if (obj.Equals(null))
                    {
                        blankFields = true;
                    }
                }

                if (blankFields)
                {
                    MessageBox.Show("Udfyld alle felter", "Data mangler", MessageBoxButtons.OK);
                }
                else
                {
                    bool updateResult = runstate.createCustomer(inData);

                    if (updateResult)
                    {
                        resetCreateFields();
                    }
                }
            }
        }

        private void resetCreateFields()
        {
            createName.Text = "";
            createStreet.Text = "";
            createCity.Text = "";
            createPostcode.Text = "";
            createEmail.Text = "";
            createPhone.Text = "";
            createPanelcount.Text = "";
            createColor.Text = "";
            createInverter.Text = "";
            createService.Text = "";
            createPaneltype.Text = "";
            createSalesRep.Text = "";

        }

        private void doEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (doEdit.Checked)
            {
                setFieldsRW();
            }
            else
            {
                setFieldsRO();
            }
        }

        private void setFieldsRO()
        {
            editName.ReadOnly = true;
            
            /*
             * editStreet.ReadOnly = true;
             * editCity.ReadOnly = true;
             * editPostcode.ReadOnly = true;
            */

            editColor.ReadOnly = true;
            editPanels.ReadOnly = true;
            editPhone.ReadOnly = true;
            editEmail.ReadOnly = true;
            editSalesRep.ReadOnly = true;
            editSave.Visible = false;
        }

        private void setFieldsRW()
        {
            editName.ReadOnly = false;

            /*
             * editStreet.ReadOnly = true;
             * editCity.ReadOnly = true;
             * editPostcode.ReadOnly = true;
            */

            editColor.ReadOnly = false;
            editPanels.ReadOnly = false;
            editPhone.ReadOnly = false;
            editEmail.ReadOnly = false;
            editSave.Visible = true;

        }

        private void excelImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet excelIn = new DataSet();
            OpenFileDialog openExcel = new OpenFileDialog();
            openExcel.ShowDialog();
            this.Cursor = Cursors.WaitCursor;
            excelIn = runstate.GetExcel(openExcel.FileName);

            int startRow = Properties.Settings.Default.excelStartRow;

            for (int i = startRow; i < excelIn.Tables[0].Rows.Count; i++)
            {
                if (!excelIn.Tables[0].Rows[i].ItemArray[0].Equals(null))
                {
                    object[] inData = new object[14];
                    inData[0] = excelIn.Tables[0].Rows[i].ItemArray[3];
                    inData[1] = excelIn.Tables[0].Rows[i].ItemArray[2];
                    inData[2] = excelIn.Tables[0].Rows[i].ItemArray[1];
                    inData[3] = Convert.ToInt32(excelIn.Tables[0].Rows[i].ItemArray[0]);
                    inData[4] = excelIn.Tables[0].Rows[i].ItemArray[4];
                    inData[5] = excelIn.Tables[0].Rows[i].ItemArray[5];
                    inData[6] = Convert.ToInt32(excelIn.Tables[0].Rows[i].ItemArray[6]);
                    inData[7] = excelIn.Tables[0].Rows[i].ItemArray[7];
                    inData[8] = excelIn.Tables[0].Rows[i].ItemArray[8];
                    inData[9] = null;
                    inData[10] = null;
                    inData[11] = "";
                    inData[12] = "";
                    /* Implementeres i kommende version
                     * inData[11] = createInverterSerial;
                     * inData[12] = createInverterSw;
                     */
                    inData[13] = "autoimport";

                    /*
                     * inData[0] = createName.Text;
                    inData[1] = createStreet.Text;
                    inData[2] = createCity.Text;
                    inData[3] = Convert.ToInt32(createPostcode.Text);
                    inData[4] = createEmail.Text;
                    inData[5] = createPhone.Text;
                    inData[6] = Convert.ToInt32(createPanelcount.Text);
                    inData[7] = createColor.Text;
                    inData[8] = (string)createInverter.SelectedItem;
                    inData[9] = createService.SelectedIndex;
                    inData[10] = createServiceDate.Value.Date;
                    inData[11] = "";
                    inData[12] = "";
                    /* Implementeres i kommende version
                     * inData[11] = createInverterSerial;
                     * inData[12] = createInverterSw;
                    inData[13] = createSalesRep.Text;
                     */
                    runstate.createCustomer(inData);
                }
                else
                {
                    break;
                }
                
                this.Cursor = Cursors.Default;
            }
            
        }
    }
}