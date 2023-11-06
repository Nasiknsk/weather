using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace weather_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string city=txtCity.Text.Trim();
            if(txtCity.Text=="")
            {
                DialogResult dt=MessageBox.Show("Please Enter a City !","Notice",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                Web_API wb = new Web_API();
                string[] results = wb.GetCity(city);
                
                if (results.Length >= 3)
                {
                    txtLocation.Text = results[0];
                    txtTemp.Text = results[1];
                    txtCountry.Text = results[2];
                }
                else if (results.Length > 0)
                {
                    MessageBox.Show(results[0]);
                }
                else
                {
                    MessageBox.Show("An error occurred while fetching weather data.");
                }
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshFormData();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (resultView.SelectedRows.Count > 0)
            {
                btnSearch.Enabled = false;
                DataGridViewRow selectedRow = resultView.SelectedRows[0];
                string selectedLocation = selectedRow.Cells[0].Value.ToString();
                //MessageBox.Show($"Selected location: {selectedLocation}");
                txtTemp.Clear();
                txtCountry.Clear();
                txtLocation.Clear();
                txtId.Clear();
                string temp, country, location,id;
                

                txtCountry.ReadOnly= false;
                txtLocation.ReadOnly= false;
                txtTemp.ReadOnly= false;
                txtId.ReadOnly= false;

                temp= selectedRow.Cells[2].Value.ToString();
                country= selectedRow.Cells[3].Value.ToString();
                location= selectedRow.Cells[1].Value.ToString();
                id= selectedRow.Cells[0].Value.ToString();

                txtTemp.Text= temp;
                txtCountry.Text= country;
                txtLocation.Text= location;
                txtId.Text= id;

            }
            else
            {
                MessageBox.Show("No row selected. Please select a row in the result view.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtCity.Clear();
            txtCountry.Clear();
            txtLocation.Clear();
            txtTemp.Clear();
            txtId.Clear();
            DialogResult dt = MessageBox.Show("Do You sure want to Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dt==DialogResult.Yes)
            {
                if (resultView.SelectedRows.Count > 0)
                {
                    btnSearch.Enabled = false;
                    DataGridViewRow selectedRow = resultView.SelectedRows[0];
                    string selectedLocation = selectedRow.Cells[0].Value.ToString();
                    //MessageBox.Show($"Selected location: {selectedLocation}");
                    DatabaseConnection db = new DatabaseConnection();
                    string msg = db.Delete(selectedLocation);
                    DialogResult ds = MessageBox.Show(msg, "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshFormData();

                }
                else
                {
                    MessageBox.Show("No row selected. Please select a row in the result view.");
                }
            }
            
        }
        private void RefreshFormData()
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection();
                DataTable dt = db.LoadResults();
                resultView.Rows.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    resultView.Rows.Add(dr[0].ToString(),dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtCountry.ReadOnly==false) 
            {
                DatabaseConnection db = new DatabaseConnection();
                string loc, coun, temp, id;
                coun= txtCountry.Text;
                loc= txtLocation.Text;
                temp= txtTemp.Text;
                id= txtId.Text;
                string msg = db.Update(loc, temp, coun, id);

                MessageBox.Show(msg);

                //MessageBox.Show("Hellow");
                txtCountry.ReadOnly= true;
                txtLocation.ReadOnly= true;
                txtTemp.ReadOnly= true;
                txtId.ReadOnly= true;   

                txtCountry.Clear();
                txtLocation.Clear();
                txtTemp.Clear();
                txtId.Clear();
                RefreshFormData();


            }
            else if(txtCountry.ReadOnly == true)
            {
                if (txtCountry.Text != "" && txtLocation.Text != "" && txtTemp.Text != "")
                {
                    string loca, count, tempe;
                    count = txtCountry.Text;
                    loca = txtLocation.Text;
                    tempe = txtTemp.Text;
                    DatabaseConnection db=new DatabaseConnection();
                    string msg = db.Insert(loca, count, tempe);
                    MessageBox.Show(msg);
                    RefreshFormData();

                }
            }
            
        }
    }
}
