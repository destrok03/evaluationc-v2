using System.Collections.Generic;
using System.Linq;

namespace MonProjet.Entities
{
    public class Student
    {
        // Numeric identifier (sequential when using InMemory repository)
        public long Id { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Grade> Grades { get; set; } = new();

        public double Average => Grades.Count == 0 ? 0 : Grades.Average(g => g.Value);

        public override string ToString()
        {
            return $"{FirstName} {LastName} (Id: {Id}) - Moy: {Average:F2}";
        }
    }
}
