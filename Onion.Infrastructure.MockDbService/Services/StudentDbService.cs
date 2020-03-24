using Onion.Domain.Entities;
using Onion.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onion.Infrastructure.MockDbService.Services
{
    public class StudentDbService : IStudentDbService
    {
        private StudentDbContext _context = new StudentDbContext();
        public StudentDbService()
        {
          
        }
        public bool EnrollStudent(Student newStudent, int semestr)
        {
            _context.Student.Add(newStudent);
            return true;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _context.Student.ToList();
        }
    }
}
