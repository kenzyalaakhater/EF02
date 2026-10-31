using EF02.Data;
using EF02.Models;
namespace EF02
{
    internal class Program
    {
        static void Main()
        {
            using var context = new AppDbContext();

            Console.WriteLine($"Database exists: {context.Database.CanConnect()}");

            var organizer = new Organizer
            {
                Name = "Sara Ahmed",
                CompanyName = "Bright Events"
            };

            context.Organizers.Add(organizer);
            context.SaveChanges();

            Console.WriteLine("Organizer saved.");
        }
    }
}