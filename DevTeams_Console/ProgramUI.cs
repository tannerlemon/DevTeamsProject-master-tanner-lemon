using DevTeamsProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Console
{
    class ProgramUI
    {
        private DevTeamRepo _devTeamRepo = new DevTeamRepo();
        private DeveloperRepo _developerRepo = new DeveloperRepo();

        // Start
        public void Run()
        {
            SeedDeveloperList();
            Menu();
        }
        // Menu
        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                // Display options to user
                Console.WriteLine("Select a menu option:\n" +
                    "1. Create New Developer\n" +
                    "2. View All Developers\n" +
                    "3. View Developer By ID\n" +
                    "4. Update Existing Developer\n" +
                    "5. Delete Existing Developer\n" +
                    "6. Add Developer To Team\n" +
                    "7. Create New Team\n" +
                    "8. View All Teams\n" +
                    "9. View Team By Name\n" +
                    "10. Update Existing Team\n" +
                    "11. Delete Existing Team\n" +
                    "12. Exit");

                // Get user's input
                string input = Console.ReadLine();

                // Evaluate the user's input and act accordingly
                switch (input)
                {
                    case "1":
                        // Create new developer
                        CreateNewDeveloper();
                        break;

                    case "2":
                        // View all developers
                        DisplayAllDevelopers();
                        break;

                    case "3":
                        // View developer by name
                        DisplayDeveloperByID();
                        break;

                    case "4":
                        // Update Existing Developer
                        UpdateExistingDeveloper();
                        break;

                    case "5":
                        // Delete Existing Developer
                        RemoveExistingDeveloper();
                        break;

                    case "6":
                        // Add developer to team
                        AddDeveloperToTeam();
                        break;

                    case "7":
                        // Create new team
                        CreateNewTeam();
                        break;

                    case "8":
                        // View all teams
                        DisplayAllTeams();
                        break;

                    case "9":
                        // View team by name
                        break;

                    case "10":
                        // Update existing team
                        break;

                    case "11":
                        // Delete Existing Team
                        break;

                    case "12":
                        // Exit
                        Console.WriteLine("Goodbye!");
                        keepRunning = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid number..");
                        break;

                }

                Console.WriteLine("\nPlease press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // Create New Developer
        private void CreateNewDeveloper()
        {
            Console.Clear();

            var newDeveloper = new Developer();


            // Name
            Console.WriteLine("Enter the name of the developer:");
            newDeveloper.Name = Console.ReadLine();


            // ID Number
            Console.WriteLine($"\nEnter {newDeveloper.Name}'s ID number:");
            string IDasString = Console.ReadLine();
            newDeveloper.ID = int.Parse(IDasString);


            // Has Access
            Console.WriteLine($"\nDoes {newDeveloper.Name} have access to Pluralsight? (y/n)");
            string hasAccessString = Console.ReadLine().ToLower();

            if (hasAccessString == "y")
            {
                newDeveloper.HasAccessToPluralsight = true;
            }
            else
            {
                newDeveloper.HasAccessToPluralsight = false;
            }


            // Team Name
            Console.WriteLine($"\nEnter the team name that {newDeveloper.Name} belongs to:");
            string devTeamAsString = Console.ReadLine();
            newDeveloper.TeamName = devTeamAsString;

            if (_devTeamRepo.GetTeamByName(devTeamAsString) == null)
            {
                DevTeam newDevTeam = new DevTeam(devTeamAsString);
                _devTeamRepo.AddDevTeam(newDevTeam);
            }
            else
            {
                _devTeamRepo.AddDeveloperToTeam(newDeveloper.ID, devTeamAsString);
            }


            _developerRepo.AddDeveloper(newDeveloper);

        }

        // Display All Developers
        private void DisplayAllDevelopers()
        {
            Console.Clear();
            List<Developer> listofDevelopers = _developerRepo.GetDeveloperList();

            foreach (Developer developer in listofDevelopers)
            {
                Console.WriteLine($"Name: {developer.Name}\n" +
                    $"ID: {developer.ID}\n" +
                    $"Pluralsight Access: {developer.HasAccessToPluralsight}\n" +
                    $"Team: {developer.TeamName}\n");
            }
        }

        // View Developer By Name
        private void DisplayDeveloperByID()
        {
            Console.Clear();

            // Prompt user to give name
            Console.WriteLine("Enter the ID of the developer you would like to view:");

            // Get user's input
            string IDasString = Console.ReadLine();
            int IDasInt = int.Parse(IDasString);

            // Find developer by that name
            Developer developer = _developerRepo.GetDeveloperByID(IDasInt);

            // Display said developer if it isn't null
            if (developer != null)
            {
                Console.WriteLine($"\nName: {developer.Name}\n" +
                     $"ID: {developer.ID}\n" +
                     $"Pluralsight Access: {developer.HasAccessToPluralsight}\n" +
                     $"Team: {developer.TeamName}\n");
            }
        }

        // Update developer
        private void UpdateExistingDeveloper()
        {
            // Display all developers
            DisplayAllDevelopers();

            // Ask which developer to update
            Console.WriteLine("\nEnter the ID of the developer you would like to update:");

            // Get that developer
            int oldDeveloper = int.Parse(Console.ReadLine());

            // Build a new object
            Developer newDeveloper = new Developer();

            // Name
            Console.WriteLine("Enter the new name of the developer:");
            newDeveloper.Name = Console.ReadLine();

            // ID
            //Console.WriteLine($"Enter the new ID for {newDeveloper.Name}:");
            newDeveloper.ID = oldDeveloper;

            // Team
            Console.WriteLine($"Enter the new team name for {newDeveloper.Name}:");
            newDeveloper.TeamName = Console.ReadLine();

            //// Not updating team name...
            //devTeamAsString = newDeveloper.TeamName;
            // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^


            if (_devTeamRepo.GetTeamByName(newDeveloper.TeamName) == null)
            {
                DevTeam newDevTeam = new DevTeam(newDeveloper.TeamName);
                _devTeamRepo.AddDevTeam(newDevTeam);
                _devTeamRepo.AddDeveloperToTeam(newDeveloper.ID, newDeveloper.TeamName);
            }
            else
            {
                _devTeamRepo.AddDeveloperToTeam(newDeveloper.ID, newDeveloper.TeamName);
            }


            // Access
            Console.WriteLine($"Does {newDeveloper.Name} have access to Pluralsight? (y/n)");
            string hasAccessString = Console.ReadLine().ToLower();

            if (hasAccessString == "y")
            {
                newDeveloper.HasAccessToPluralsight = true;
            }
            else
            {
                newDeveloper.HasAccessToPluralsight = false;
            }

            // Verify update worked
            bool wasUpdated = _developerRepo.UpdateExistingDeveloper(oldDeveloper, newDeveloper);

            if (wasUpdated)
            {
                Console.WriteLine($"\n{newDeveloper.Name} was successfully updated.");
            }
            else
            {
                Console.WriteLine("\nCould not update developer.");
            }
        }

        // Remove existing developer
        private void RemoveExistingDeveloper()
        {
            DisplayAllDevelopers();

            // Get the ID of the developer to remove
            Console.WriteLine("Enter the ID of the developer you would like to remove:");

            int input = int.Parse(Console.ReadLine());

            // Call the delete method
            bool wasDeleted = _developerRepo.RemoveDeveloperFromList(input);

            //If was deleted, say so
            if (wasDeleted)
            {
                Console.WriteLine($"Developer {input} was successfully deleted.");
            }
            else
            {
                Console.WriteLine("The developer could not be deleted.");
            }
        }

        // Add developer to team
        private void AddDeveloperToTeam()
        {
            Console.Clear();

            // Get ID from user
            Console.WriteLine("Enter the ID of the developer you would like to add:");
            int developerID = int.Parse(Console.ReadLine());

            // Get team name from user
            Console.WriteLine("Enter the name of the team you would like to add them to:");
            string teamName = Console.ReadLine();

            // Call add method with parameters
            _devTeamRepo.AddDeveloperToTeam(developerID, teamName);
        }

        private void CreateNewTeam()
        {
            Console.Clear();

            // New DevTeam
            DevTeam newTeam = new DevTeam();

            // Get team name
            Console.WriteLine("Enter the name of your new team:");
            newTeam.TeamName = Console.ReadLine();

            // Ask if user wants to add any developers to team
            Console.WriteLine($"Do you want to add any developers to {newTeam.TeamName}? (y/n)");
            string input = Console.ReadLine();

            // If yes, call add funtion
            if (input.ToLower() == "y")
            {
                Console.WriteLine("Enter the ID of the developer you would like to add:");
                int developerID = int.Parse(Console.ReadLine());

                _devTeamRepo.AddDeveloperToTeam(developerID, newTeam.TeamName);

                // Ask if anymore
                Console.WriteLine($"Would you like to add anymore developers to {newTeam.TeamName}? (y/n)");
            }



        }


        // View List of Teams
        private void DisplayAllTeams()
        {
            Console.Clear();

            List<DevTeam> listofTeams = _devTeamRepo.GetDevTeamsList();

            foreach (DevTeam devTeam in listofTeams)
            {
                Console.WriteLine($"Team Name: {devTeam.TeamName}\n" +
                    $"Member Count: {devTeam.TeamSize}\n" +
                    $"All Members Have Pluralsight Access: {devTeam.AllMembersHaveAccess}\n");
            }
        }





        // Seed Developer List
        private void SeedDeveloperList()
        {
            Developer tanner = new Developer("Tanner Lemon", 456985, true, new DevTeam("Monkey"));
            Developer frank = new Developer("Frank Ocean", 546884, false, new DevTeam("Monkey"));
            Developer john = new Developer("John Mayer", 546666, true, new DevTeam("Ape"));

            _developerRepo.AddDeveloper(tanner);
            _developerRepo.AddDeveloper(john);
            _developerRepo.AddDeveloper(frank);
        }
    }
}
