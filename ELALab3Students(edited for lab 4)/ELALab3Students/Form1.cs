using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELALab3Students
{
    public partial class Form1 : Form
    {
        string firstNameField, lastNameField, facultyField;
        int idNum;
        StudentsDAO dataAccess;

        public Form1()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (isEmpty()) { MessageBox.Show("Fill all fields"); }
            else
            {
                firstNameField = firstNameTextBox.Text.ToString();
                lastNameField = lastNameTextBox.Text.ToString();
                facultyField = facultyTextBox.Text.ToString();
                idNum = Convert.ToInt32(IDTextBox.Text.ToString());
                Student st = new Student
                {
                    student_id = idNum,
                    first_name = firstNameField,
                    last_name = lastNameField,
                    faculty = facultyField
                };
                if (dataAccess.insertStudent(st))
                {
                    MessageBox.Show("Student added successfully.");
                    LoadToGrid();
                }
                else MessageBox.Show("Try again");
            }
            FieldsRefresh();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string idNumString = IDTextBox.Text.ToString();
            fieldLabel.Visible = false;

            if (idNumString.Equals("")) 
                MessageBox.Show("Enter ID number to remove student"); 
            else{
                int studentId = Convert.ToInt32(IDTextBox.Text.ToString());
                if (dataAccess.deleteStudent(idNum)){
                    MessageBox.Show("Student deleted successfully.");
                    LoadToGrid();
                }
                else MessageBox.Show("Try again");

            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            fieldLabel.Visible = false;
            LoadToGrid();
        }

        private void LoadToGrid()
        {
            dataGridView1.DataSource = dataAccess.takeAllData();
            dataGridView1.Refresh();
            FieldsRefresh();
        }

        public void FieldsRefresh()
        {
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            facultyTextBox.Text = "";
            IDTextBox.Text = "";
        }

        public bool isEmpty()
        {
            firstNameField = firstNameTextBox.Text.ToString();
            lastNameField = lastNameTextBox.Text.ToString();
            facultyField = facultyTextBox.Text.ToString();
            string idNumString = IDTextBox.Text.ToString();

            if (firstNameField.Equals("") || lastNameField.Equals("") || facultyField.Equals("") || idNumString.Equals(""))
            {
                fieldLabel.Visible = true;
                return true;
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataAccess = new StudentsDAO();
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            firstNameField = firstNameTextBox.Text.ToString();
            lastNameField = lastNameTextBox.Text.ToString();
            facultyField = facultyTextBox.Text.ToString();
            if (IDTextBox.Text!="") idNum = Convert.ToInt32(IDTextBox.Text.ToString());
            Student tempStudent = new Student();


            if (!IDTextBox.Text.ToString().Equals(""))
            {
                tempStudent = dataAccess.findById(idNum);
                 MessageBox.Show(stringStudent(tempStudent));
            }
            else 
            {
                if (!firstNameField.Equals(""))
                {
                    tempStudent = dataAccess.findByName(firstNameField, 1);
                    MessageBox.Show(stringStudent(tempStudent));
                }

                else if (!lastNameField.Equals(""))
                {
                    dataAccess.findByName(lastNameField, 4);
                    MessageBox.Show(stringStudent(tempStudent));
                }

                else MessageBox.Show("Enter query");
            }
            FieldsRefresh();
        }

        private string stringStudent(Student st)
        {
            if (st == null) return "No such student";
            return "ID: " + st.student_id + ". Name: " + st.first_name + " " + st.last_name + ", " + st.faculty + " ";
        }
    }
}
