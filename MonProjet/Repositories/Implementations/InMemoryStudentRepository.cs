using System.Collections.Generic;
using System.Linq;
using MonProjet.Entities;

namespace MonProjet.Repositories
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();
        private long _nextId = 1;

        public void Add(Student student)
        {
            if (student.Id == 0)
            {
                student.Id = _nextId++;
            }
            _students.Add(student);
        }

        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public Student? GetById(long id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public bool Delete(long id)
        {
            var s = GetById(id);
            if (s == null) return false;
            return _students.Remove(s);
        }

        public void Update(Student student)
        {
            var idx = _students.FindIndex(s => s.Id == student.Id);
            if (idx >= 0) _students[idx] = student;
        }
    }
}
