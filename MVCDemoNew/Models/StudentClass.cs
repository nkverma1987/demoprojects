using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCDemoNew.Entity;
using System.Data.SqlClient;
using MVCDemoNew.DI;
using System.ComponentModel.DataAnnotations;

namespace MVCDemoNew.Models
{
    public class StudentClass : IStudent
    {
        public int StudentID { get; set; }
        public string City { get; set; }
        public string Class { get; set; }
        [Required(ErrorMessage = "Please enter Name!")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string EnrollYear { get; set; }
        public Country AssociatedCountry { get; set; }

        NitisDBDataContext db = new NitisDBDataContext();
        public IEnumerable<StudentClass> GetStudents()
        {
            List<StudentClass> students = new List<Models.StudentClass>();
            var selectStudents = from s in db.Students
                                 join c in db.tblCountries on s.Country equals c.Id
                                 select new {
               s.StudentID, s.City,s.Class,s.Email,s.EnrollYear,s.Name,CountryName=c.Name,c.Id
            };
            foreach(var item in selectStudents)
            {
                StudentClass student = new Models.StudentClass
                {
                    StudentID = item.StudentID,
                    Name = item.Name,
                    City = item.City,
                    Class = item.Class,
                    Email = item.Email,
                    EnrollYear = item.EnrollYear,
                    AssociatedCountry=new Country { Id=item.Id,Name=item.CountryName}
                };
                students.Add(student);
            }
            return students;
        }

        public Student GetDetail(int id)
        {
            return db.Students.SingleOrDefault(i => i.StudentID == id);
        }
        public bool Save(MVCDemoNew.Entity.Student student, out string msg)
        {
            msg = "Not Saved";
            var saved = false;
            try
            {
                db.Students.InsertOnSubmit(student);
                db.SubmitChanges();
                msg = "Saved";
                saved = false;
            }
            catch (Exception ex)
            {
                saved = true;
                throw ex;
            }
            return saved;
        }

        public bool Update(Student student, out string msg)
        {
            msg = "Not Saved";
            var saved = false;
            try
            {
                db = new NitisDBDataContext();
                Student std = db.Students.SingleOrDefault(i => i.StudentID == student.StudentID);
                std.Name = student.Name;
                std.Email = student.Email;
                std.Class = student.Class;
                std.EnrollYear = student.EnrollYear;
                std.City = student.City;
                std.Country = student.Country;
                db.SubmitChanges();
                msg = "Updated";
                saved = false;
            }
            catch (Exception ex)
            {
                saved = true;
                throw ex;
            }
            return saved;
        }

        public bool Delete(int id, out string msg)
        {
            msg = "Not Deleted";
            var saved = false;
            try
            {
                Student std = db.Students.SingleOrDefault(i => i.StudentID == id);
                db.Students.DeleteOnSubmit(std);
                db.SubmitChanges();
                msg = "Deleted";
                saved = false;
            }
            catch (Exception ex)
            {
                saved = true;
                throw ex;
            }
            return saved;
        }
    }
    public class ADOProcess
    {
        private string name;
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings["NitishConnectionString"].ConnectionString.ToString();
        SqlConnection con = null;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public void Add(out string msg)
        {
            msg = string.Empty;
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SP_AddStudent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = this.Name,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input
                };
                SqlParameter paramMessage = new SqlParameter
                {
                    ParameterName = "@Message",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 4000,
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramMessage);
                con.Open();
                cmd.ExecuteNonQuery();
                msg = paramMessage.Value.ToString();
            }
        }
    }
}