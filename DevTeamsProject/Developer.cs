using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class Developer
    {
        private readonly DeveloperRepo _developerRepo = new DeveloperRepo();

        public string Name { get; set; }
        public int ID { get; set; }
        public bool HasAccessToPluralsight { get; set; }
        public string TeamName { get { return GetTeamNames(ID); } }

        public Developer() { }
        public Developer(string name, int IDnumber, bool hasAccessToPluralsight)
        {
            Name = name;
            ID = IDnumber;
            HasAccessToPluralsight = hasAccessToPluralsight;
        }


        // Get developer's teams
        public string GetTeamNames(int devID)
        {

            List<DevTeam> listOfTeams = KomodoDataBase._teams;
            List<DevTeam> listOfTeamsForDeveloper = new List<DevTeam>();

            string stringList = "";

            // get developer
            Developer selectedDev = _developerRepo.GetDeveloperByID(devID);

            // for each team
            foreach (DevTeam team in listOfTeams)
            {
                // check if given developer is on that team
                bool doesContainDeveloper = team.listOfDevelopers.Contains(selectedDev);

                // if they are, add the team to the listOfTeamsForDeveloper list
                if (doesContainDeveloper)
                {
                    listOfTeamsForDeveloper.Add(team);
                    stringList += team.TeamName + ", ";
                }
            }

            // return the list
            return stringList;

        }


    }
}
