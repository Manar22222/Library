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
using System.Collections;

namespace test
{
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=.;Initial Catalog=MyLipraryDB;Integrated Security=TrueData Source=.;Initial Catalog=MyLipraryDB;Integrated Security=True;Connect Timeout=30");
        public void populate()
        {
            Con.Open();
            String query = "Select * from IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            IssueBookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        public void populatereturn()
        {
            Con.Open();
            String query = "Select * from ReturnTb";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnedBookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void IssueBookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuTextBox1.Text = IssueBookDGV.SelectedRows[0].Cells[0].Value.ToString();
            StdCb.Text = IssueBookDGV.SelectedRows[0].Cells[1].Value.ToString();
            stdnameTb.Text = IssueBookDGV.SelectedRows[0].Cells[2].Value.ToString();
            stddpmntTb.Text = IssueBookDGV.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTb.Text = IssueBookDGV.SelectedRows[0].Cells[4].Value.ToString();
            Bookcb.Text = IssueBookDGV.SelectedRows[0].Cells[5].Value.ToString();
            IssueDatee.Text = IssueBookDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void FillBook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qey>" + 0 + "", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", typeof(string));
            dt.Load(rdr);
            /*Bookcb.ValueMember = "BookName";
            Bookcb.DataSource = dt;*/
            Con.Close();
        }
        private void UpdateBook()
        {
            int Qey, newQey;

            Con.Open();
            string query = " select * from BookTbl where BookName = '" + Bookcb.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qey = Convert.ToInt32(dr["Qey"].ToString());
                newQey = Qey + 1;
                string query1 = " update BookTbl set Qey=" + newQey + " where BookName='" + Bookcb.Text + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();

            }
            Con.Close();
        }
        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (ReturnNumTb.Text == "" || stdnameTb1.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
               // string issuedate = IssueDatee.Value.Day.ToString() + "/" + IssueDatee.Value.Month.ToString() + "/" + IssueDatee.Value.Year.ToString();
              //  string returndate = ReturnDatee.Value.Day.ToString() + "/" + IssueDatee.Value.Month.ToString() + "/" + IssueDatee.Value.Year.ToString();
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ReturnTb values('" +Convert.ToInt32(bunifuTextBox1.Text) + "','" +Convert.ToInt32(StdCb.Text) + "','" + stdnameTb.Text +"','" + stddpmntTb.Text + "','" + PhoneTb.Text + "','" + Bookcb.Text + "','" + IssueDatee.Value + "','" + ReturnDatee.Value + "')",Con );
                ////SqlCommand cmd = new SqlCommand("insert into ReturnTbl values('" + ReturnNumTb.Text + "','" + StdCb.SelectedItem.ToString() + "','" + stdnameTb.Text + "','" + stddpmntTb.Text + " ','" + PhoneTb.Text + " ','" + Bookcb.SelectedValue.ToString() + "',' " + issuedate + "','"+returndate+"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfuly Returned");
                SqlCommand cmd1 = new SqlCommand("delete from IssueTbl where IssueNum =('" + Convert.ToInt32(bunifuTextBox1.Text) + "')",Con);
                cmd1.ExecuteNonQuery();
                Con.Close();
                bunifuTextBox1.Text = stdnameTb.Text = PhoneTb.Text = StdCb.Text = stddpmntTb.Text = Bookcb.Text = ""; IssueDatee.Value = DateTime.Now;
                UpdateBook();
                populate();
                populatereturn();
              
            }
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
        private void ReturnBook_Load(object sender, EventArgs e)
        {
            populate();
            populatereturn();
            FillBook();
            ReturnDatee.Value = DateTime.Now;
            bunifuTextBox1.Enabled=stdnameTb.Enabled=PhoneTb.Enabled=StdCb.Enabled=stddpmntTb.Enabled=Bookcb.Enabled=IssueDatee.Enabled = false;

        }

        
       
        Bitmap bitmap;
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            this.Controls.Add(panel);
            Graphics graphics = panel.CreateGraphics();
            Size size = this.ClientSize;
            bitmap = new Bitmap(size.Width, size.Height, graphics);
            graphics = Graphics.FromImage(bitmap);
            Point point = PointToScreen(panel.Location);
            graphics.CopyFromScreen(point.X, point.Y,0,0,size);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

         }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap,0,0);
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
