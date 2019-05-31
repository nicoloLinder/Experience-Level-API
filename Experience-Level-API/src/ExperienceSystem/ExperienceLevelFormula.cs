namespace ExperienceSystem
{
    public interface ExperienceLevelFormula
    {
        /// <summary>
        ///     Calculate the level given an experience amount
        /// </summary>
        /// <param name="experience">The experience amount</param>
        /// <returns>The level value corresponding to the experience amount</returns>
        long CalculateLevel(long experience);

        /// <summary>
        ///     Calculate the experience level given a level value
        /// </summary>
        /// <param name="level">The level value</param>
        /// <returns>The experience amount corresponding to the level value</returns>
        long CalculateExperience(long level);
    }
}