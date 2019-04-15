using MVCDemoNew.Entity;
using MVCDemoNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemoNew.DI
{
   public interface IStudent
    {
        IEnumerable<StudentClass> GetStudents();
        Student GetDetail(int id);
        bool Save(Student student, out string msg);
        bool Update(Student student, out string msg);
        bool Delete(int id, out string msg);
    }
}
