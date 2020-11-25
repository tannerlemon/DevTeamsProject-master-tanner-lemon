using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class DeveloperRepo
    {
        private readonly List<Developer> _developerDirectory = KomodoDataBase._developers;


        //Developer Create
        public void AddDeveloper(Developer developer)
        {
            _developerDirectory.Add(developer);
        }


        //Developer Read
        public List<Developer> GetDeveloperList()
        {
            return _developerDirectory;
        }


        //Developer Update
        public bool UpdateExistingDeveloper(int originalIDNumber, Developer newDeveloper)
        {
            Developer oldDeveloper = GetDeveloperByID(originalIDNumber);

            if (oldDeveloper != null)
            {
                oldDeveloper.Name = newDeveloper.Name;
                oldDeveloper.ID = newDeveloper.ID;
                oldDeveloper.HasAccessToPluralsight = newDeveloper.HasAccessToPluralsight;

                return true;
            }
            else
            {
                return false;
            }
        }


        //Developer Delete
        public bool RemoveDeveloperFromList(int IDNumber)
        {
            Developer developer = GetDeveloperByID(IDNumber);

            if (developer == null)
            {
                return false;
            }

            int initialCount = _developerDirectory.Count;
            _developerDirectory.Remove(developer);

            if (initialCount > _developerDirectory.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Developer Helper (Get Developer by ID)
        public Developer GetDeveloperByID(int IDNumber)
        {
            foreach (Developer developer in _developerDirectory)
            {
                if (developer.ID == IDNumber)
                {
                    return developer;
                }
            }

            return null;
        }
    }
}
