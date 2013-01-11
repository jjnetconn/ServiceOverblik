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
    public partial class RegPayment : Form
    {
        private ServiceManager mainApp;
        private int userID;

        public RegPayment()
        {
            InitializeComponent();
            mainApp = new ServiceManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object[] rObject = mainApp.findServiceContractByNo(Int32.Parse(regPaymentContract.Text));
            this.userID = (int)rObject[0];

            if (rObject[1].ToString().Equals(""))
            {
                MessageBox.Show("Service aftale ikke fundet", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                regPaymentName.Text = rObject[1].ToString();
                regPaymentIsPaid.Checked = (bool)rObject[2];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool paymentRegOk = false;

            paymentRegOk = mainApp.setServiceContractPaid(userID);
            if (!paymentRegOk)
            {
                MessageBox.Show("Fejl i registrering af betaling!", "Fejl!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.Dispose();
            }

        }
    }
}
