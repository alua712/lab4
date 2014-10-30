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
    public class StudentsDAO
    {
        StudentsDatabaseEntities studentContext = new StudentsDatabaseEntities();

        public bool insertStudent(Student student)
        {
            try
            {
                studentContext.Students.Add(student);
                studentContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteStudent(int idNum)
        {
            try
            {
                Student st = studentContext.Students.First(i => i.student_id == idNum);
                studentContext.Students.Remove(st);
                studentContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool updateStudent()
        {
            return true;
        }

        public List<Student> takeAllData()
        {
            var load = from g in studentContext.Students select g;
            if (load != null)
            {
                List<Student> st = load.ToList<Student>();
                return st;
            }
            return null;
        }

        public Student findById(int id)
        {
            try
            {
                Student st = studentContext.Students.First(i => i.student_id == id);
                return st;
            }
            catch {
                return null;
            }
            
        }

        public Student findByName(string name, int mode)
        {
            if (mode == 1)
            {
                try
                {
                    Student st = studentContext.Students.First(i => i.first_name == name);
                    return st;
                }
                catch
                {
                    return null;
                }
            }


            try
            {
                Student st = studentContext.Students.First(i => i.last_name == name);
                return st;
            }
            catch
            {
                return null;
            }
        }
    }
}
