using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class DevTeam
    {
        public string TeamName { get; set; }
        public int TeamSize { get { return listOfDevelopers.Count; } }
        public bool AllMembersHaveAccess { get { return CheckAccess(listOfDevelopers); } }
        public List<Developer> listOfDevelopers { get; set; } = new List<Developer>();

        public DevTeam() { }

        public DevTeam(string teamName)
        {
            TeamName = teamName;
        }

        // Check access
        public bool CheckAccess(List<Developer> developerDirectory)
        {
            int theyHaveAccess = 0;

            foreach (Developer developer in developerDirectory)
            {
                if (developer.HasAccessToPluralsight == true)
                {
                    theyHaveAccess++;
                }
            }

            if (theyHaveAccess == developerDirectory.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
