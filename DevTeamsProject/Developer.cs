using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class Developer
    {
        private readonly List<DevTeam> _devTeams = new List<DevTeam>();


        public string Name { get; set; }
        public int ID { get; set; }
        public bool HasAccessToPluralsight { get; set; }
        public string TeamName { get; set; }

        public Developer() { }
        public Developer(string name, int IDnumber, bool hasAccessToPluralsight, DevTeam team)
        {
            Name = name;
            ID = IDnumber;
            HasAccessToPluralsight = hasAccessToPluralsight;
            TeamName = team.TeamName;
        }


        // Helper - Get developer's team
        public string GetDevelopersTeamName(string developerName)
        {
            foreach (DevTeam devTeam in _devTeams)
            {
                if (devTeam.listOfDevelopers.Contains(new Developer { Name = developerName }))
                {
                    return devTeam.TeamName;
                }
            }

            return null;
        }
    }
}
