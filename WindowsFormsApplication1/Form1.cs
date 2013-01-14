using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;


namespace ServiceOverblik
{
    public partial class Form1 : Form
    {
        private ServiceManager runstate;
        private DataTable CustomerView;
        private DataGridView dataGridView1;
        private GroupBox editGrpBx;
        private Label label39;
        private Label label38;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private Button button4;
        private Button button6;
        private Button button7;

        private static string activeSalesRep;
        private int selUserId;
        private string selectedService;
       
        public Form1()
        {
            InitializeComponent();
            createEditGroupBox();
            this.editGrpBx.Hide();
            createTMPFolder();

            runstate = new ServiceManager();
            Form2 loginForm = new Form2();

            //loginForm.ShowDialog();

            while (ActiveSalesRep == null)
            {
                //loginForm = new Form2();
                loginForm.ShowDialog();
            }

            loginForm.Dispose();
            createSalesRep.Text = ActiveSalesRep;

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
                editService.Items.Add(stp.sname);
                createService.Items.Add(stp.sname);
            }

            foreach (salesreps srp in runstate.getSalesReps())
            {
                searchSalesReps.Items.Add(srp.init);
            }

            foreach (paneltypes ptp in runstate.getPaneltypes())
            {
                createPaneltype.Items.Add(ptp.name);
            }

        }

        public static string ActiveSalesRep
        {
            get { return activeSalesRep; }
            set { activeSalesRep = value; }
        }

        private void createTMPFolder()
        {
            if (Directory.Exists(Properties.Settings.Default.pdfSavePath))
            {
                DirectoryInfo tmpDir = new DirectoryInfo(Properties.Settings.Default.pdfSavePath);
                foreach (FileInfo file in tmpDir.GetFiles())
                {
                    file.Delete();
                }

                //Directory.Delete(Properties.Settings.Default.pdfSavePath);
            }
            //Directory.CreateDirectory(Properties.Settings.Default.pdfSavePath);
        }


        private void createDataGridSearch()
        {
            dataGridView1 = new DataGridView();
            dataGridView1.Location = new Point(28, 263);
            dataGridView1.Size = new Size(1236, 387);
            dataGridView1.MouseClick += dataGridView1_MouseClick;
            this.Controls.Add(this.dataGridView1);
        }

        private void createEditGroupBox()
        {
            //Create items to populate groupbox
            label39 = new Label();
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(521, 16);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(42, 13);
            this.label39.TabIndex = 2;
            this.label39.Text = "Historik";

            label38 = new Label();
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(10, 161);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(33, 13);
            this.label38.TabIndex = 1;
            this.label38.Text = "Noter";

            richTextBox1 = new RichTextBox();
            this.richTextBox1.Location = new System.Drawing.Point(13, 177);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(391, 193);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.Click += richTextBox1_Click;

            richTextBox2 = new RichTextBox();
            this.richTextBox2.Location = new System.Drawing.Point(521, 33);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox2.Size = new System.Drawing.Size(347, 369);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";

            checkBox1 = new CheckBox();
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 35);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(122, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Aktiv service aftale?";
            this.checkBox1.UseVisualStyleBackColor = true;

            checkBox2 = new CheckBox();
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(13, 57);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(59, 17);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "Betalt?";
            this.checkBox2.UseVisualStyleBackColor = true;

            checkBox3 = new CheckBox();
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(13, 80);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(156, 17);
            this.checkBox3.TabIndex = 7;
            this.checkBox3.Text = "Igangværende servicesag?";
            this.checkBox3.UseVisualStyleBackColor = true;

            button4 = new Button();
            this.button4.Location = new System.Drawing.Point(13, 379);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Tilføj note";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += addCustomerHistory;

            button6 = new Button();
            this.button6.Location = new System.Drawing.Point(13, 115);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(122, 32);
            this.button6.TabIndex = 8;
            this.button6.Text = "Start Servicesag";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += button6_Click;

            button7 = new Button();
            this.button7.Location = new System.Drawing.Point(160, 115);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(126, 32);
            this.button7.TabIndex = 9;
            this.button7.Text = "Afslut Servicesag";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += button7_Click;


            //Create GroupBox and populate
            editGrpBx = new GroupBox(); 
            editGrpBx.Controls.Add(this.button7);
            editGrpBx.Controls.Add(this.button6);
            editGrpBx.Controls.Add(this.checkBox3);
            editGrpBx.Controls.Add(this.checkBox2);
            editGrpBx.Controls.Add(this.checkBox1);
            editGrpBx.Controls.Add(this.button4);
            editGrpBx.Controls.Add(this.richTextBox2);
            editGrpBx.Controls.Add(this.label39);
            editGrpBx.Controls.Add(this.label38);
            editGrpBx.Controls.Add(this.richTextBox1);
            editGrpBx.Location = new System.Drawing.Point(28, 230);
            editGrpBx.Name = "customerInfo";
            editGrpBx.Size = new System.Drawing.Size(1236, 420);
            editGrpBx.TabIndex = 23;
            editGrpBx.TabStop = false;
            editGrpBx.Text = "Kunde information";

            this.Controls.Add(this.editGrpBx);
        }
       
