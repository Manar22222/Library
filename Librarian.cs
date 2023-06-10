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
using Bunifu.UI.WinForms;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows.Input;

namespace test
{
    public partial class Librarian : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=MyLipraryDB;Integrated Security=TrueData Source=.;Initial Catalog=MyLipraryDB;Integrated Security=True;Connect Timeout=30");
        public Librarian()
        {
            InitializeComponent();
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
        }
        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Exit();

            }
        }

        public void populate()
        {
            con.Open();
            String query = "Select * from LibrarianTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            LibrarianDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (Libid.Text == "" || LibName.Text == "" || LibPassword.Text == "" || LibPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into librarianTbl values('" + Libid.Text + "','" + LibName.Text + "','" + LibPassword.Text + "','" + LibPhone.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Added Successfuly");
                con.Close();
                populate();
            }
        }

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();

        }

        private void Librarian_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void LibrarianDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            Libid.Text = LibrarianDGV.SelectedRows[0].Cells[0].Value.ToString();
            LibName.Text = LibrarianDGV.SelectedRows[0].Cells[1].Value.ToString();
            LibPhone.Text = LibrarianDGV.SelectedRows[0].Cells[2].Value.ToString();
            LibPassword.Text = LibrarianDGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (Libid.Text == "")
            {
                MessageBox.Show("Enter Student Id");
            }
            else
            {
                con.Open();
                String query = "delete from LibrarianTbl Where Libid =" + Libid.Text + ";";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Successfully deleted");
                con.Close();
                populate();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (Libid.Text == "" || LibName.Text == "" || LibPassword.Text == "" || LibPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                String query = "update  LibrarianTbl Set LibName ='" + LibName.Text + "',LibPassword='" + LibPassword.Text + "',LibPhone='" + LibPhone.Text + "'Where Libid='" + Libid.Text + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Successfully Updated");
                con.Close();
                populate();
            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
    }
}