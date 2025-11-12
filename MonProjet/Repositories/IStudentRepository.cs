using System.Collections.Generic;
using MonProjet.Entities;

namespace MonProjet.Repositories
{
    public interface IStudentRepository
    {
        void Add(Student student);
        IEnumerable<Student> GetAll();
        Student? GetById(long id);
        bool Delete(long id);
        void Update(Student student);
    }
}