        private void button7_Click(object sender, EventArgs e)
        {
            runstate.endServiceCase(selUserId);
            this.checkBox3.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            runstate.startServiceCase(selUserId);
            this.checkBox3.Checked = true;
        }

        private void addCustomerHistory(object sender, EventArgs e)
        {
            string[] strArr = richTextBox1.Lines;
            richTextBox2.AppendText(richTextBox1.Text + "\n");
            runstate.addCustomerHistory(strArr, selUserId);
            richTextBox1.Clear();

        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            StringBuilder headerStr = new StringBuilder();
            headerStr.Append("--- Note ");
            headerStr.Append(DateTime.Now.ToShortDateString() + ",");
            headerStr.Append(" " + DateTime.Now.ToLongTimeString() + " af:");
            headerStr.Append(" " + activeSalesRep + " ---");
            headerStr.Append("\n");

            richTextBox1.AppendText(headerStr.ToString());
        }

        private void searchCustomer()
         {
             if (this.dataGridView1 == null || this.dataGridView1.IsDisposed )
             {
                 createDataGridSearch();
             }
             
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
            if (searchSalesReps.SelectedIndex >= 0)
            {
                searchresults.Add(runstate.searchCustomerBySalesRep(searchSalesReps.SelectedItem.ToString()));
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
            this.searchSalesReps.SelectedIndex = -1;
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
            fillEditHistory(selUserId);
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
                    editkWp.Text = query.kwp.ToString();
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
            object[] rObject = runstate.getEditCheckBoxes(selUserId);
            this.checkBox1.Checked = (bool)rObject[2];
            this.checkBox2.Checked = (bool)rObject[1];
            this.checkBox3.Checked = (bool)rObject[0];
        }

        private void editCustomer()
        {
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
                    editkWp.Text = query.kwp.ToString();
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
            object[] rObject = runstate.getEditCheckBoxes(selUserId);
            this.checkBox1.Checked = (bool)rObject[2];
            this.checkBox2.Checked = (bool)rObject[1];
            this.checkBox3.Checked = (bool)rObject[0];
        }

        private void fillEditHistory(int selUserId)
        {
            String[] history = runstate.loadCustomerHistory(selUserId).Split(new string[] { "%NL" }, StringSplitOptions.None);
            richTextBox2.Lines = history;
            richTextBox2.SelectionStart = richTextBox2.Text.Length;
            richTextBox2.ScrollToCaret();
        }

        private void editSave_Click(object sender, EventArgs e)
        {
            object[] updateData = new object[15];

            updateData[0] = editName.Text;
            updateData[4] = editEmail.Text;
            updateData[5] = editPhone.Text;
            updateData[6] = editPanels.Text;
            updateData[8] = editInverter.SelectedItem;
            updateData[9] = editService.FindString(editService.SelectedItem.ToString(), 0);
            updateData[10] = editServiceDate.Value;
            updateData[14] = editkWp.Text;

            if (runstate.updateCustomer(this.selUserId, updateData))
            {
                MessageBox.Show("Kunde info opdateret");
                editCustomer();
            }

            //searchCustomer();
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

        private void createCustomer_Click(object sender, EventArgs e)
        {
                object[] inData = new object[16];
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
                inData[13] = createSalesRep.Text;
                inData[14] = createPaneltype.SelectedItem;
                inData[15] = createkWp.Text;

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
            LabelCreatekWp.Text = "";
            //createSalesRep.Text = "";

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
            editkWp.ReadOnly = true;
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
            editkWp.ReadOnly = false;
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

                    runstate.createCustomer(inData);
                }
                else
                {
                    break;
                }
                
                this.Cursor = Cursors.Default;
            }
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (this.editGrpBx != null || this.editGrpBx.IsDisposed)
                    {
                        this.editGrpBx.Hide();
                    }
                    if (this.dataGridView1 == null || this.dataGridView1.IsDisposed)
                    {
                        createDataGridSearch();
                    }
                    this.textBox1.Clear();
                    this.textBox2.Clear();
                    this.textBox4.Clear();
                    this.textBox5.Clear();
                    break;

                case 1:
                    if (this.editGrpBx != null)
                    {
                        this.editGrpBx.Hide();
                    }
                    if (this.dataGridView1 != null)
                    {
                        this.dataGridView1.Dispose();
                    }
                    break;

                case 2:
                    if (this.dataGridView1 != null)
                    {
                        this.dataGridView1.Dispose();
                    }
                    if (this.editGrpBx == null || this.editGrpBx.IsDisposed)
                    {
                        createEditGroupBox();
                    }
                    else
                    {
                        this.editGrpBx.Show();
                    }
                    break;

                default: break;
            }
        }

        private void lukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void registrerBetalingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegPayment regPayForm = new RegPayment();
            regPayForm.ShowDialog();
        }

        private void contractView_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            object[] pdfData = new object[16];

            pdfData[0] = editName.Text;
            pdfData[1] = editStreet.Text;
            pdfData[2] = editCity.Text;
            pdfData[3] = editPostcode.Text;
            pdfData[4] = editColor.Text;
            pdfData[5] = editPanels.Text;
            pdfData[5] = editkWp.Text;
            pdfData[6] = editPhone.Text;
            pdfData[7] = editEmail.Text;
            pdfData[8] = editSalesRep.Text;
            pdfData[9] = editPaneltype.Text;
            pdfData[10] = editServiceDate.Value.ToLongDateString();
            pdfData[11] = editServiceEndDate.Value.ToLongDateString();
            pdfData[12] = editInverter.SelectedIndex;
            pdfData[13] = "";
            pdfData[14] = editService.SelectedItem;
            pdfData[15] = selectedService;

            int serviceNo = runstate.getServiceContractId(selUserId);
            object[] rObject = runstate.getServiceInfo(editService.SelectedIndex + 1);
            object[] rObject2 = runstate.getSalesReps(editSalesRep.Text);

            double servicePrice = runstate.calcServicePrice(editService.SelectedIndex + 1, rObject, Double.Parse(editkWp.Text));
            Cursor.Current = Cursors.Default;
            runstate.createPDF(pdfData, selUserId, servicePrice);

        }
   
        private void contractSend_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            object[] pdfData = new object[16];

            pdfData[0] = editName.Text;
            pdfData[1] = editStreet.Text;
            pdfData[2] = editCity.Text;
            pdfData[3] = editPostcode.Text;
            pdfData[4] = editColor.Text;
            pdfData[5] = editPanels.Text;
            pdfData[5] = editkWp.Text;
            pdfData[6] = editPhone.Text;
            pdfData[7] = editEmail.Text;
            pdfData[8] = editSalesRep.Text;
            pdfData[9] = editPaneltype.Text;
            pdfData[10] = editServiceDate.Value.ToLongDateString();
            pdfData[11] = editServiceEndDate.Value.ToLongDateString();
            pdfData[12] = editInverter.SelectedIndex;
            pdfData[13] = "";
            pdfData[14] = editService.SelectedItem;
            pdfData[15] = selectedService;
            
            int serviceNo = runstate.getServiceContractId(selUserId);
            object[] rObject = runstate.getServiceInfo(editService.SelectedIndex + 1);
            object[] rObject2 = runstate.getSalesReps(editSalesRep.Text);

            double servicePrice = runstate.calcServicePrice(editService.SelectedIndex + 1, rObject, Double.Parse(editkWp.Text));

            string fileName = runstate.sendPDF(pdfData, selUserId, servicePrice);
            Cursor.Current = Cursors.Default;
            EmailSender send = new EmailSender();
            send.sendToCustomer(editEmail.Text, serviceNo, editName.Text, editSalesRep.Text, (string)rObject2[0], (string)rObject2[1], fileName);

        }
    }
}