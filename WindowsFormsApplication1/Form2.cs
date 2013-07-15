using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceOverblik
{
    public partial class Form2 : Form
    {
        private string _selSalesRep = null;
        public string SelSalesRep
        {
            get { return _selSalesRep; }
            set { _selSalesRep = value; }
        }

        public Form2()
        {
            InitializeComponent();
            fillLoginBox();
        }
        private void fillLoginBox()
        {
            ServiceManager mainApp = new ServiceManager();
            foreach (salesreps srp in mainApp.getSalesReps())
            {
                loginSalesRep.Items.Add(srp.init);
                loginSalesRep.SelectedIndex = 0;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelSalesRep = loginSalesRep.SelectedItem.ToString();
            //Form1.ActiveSalesRep = loginSalesRep.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
