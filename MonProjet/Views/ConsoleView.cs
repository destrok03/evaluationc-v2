using System;
using System.Globalization;
using MonProjet.Services;
using MonProjet.Entities;

namespace MonProjet.Views
{
    public class ConsoleView
    {
        private readonly StudentService _service;

        public ConsoleView(StudentService service)
        {
            _service = service;
        }

        public void Run()
        {
            while (true)
            {
                PrintMenu();
                Console.Write("Choix: ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;

                if (!int.TryParse(input, out var choice))
                {
                    Console.WriteLine("Veuillez entrer un nombre valide.");
                    continue;
                }

                switch (choice)
                {
                    case 1: AddStudent(); break;
                    case 2: ShowStudents(); break;
                    case 3: AddGrade(); break;
                    case 4: ShowStudentGrades(); break;
                    case 5: DeleteStudent(); break;
                    case 6: ShowBestStudent(); break;
                    case 7: ShowClassAverage(); break;
                    case 8: return;
                    default: Console.WriteLine("Option inconnue."); break;
                }
                PauseAfterAction();
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Que souhaitez-vous faire ?");
            Console.WriteLine("1. Ajouter un etudiant");
            Console.WriteLine("2. Afficher les Etudiants");
            Console.WriteLine("3. Ajouter une note à un etudiant");
            Console.WriteLine("4. Afficher les notes d'un etudiant avec l’appreciation");
            Console.WriteLine("5. Supprimer un Etudiant");
            Console.WriteLine("6. Afficher le Meilleur etudiant");
            Console.WriteLine("7. Afficher la moyenne de la classe");
            Console.WriteLine("8. Quitter");
        }

        private void AddStudent()
        {
            Console.Write("Prénom: ");
            var fn = Console.ReadLine() ?? string.Empty;
            Console.Write("Nom: ");
            var ln = Console.ReadLine() ?? string.Empty;
            var s = _service.AddStudent(fn.Trim(), ln.Trim());
            Console.WriteLine($"Étudiant ajouté : {s}");
        }

        private void ShowStudents()
        {
            var all = _service.GetAll();
            Console.WriteLine("Liste des étudiants :");
            foreach (var s in all)
            {
                // Afficher Id numérique clairement
                Console.WriteLine($"Id: {s.Id} - {s.FirstName} {s.LastName} - Moy: {s.Average:F2}");
            }
        }

        private void AddGrade()
        {
            Console.Write("Id de l'étudiant: ");
            if (!long.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Id invalide.");
                return;
            }
            Console.Write("Matière: ");
            var subj = Console.ReadLine() ?? string.Empty;
            Console.Write("Note (ex: 14.5): ");
            var noteStr = Console.ReadLine();
            if (!double.TryParse(noteStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
            {
                if (!double.TryParse(noteStr, out value))
                {
                    Console.WriteLine("Note invalide.");
                    return;
                }
            }

            var ok = _service.AddGrade(id, subj.Trim(), value);
            Console.WriteLine(ok ? "Note ajoutée." : "Étudiant introuvable.");
        }

        private void ShowStudentGrades()
        {
            Console.Write("Id de l'étudiant: ");
            if (!long.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Id invalide.");
                return;
            }

            var (student, grades) = _service.GetStudentGrades(id);
            if (student == null)
            {
                Console.WriteLine("Étudiant introuvable.");
                return;
            }

            Console.WriteLine(student);
            if (!grades.Any())
            {
                Console.WriteLine("Aucune note.");
                return;
            }

            foreach (var g in grades)
            {
                Console.WriteLine(g);
            }
        }

        private void DeleteStudent()
        {
            Console.Write("Id de l'étudiant à supprimer: ");
            if (!long.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Id invalide.");
                return;
            }
            var ok = _service.DeleteStudent(id);
            Console.WriteLine(ok ? "Étudiant supprimé." : "Étudiant introuvable.");
        }

        private void PauseAfterAction()
        {
            Console.WriteLine();
            Console.WriteLine("Appuyez sur Entrée pour continuer...");
            Console.ReadLine();
        }

        private void ShowBestStudent()
        {
            var s = _service.GetBestStudent();
            if (s == null)
            {
                Console.WriteLine("Aucun étudiant ou aucune note.");
                return;
            }
            Console.WriteLine($"Meilleur étudiant : {s}");
        }

        private void ShowClassAverage()
        {
            var avg = _service.GetClassAverage();
            Console.WriteLine($"Moyenne de la classe : {avg:F2}");
        }
    }
}
