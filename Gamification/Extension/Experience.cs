using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public class Experience
    {
        public string Name { get; private set; }
        public string Alias { get; private set; }
        public Int64 ExperiencePoints { get; private set; }
        public Int64 ExperienceNextLevel { get; private set; }
        public int Level { get; private set; }
        public string LevelPropertiesFile{get; private set;}

        public Experience(string name, string lvlPropFile, string alias)
        {
            Alias = alias;
            Name = name;
            LevelPropertiesFile = lvlPropFile;
            ExperiencePoints = 0;
            ExperienceNextLevel = 0;
            Level = 1;
        }

        public void AddExperience(long exp)
        {
            ExperiencePoints += exp;
            CalculateLevel();
        }

        protected void CalculateLevel()
        {           
            var currentLevel= 0;
            using (var reader = new StreamReader(LevelPropertiesFile))
            {
                while (!reader.EndOfStream)
                {
                    var levelExp = Convert.ToInt64(reader.ReadLine().Split('=')[1]);                    
                    if (ExperiencePoints >= levelExp)
                    {
                        currentLevel++;
                    }
                    else
                    {
                        Level = currentLevel;
                        ExperienceNextLevel = levelExp;
                        break;
                    }
                }
            }
        }

        public void AddPluginExperience(Experience exp)
        {
            AddExperience(exp.ExperiencePoints);
        }
    }
}
