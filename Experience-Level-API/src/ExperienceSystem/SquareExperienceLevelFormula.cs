using System;

namespace ExperienceSystem
{
    public class SquareExperienceLevelFormula : ExperienceLevelFormula
    {
        private readonly double _constant = 0.1;

        public SquareExperienceLevelFormula()
        {
        }

        public SquareExperienceLevelFormula(double constant)
        {
            _constant = constant;
        }

        public long CalculateLevel(long experience)
        {
            return (long) (_constant * Math.Sqrt(experience));
        }

        public long CalculateExperience(long level)
        {
            return (long) Math.Pow(level / _constant, 2);
        }
    }
}