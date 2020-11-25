﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class DevTeamRepo
    {
        private readonly DeveloperRepo _developerRepo = new DeveloperRepo(); // this gives you access to the _developerDirectory so you can access existing Developers and add them to a team
        private readonly List<DevTeam> _devTeams = new List<DevTeam>();

        //DevTeam Create
        public void AddDevTeam(DevTeam devTeam)
        {
            _devTeams.Add(devTeam);
        }


        //DevTeam Read
        public List<DevTeam> GetDevTeamsList()
        {
            return _devTeams;
        }


        //DevTeam Update
        public bool UpdateExistingTeam(string originalTeamName, DevTeam newTeam)
        {
            DevTeam oldTeam = GetTeamByName(originalTeamName);

            if (oldTeam != null)
            {
                oldTeam.TeamName = newTeam.TeamName;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddDeveloperToTeam(int developerID, string devTeamName)
        {
            // Get existing developer
            Developer selectedDeveloper = _developerRepo.GetDeveloperByID(developerID);

            // Get team
            DevTeam selectedTeam = GetTeamByName(devTeamName);

            // Add developer to team
            if (selectedTeam != null)
            {
                selectedTeam.listOfDevelopers.Add(selectedDeveloper);
            }
            else
            {
                Console.WriteLine("\nSorry, that team doesn't exist.");
            }

        }


        //DevTeam Delete
        public bool RemoveDeveloperTeamFromList(string teamName)
        {
            DevTeam devTeam = GetTeamByName(teamName);

            if (devTeam == null)
            {
                return false;
            }

            int initialCount = _devTeams.Count;
            _devTeams.Remove(devTeam);

            if (initialCount > _devTeams.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //DevTeam Helper (Get Team by Name)
        public DevTeam GetTeamByName(string teamName)
        {
            foreach (DevTeam devTeam in _devTeams)
            {
                if (devTeam.TeamName.ToLower() == teamName.ToLower())
                {
                    return devTeam;
                }
            }

            return null;
        }
    }
}
