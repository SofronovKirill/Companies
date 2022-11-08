using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;


namespace Companies
{
    public partial class Form1 : Form
    {
        private SQLiteConnection SQLiteConn;
        private DataTable dTable;

        public Form1()
        {
            InitializeComponent();
           
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SQLiteConn = new SQLiteConnection();
            dTable = new DataTable();
            
        }

        private bool OpenDBFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Все файлы(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                SQLiteConn = new SQLiteConnection("Data Source=" + openFileDialog.FileName + ";Version=3;");
                SQLiteConn.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = SQLiteConn;
                return true;
            }
            else return false;
        }

        private void GetTableNames()
        {
            string SQLQuery = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";
            SQLiteCommand command = new SQLiteCommand(SQLQuery, SQLiteConn);
            SQLiteDataReader reader = command.ExecuteReader();
            comboBox2.Items.Clear();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader[0].ToString());
            }
        }



        private void ShowTable(string SQLQuery)
        {
            dTable.Clear();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(SQLQuery, SQLiteConn);
            adapter.Fill(dTable);
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            for (int col = 0; col < dTable.Columns.Count; col++)
            {
                string ColName = dTable.Columns[col].ColumnName;
                dataGridView1.Columns.Add(ColName, ColName);
                //dataGridView1.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            for (int row = 0; row < dTable.Rows.Count; row++)
            {
                dataGridView1.Rows.Add(dTable.Rows[row].ItemArray);
            }
        }

        private void ShowImageGg()
        {
            string PQuery = "SELECT [Microsoft] FROM [Logo];";
            byte[] byteArray = new byte[0];
            SQLiteCommand command = new SQLiteCommand(PQuery, SQLiteConn);
            byteArray = (byte[])command.ExecuteScalar();

            MemoryStream ms = new MemoryStream(byteArray);
            this.pictureBox7.Image = Image.FromStream(ms);
            ms.Close();
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void ShowImageMc()
        {
            string PQuery = "SELECT [Google] FROM [Logo];";
            byte[] byteArray = new byte[0];
            SQLiteCommand command = new SQLiteCommand(PQuery, SQLiteConn);
            byteArray = (byte[])command.ExecuteScalar();

            MemoryStream ms = new MemoryStream(byteArray);
            this.pictureBox2.Image = Image.FromStream(ms);
            ms.Close();
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ShowImageAp()
        {
            string PQuery = "SELECT [Apple] FROM [Logo];";
            byte[] byteArray = new byte[0];
            SQLiteCommand command = new SQLiteCommand(PQuery, SQLiteConn);
            byteArray = (byte[])command.ExecuteScalar();

            MemoryStream ms = new MemoryStream(byteArray);
            this.pictureBox6.Image = Image.FromStream(ms);
            ms.Close();
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ShowImageTs()
        {
            string PQuery = "SELECT [Tesla] FROM [Logo];";
            byte[] byteArray = new byte[0];
            SQLiteCommand command = new SQLiteCommand(PQuery, SQLiteConn);
            byteArray = (byte[])command.ExecuteScalar();

            MemoryStream ms = new MemoryStream(byteArray);
            this.pictureBox10.Image = Image.FromStream(ms);
            ms.Close();
            pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ShowImageNv()
        {
            string PQuery = "SELECT [NVIDIA] FROM [Logo];";
            byte[] byteArray = new byte[0];
            SQLiteCommand command = new SQLiteCommand(PQuery, SQLiteConn);
            byteArray = (byte[])command.ExecuteScalar();

            MemoryStream ms = new MemoryStream(byteArray);
            this.pictureBox8.Image = Image.FromStream(ms);
            ms.Close();
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (OpenDBFile() == true)
            {
                GetTableNames();

                comboBox2.SelectedIndex = 1;
                dataGridView1.AllowUserToAddRows = false;
                ShowImageGg();
                ShowImageAp();
                ShowImageMc();
                ShowImageNv();
                ShowImageTs();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
           
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите таблицу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ShowTable(SQL_AllTable2());
           // dataGridView1.AllowUserToAddRows = false;
        }

        private void Rasszet()
         {
             if (comboBox2.SelectedIndex == -1)
             {
                 MessageBox.Show("Выберите таблицу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             ShowTable(SQL_AllTable2());
             dataGridView1.AllowUserToAddRows = false;
             dataGridView1.Rows.Add();
             double[] year = new double[dataGridView1.Columns.Count];
             int i, j;
             double[] variable = new double[dataGridView1.Rows.Count];
             
             dataGridView1.Rows[12].Cells[0].Value = "Год";
             for (i = 1; i < dataGridView1.Columns.Count; i++)
             {
                 for (j = 0; j < dataGridView1.Rows.Count - 1; j++)
                 {
                     variable[j]=Convert.ToDouble(dataGridView1.Rows[j].Cells[i].Value);
                     year[i] += variable[j];
                 }
                 dataGridView1.Rows[12].Cells[i].Value = year[i];
             }

             dataGridView1.Rows.Add();
             
             dataGridView1.Rows[13].Cells[0].Value = "Среднее";
             double[] sred = new double[dataGridView1.Columns.Count];
             for (i = 1; i < dataGridView1.Columns.Count; i++)
             {
                 for (j = 0; j < dataGridView1.Rows.Count; j++)
                 {
                     sred[i] = year[i] / 12;
                 }
                 dataGridView1.Rows[13].Cells[i].Value = Math.Round(sred[i], 2);
             }


         }

        private void button3_Click(object sender, EventArgs e)
        {
            tabPage8.Enabled = true;
            tabControl4.Enabled = true;
            Rasszet();
            chart3.Series.Clear();
            
            chart3.Series.Add(new Series("Microsoft"));
            chart3.Series["Microsoft"].Points.Clear();
            chart3.Series["Microsoft"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart3.Series["Microsoft"].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            chart3.Series["Microsoft"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart3.Series["Microsoft"].Enabled = true;
            chart3.Series["Microsoft"].BorderWidth = 1;

            int RowCount = dataGridView1.Rows.Count;
            double[] M = new double[12];
            double[] Epochs = new double[12];

             for (int i = 1; i < 12; i++)
             {
                Epochs[i] = i;
             }


            for (int i = 0; i < 12; i++)
            {
                M[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
            }


            for (int i = 0; i < 12; i++)
            {
                double y = M[i];
                double x = Epochs[i];
                chart3.Series["Microsoft"].Points.AddXY(x, y);
            }

            
            chart3.Series.Add(new Series("NVIDIA"));
            chart3.Series["NVIDIA"].Points.Clear();
            chart3.Series["NVIDIA"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart3.Series["NVIDIA"].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            chart3.Series["NVIDIA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart3.Series["NVIDIA"].Enabled = true;
            chart3.Series["NVIDIA"].BorderWidth = 1;

            
            double[] M_NV = new double[12];
            

            for (int i = 0; i < 12; i++)
            {
                M_NV[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }

            for (int i = 0; i < 12; i++)
            {
                double y = M_NV[i];
                double x = Epochs[i];
                int point = chart3.Series["NVIDIA"].Points.AddXY(x, y);
            }

            
            chart3.Series.Add(new Series("Apple"));
            chart3.Series["Apple"].Points.Clear();
            chart3.Series["Apple"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart3.Series["Apple"].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            chart3.Series["Apple"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart3.Series["Apple"].Enabled = true;
            chart3.Series["Apple"].BorderWidth = 1;


            double[] M_Ap = new double[12];
           

            for (int i = 0; i < 12; i++)
            {
                M_Ap[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }

            

            for (int i = 0; i < 12; i++)
            {
                double y = M_Ap[i];
                double x = Epochs[i];
               //chart3.ChartAreas[0].AxisY.Minimum = minAp - 10;
               // chart3.ChartAreas[0].AxisY.Maximum = maxAp + 10;
                chart3.Series["Apple"].Points.AddXY(x, y);
            }

            
            chart3.Series.Add(new Series("Tesla"));
            chart3.Series["Tesla"].Points.Clear();
            chart3.Series["Tesla"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart3.Series["Tesla"].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            chart3.Series["Tesla"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart3.Series["Tesla"].Enabled = true;
            chart3.Series["Tesla"].BorderWidth = 1;


            double[] M_Ts = new double[12];

            for (int i = 0; i < 12; i++)
            {
                M_Ts[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
            }

            double min = M_Ts[0];
            double max = min;
            for (int i = 0; i < M_Ts.Length; i++)
            {
                if (M_Ts[i] > max) max = M_Ts[i];
                if (M_Ts[i] < min) min = M_Ts[i];
            }

            for (int i = 0; i < 12; i++)
            {
                double y = M_Ts[i];
                double x = Epochs[i];
                //chart3.ChartAreas[0].AxisY.Minimum = min - 10;
                chart3.ChartAreas[0].AxisY.Maximum = max + 10;
                chart3.Series["Tesla"].Points.AddXY(x, y);
            }

         
            chart3.Series.Add(new Series("Google"));
            chart3.Series["Google"].Points.Clear();
            chart3.Series["Google"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart3.Series["Google"].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            chart3.Series["Google"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart3.Series["Google"].Enabled = true;
            chart3.Series["Google"].BorderWidth = 1;


            double[] M_Gg = new double[12];

            for (int i = 0; i < 12; i++)
            {
                M_Gg[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
            }

            double minGg = M_Ap[0];
            double maxGg = minGg;
            for (int i = 0; i < M_Ap.Length; i++)
            {
                if (M_Gg[i] > maxGg) maxGg = M_Gg[i];
                if (M_Gg[i] < minGg) minGg = M_Gg[i];
            }

            for (int i = 0; i < 12; i++)
            {
                double y = M_Gg[i];
                double x = Epochs[i];
                chart3.ChartAreas[0].AxisY.Minimum = minGg - 10;
                chart3.Series["Google"].Points.AddXY(x, y);
            }

            
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            chart3.Series["Microsoft"].Enabled = checkBox1.Checked;
        }




        private string SQL_AllTable2()
        {
            return "SELECT * FROM [" + comboBox2.SelectedItem + "] order by 2";
        }

     

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            chart3.Series["NVIDIA"].Enabled = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            chart3.Series["Apple"].Enabled = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            chart3.Series["Tesla"].Enabled = checkBox4.Checked;
        }


        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            chart3.Series["Google"].Enabled = checkBox5.Checked;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
