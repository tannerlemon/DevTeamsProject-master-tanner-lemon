using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class Developer
    {
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
    }
}
