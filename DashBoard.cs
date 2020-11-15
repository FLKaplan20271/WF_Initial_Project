using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WF_Initial_Project
{

    public partial class DashBoard : Form
    {
        SqlDataAdapter sda;
        SqlCommandBuilder sbc;
        DataTable dt;

        public DashBoard()
        {
            InitializeComponent();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'personData.tblPersonData' table. You can move, or remove it, as needed.
            //this.dtaTblPerson.Fill(this.dsPersonData.tblPersonData);
            btnUpdateRecords.Enabled = false;

        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=franksdb;Integrated security=True;");
            //sda = new SqlDataAdapter(@"select id,fname,lname,email,gender,address, city,state,postal,phonenumber,yearlysalary,unitname,ip_address from tblPersonData",con); //works but is too long
            //sda = new SqlDataAdapter(@"EXEC sp_GetPersonData", con); //does not work with a multi-joined table
            sda = new SqlDataAdapter(@"SELECT * FROM vwPerson", con);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            btnUpdateRecords.Enabled = true;
        }

        private void btnUpdateRecords_Click(object sender, EventArgs e)
        {

            try
            {
                sbc = new SqlCommandBuilder(sda);
                sda.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
