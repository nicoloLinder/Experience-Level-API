using System;

namespace ExperienceAPI
{
    public class LinearExperienceLevelFormula : ExperienceLevelFormula
    {
        public long CalculateLevel(long experience)
        {
            return experience / 100;
        }

        public long CalculateExperience(long level)
        {
            return level * 100;
        }
    }
}