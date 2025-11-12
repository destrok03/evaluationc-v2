using System.Collections.Generic;
using System.Linq;
using MonProjet.Entities;
using MonProjet.Repositories;

namespace MonProjet.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public Student AddStudent(string firstName, string lastName)
        {
            var s = new Student { FirstName = firstName, LastName = lastName };
            _repo.Add(s);
            return s;
        }

        public IEnumerable<Student> GetAll() => _repo.GetAll();

        public bool AddGrade(long studentId, string subject, double value)
        {
            var s = _repo.GetById(studentId);
            if (s == null) return false;
            s.Grades.Add(new Grade { Subject = subject, Value = value });
            _repo.Update(s);
            return true;
        }

        public (Student? student, IEnumerable<Grade> grades) GetStudentGrades(long id)
        {
            var s = _repo.GetById(id);
            if (s == null) return (null, Enumerable.Empty<Grade>());
            return (s, s.Grades);
        }

        public bool DeleteStudent(long id) => _repo.Delete(id);

        public Student? GetBestStudent()
        {
            return _repo.GetAll().OrderByDescending(s => s.Average).FirstOrDefault();
        }

        public double GetClassAverage()
        {
            var allGrades = _repo.GetAll().SelectMany(s => s.Grades);
            if (!allGrades.Any()) return 0;
            return allGrades.Average(g => g.Value);
        }
    }
}
