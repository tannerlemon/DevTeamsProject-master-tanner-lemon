﻿using DevTeamsProject;
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
            Seed();
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
                    "7. Remove Developer From Team\n" +
                    "8. Create New Team\n" +
                    "9. View All Teams\n" +
                    "10. View Team By Name\n" +
                    "11. Update Existing Team\n" +
                    "12. Delete Existing Team\n" +
                    "13. List Of Developers That Need Pluralsight Access\n" +
                    "14. Exit");

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
                        // Remove developer from team
                        RemoveDeveloperFromTeam();
                        break;

                    case "8":
                        // Create new team
                        CreateNewTeam();
                        break;

                    case "9":
                        // View all teams
                        DisplayAllTeams();
                        break;

                    case "10":
                        // View team by name
                        DisplayTeamByName();
                        break;

                    case "11":
                        // Update existing team
                        UpdateExistingTeam();
                        break;

                    case "12":
                        // Delete Existing Team
                        RemoveExistingTeam();
                        break;

                    case "13":
                        // List of developers that need pluralsight
                        GetListOfPluralsightNeeders();
                        break;

                    case "14":
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
            // Check for empty string
            if (!Input(newDeveloper.Name))
            {
                return;
            }


            // ID Number
            Console.WriteLine($"\nEnter {newDeveloper.Name}'s ID number:");
            string IDasString = Console.ReadLine();
            // Check for empty string
            if (!Input(IDasString))
            {
                return;
            }

            if (IsNum(IDasString))
            {
                newDeveloper.ID = int.Parse(IDasString);
            }
            else
            {
                Console.WriteLine("Sorry, you need to enter a number here.");
                return;
            }




            // Has Access
            Console.WriteLine($"\nDoes {newDeveloper.Name} have access to Pluralsight? (y/n)");
            string hasAccessString = Console.ReadLine().ToLower();
            // Check for empty string
            if (!Input(hasAccessString))
            {
                return;
            }

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
            // Check for empty string
            if (!Input(devTeamAsString))
            {
                return;
            }

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
            _devTeamRepo.AddDeveloperToTeam(newDeveloper.ID, devTeamAsString);

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
                    $"Team(s): {developer.TeamName}\n");
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
            // Check for empty string
            if (!Input(IDasString))
            {
                return;
            }

            int IDasInt;
            if (IsNum(IDasString))
            {
                IDasInt = int.Parse(IDasString);
            }
            else
            {
                Console.WriteLine("Sorry, you need to enter a number here.");
                return;
            }


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
            string oldDeveloperAsString = Console.ReadLine();
            // Check for empty string
            if (!Input(oldDeveloperAsString))
            {
                return;
            }

            int oldDeveloper;
            if (IsNum(oldDeveloperAsString))
            {
                oldDeveloper = int.Parse(oldDeveloperAsString);
            }
            else
            {
                Console.WriteLine("Sorry, you need to enter a number here.");
                return;
            }



            if (!DeveloperExists(oldDeveloper))
            {
                return;
            }

            // Build a new object
            Developer newDeveloper = new Developer();

            // Name
            Console.WriteLine("Enter the new name of the developer:");
            newDeveloper.Name = Console.ReadLine();
            // Check for empty string
            if (!Input(newDeveloper.Name))
            {
                return;
            }

            // ID
            Console.WriteLine($"Enter the new ID for {newDeveloper.Name}:");
            newDeveloper.ID = oldDeveloper;

            // Team
            Console.WriteLine($"Enter the new team name for {newDeveloper.Name}:");
            string devTeam = Console.ReadLine();
            // Check for empty string
            if (!Input(devTeam))
            {
                return;
            }


            if (_devTeamRepo.GetTeamByName(devTeam) == null)
            {
                DevTeam newDevTeam = new DevTeam(devTeam);
                _devTeamRepo.AddDevTeam(newDevTeam);
                _devTeamRepo.AddDeveloperToTeam(newDeveloper.ID, devTeam);
            }
            else
            {
                _devTeamRepo.AddDeveloperToTeam(newDeveloper.ID, devTeam);
            }


            // Access
            Console.WriteLine($"Does {newDeveloper.Name} have access to Pluralsight? (y/n)");
            string hasAccessString = Console.ReadLine().ToLower();
            // Check for empty string
            if (!Input(hasAccessString))
            {
                return;
            }

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
                Console.WriteLine("\nSorry, I could not update the developer.");
            }
        }

        // Remove existing developer
        private void RemoveExistingDeveloper()
        {
            DisplayAllDevelopers();

            // Get the ID of the developer to remove
            Console.WriteLine("Enter the ID of the developer you would like to remove:");

            string IDasString = Console.ReadLine();
            // Check for empty string
            if (!Input(IDasString))
            {
                return;
            }

            int input;
            if (IsNum(IDasString))
            {
                input = int.Parse(IDasString);
            }
            else
            {
                Console.WriteLine("Sorry, you need to enter a number here.");
                return;
            }

            if (!DeveloperExists(input))
            {
                return;
            }

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
            DisplayAllDevelopers();

            // Get ID from user
            Console.WriteLine("Enter the ID of the developer you would like to add:");
            string IDasString = Console.ReadLine();
            // Check for empty string
            if (!Input(IDasString))
            {
                return;
            }

            int developerID;
            if (IsNum(IDasString))
            {
                developerID = int.Parse(IDasString);
            }
            else
            {
                Console.WriteLine("Sorry, you need to enter a number here.");
                return;
            }

            if (!DeveloperExists(developerID))
            {
                return;
            }

            // Get team name from user
            Console.WriteLine("Enter the name of the team you would like to add them to:");
            string teamName = Console.ReadLine();
            // Check for empty string
            if (!Input(teamName))
            {
                return;
            }

            // Call add method with parameters
            bool wasAdded = _devTeamRepo.AddDeveloperToTeam(developerID, teamName);

            // Ask if they want to add anymore to the team
            if (wasAdded)
            {
                Console.WriteLine($"Developer {developerID} was successfully added to {teamName}.");
                AddAnymoreQuestionMark(teamName);
            }
            else
            {
                Console.WriteLine($"Sorry, team {teamName} doesn't exist. Would you like to create it now? (y/n)");
                string yesOrNo = Console.ReadLine();
                // Check for empty string
                if (!Input(yesOrNo))
                {
                    return;
                }

                if (yesOrNo.ToLower() == "y")
                {
                    DevTeam newTeam = new DevTeam();
                    _devTeamRepo.AddDevTeam(newTeam);
                    newTeam.TeamName = teamName;

                    _devTeamRepo.AddDeveloperToTeam(developerID, teamName);

                    Console.WriteLine($"I created team {teamName} for you and added developer {developerID} to it. ");
                    AddAnymoreQuestionMark(teamName);
                }
                else
                {
                    Console.WriteLine("Okay, I won't create it.");
                }
            }

        }

        // Remove developer from team
        private void RemoveDeveloperFromTeam()
        {
            DisplayAllDevelopers();

            // Get ID from user
            Console.WriteLine("Enter the ID of the developer you would like to remove:");
            string IDasString = Console.ReadLine();
            // Check for empty string
            if (!Input(IDasString))
            {
                return;
            }

            int developerID = int.Parse(IDasString);

            if (!DeveloperExists(developerID))
            {
                return;
            }

            // Get team name from user
            Console.WriteLine("Enter the name of the team you would like to remove them from:");
            string teamName = Console.ReadLine();
            // Check for empty string
            if (!Input(teamName))
            {
                return;
            }

            // Call aremove method with parameters
            bool wasRemoved = _devTeamRepo.RemoveDeveloperFromTeam(developerID, teamName);

            // Ask if they want to add anymore to the team
            if (wasRemoved)
            {
                Console.WriteLine($"Developer {developerID} was successfully removed from {teamName}.");
            }
            else
            {
                Console.WriteLine("Sorry, that team doesn't exist.");
            }
        }

        // Create new team 
        private void CreateNewTeam()
        {
            Console.Clear();

            // New DevTeam
            DevTeam newTeam = new DevTeam();

            // Get team name
            Console.WriteLine("Enter the name of your new team:");
            newTeam.TeamName = Console.ReadLine();
            // Check for empty string
            if (!Input(newTeam.TeamName))
            {
                return;
            }

            _devTeamRepo.AddDevTeam(newTeam);

            // Ask if user wants to add any developers to team
            AddAnymoreQuestionMark(newTeam.TeamName);
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

        // View team by name
        private void DisplayTeamByName()
        {
            Console.Clear();

            // Get user's input (name)
            Console.WriteLine("Enter the name of the team you would like to view:");
            string teamName = Console.ReadLine();

            // Find that team
            DevTeam team = _devTeamRepo.GetTeamByName(teamName);

            // Display that team if not null
            if (team != null)
            {
                Console.WriteLine($"Team Name: {team.TeamName}\n" +
                    $"Member Count: {team.TeamSize}\n" +
                    $"All Members Have Pluralsight Access: {team.AllMembersHaveAccess}\n");
            }
            else
            {
                Console.WriteLine("I could not find a team by that name..");
            }
        }

        // Update Existing Team
        private void UpdateExistingTeam()
        {
            DisplayAllTeams();

            // Get team to update
            Console.WriteLine("Enter the name of the team you would like to update:");
            string teamName = Console.ReadLine();

            // Get new info - Team Name
            Console.WriteLine("Enter the new team name:");
            string newName = Console.ReadLine();

            // Update name
            bool wasUpdated = _devTeamRepo.UpdateExistingTeam(teamName, newName);

            // Display whether or not it was updated
            if (wasUpdated)
            {
                Console.WriteLine($"\nYou successfully changed {teamName} to {newName}.");
            }
            else
            {
                Console.WriteLine("The team could not be updated.");
            }
        }

        // Delete existing team
        private void RemoveExistingTeam()
        {
            DisplayAllTeams();

            // Get team name
            Console.WriteLine("Enter the name of the team you would like to delete:");
            string selectedTeam = Console.ReadLine();

            // Remove team
            bool wasDeleted = _devTeamRepo.RemoveDeveloperTeamFromList(selectedTeam);

            // State whether or not it was deleted
            if (wasDeleted)
            {
                Console.WriteLine($"{selectedTeam} was successfully deleted.");
            }
            else
            {
                Console.WriteLine($"{selectedTeam} could not be deleted.");
            }
        }

        // Get list of developers that need access -- 
        private void GetListOfPluralsightNeeders()
        {
            Console.Clear();

            Console.WriteLine($"These developers need access to Pluralsight:\n");

            List<Developer> listofDevs = KomodoDataBase._developers;
            foreach (Developer developer in listofDevs)
            {
                if (!developer.HasAccessToPluralsight)
                {
                    Console.WriteLine($"Name: {developer.Name}\n" +
                        $"ID Number: {developer.ID}\n" +
                        $"Team(s): {developer.TeamName}\n");
                }
            }
        }






        /*
         * *******************
         * Helpers
         * *******************
         */

        // ********** Need a method to check if user inputs a number or not for .Parse methods ***********


        // Seed Developer and Team Lists
        private void Seed()
        {
            DevTeam monkey = new DevTeam("Monkey");
            DevTeam ape = new DevTeam("Ape");
            DevTeam bonobo = new DevTeam("Bonobo");

            _devTeamRepo.AddDevTeam(monkey);
            _devTeamRepo.AddDevTeam(ape);
            _devTeamRepo.AddDevTeam(bonobo);

            Developer tanner = new Developer("Tanner Lemon", 456985, true);
            Developer frank = new Developer("Frank Ocean", 546884, false);
            Developer john = new Developer("John Mayer", 546666, true);

            _developerRepo.AddDeveloper(tanner);
            _developerRepo.AddDeveloper(john);
            _developerRepo.AddDeveloper(frank);

            _devTeamRepo.AddDeveloperToTeam(tanner.ID, "Monkey");
            _devTeamRepo.AddDeveloperToTeam(john.ID, "Ape");
            _devTeamRepo.AddDeveloperToTeam(frank.ID, "Monkey");
            _devTeamRepo.AddDeveloperToTeam(tanner.ID, "Bonobo");
        }

        // Add developers to team helper method
        private void AddAnymoreQuestionMark(string teamName)
        {
            DisplayAllDevelopers();

            bool keepAsking = true;

            while (keepAsking)
            {
                Console.WriteLine($"Would you like to add anymore developers to {teamName}? (y/n)");
                string input = Console.ReadLine();
                // Check for empty string
                if (!Input(input))
                {
                    return;
                }

                if (input.ToLower() == "y")
                {
                    Console.WriteLine("Enter the ID of the developer you would like to add:");
                    string IDasString = Console.ReadLine();
                    // Check for empty string
                    if (!Input(IDasString))
                    {
                        return;
                    }

                    int developerID = int.Parse(IDasString);

                    _devTeamRepo.AddDeveloperToTeam(developerID, teamName);
                    keepAsking = true;
                }
                else
                {
                    keepAsking = false;
                }
            }
        }

        // Check if developer exists
        private bool DeveloperExists(int developerID)
        {
            if (!_developerRepo.DeveloperExists(developerID))
            {
                Console.WriteLine($"Sorry, I can't find a developer with ID number {developerID}..");
                return false;
            }
            else
            {
                return true;
            }
        }

        // Check if empty input
        private bool Input(string input)
        {
            if (input == "")
            {
                Console.WriteLine("Sorry, you have to enter something for this to work.");
                return false;
            }
            else
            {
                return true;
            }
        }

        // Check if number
        private bool IsNum(string numAsString)
        {
            int num;
            return int.TryParse(numAsString, out num);
        }
    }
}
