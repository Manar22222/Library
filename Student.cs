using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{

    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=MyLipraryDB;Integrated Security=TrueData Source=.;Initial Catalog=MyLipraryDB;Integrated Security=True;Connect Timeout=30");

        public void Poplate()
        {
            con.Open();
            String query = "Select * from StudentTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            bunifuDataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Exit();

            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();
        }

        private void Student_Load(object sender, EventArgs e)
        {
            Poplate();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (Stdid.Text == "" || StdName.Text == "" || StdPhone.Text == "" || StdSem.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into StudentTbl values(" + Stdid.Text + ",'" + StdName.Text + "','" + StdDep.Text + "','" + StdSem.SelectedItem.ToString() + "','" + StdPhone.Text + "')", con);/*("insert into StudentTbl value("  +Stdid.Text+ ",'"  +StdName.Text+ "','" +StdDep.Text+ "'," +StdSem.SelectedItem.ToString()+ ",'"  +StdPhone.Text+ "')", con);*/
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Successfully");
                con.Close();
                Poplate();
            }
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            Stdid.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            StdName.Text = bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            StdDep.Text = bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            StdSem.SelectedItem = bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            StdPhone.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (Stdid.Text == "")
            {
                MessageBox.Show("Enter Student Id");
            }
            else
            {
                con.Open();
                String query = "delete from StudentTbl Where Stdid =" + Stdid.Text + ";";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully deleted");
                con.Close();
                Poplate();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (Stdid.Text == "" || StdName.Text == "" || StdPhone.Text == "" || StdSem.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                String query = "update  StudentTbl Set StdName ='" + StdName.Text + "',StdDep='" + StdDep.Text + "',StdSem='" + StdSem.SelectedItem.ToString() + "',StdPhone='" + StdPhone.Text + "'Where Stdid='" + Stdid.Text + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully Updated");
                con.Close();
                Poplate();
            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
    }
}
