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
                this.loginSalesRep.Items.Add(srp.init);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.ActiveSalesRep = loginSalesRep.SelectedItem.ToString();
            this.Hide();
        }
    }
}
