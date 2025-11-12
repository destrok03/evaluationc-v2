using MonProjet.Repositories;
using MonProjet.Services;
using MonProjet.Views;

// Simple composition root
var repo = new InMemoryStudentRepository();
var service = new StudentService(repo);
var view = new ConsoleView(service);

Console.WriteLine("Gestion des étudiants - démarrage...");
view.Run();

Console.WriteLine("Au revoir.");
