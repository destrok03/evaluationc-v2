using System;

namespace MonProjet.Entities
{
    public class Grade
    {
        public string Subject { get; set; } = string.Empty;
        public double Value { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string Appreciation
        {
            get
            {
                if (Value >= 16) return "Excellent";
                if (Value >= 14) return "Très bien";
                if (Value >= 12) return "Bien";
                if (Value >= 10) return "Passable";
                return "Insuffisant";
            }
        }

        public override string ToString()
        {
            return $"{Subject}: {Value:F2} ({Appreciation}) le {Date:yyyy-MM-dd}";
        }
    }
}
