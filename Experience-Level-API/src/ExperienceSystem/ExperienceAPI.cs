using CharacterSystem;

namespace ExperienceSystem
{
    public static class ExperienceAPI
    {
        #region Get / Set Level

        /// <summary>
        /// Get the current level of an character
        /// </summary>
        /// <param name="characterID">The id of the character</param>
        /// <returns>Returns the current level of the character</returns>
        public static long GetCurrentLevel(string characterID)
        {
            return CharacterDB.FindCharacter(characterID).Level;
        }

        /// <summary>
        /// Set the current level of an character
        /// </summary>
        /// <param name="characterID">The id of the character</param>
        /// <param name="level">The level value to set</param>
        public static void SetCurrentLevel(string characterID, long level)
        {
            CharacterDB.FindCharacter(characterID).Level = level;
        }

        #endregion

        #region Experience Calculations

        /// <summary>
        /// Calculate the experience amount at a given level
        /// </summary>
        /// <param name="characterID">The ID of the character</param>
        /// <param name="level">The level value to calculate the experience from</param>
        /// <returns>Returns the calculated experience amount at a given level value</returns>
        public static long CalculateExperience(string characterID, long level)
        {
            var character = CharacterDB.FindCharacter(characterID);
            return CalculateExperience(character, level);
        }

        /// <summary>
        /// Calculate the experience amount at a given level
        /// </summary>
        /// <param name="character">The character</param>
        /// <param name="level">The level value to calculate the experience from</param>
        /// <returns>The calculated experience amount at a given level value</returns>
        public static long CalculateExperience(Character character, long level)
        {
            return CalculateExperience(character.ExperienceLevelFormula, level);
        }

        /// <summary>
        /// Calculate the experience amount at a given level
        /// </summary>
        /// <param name="experienceLevelFormula">The experience-level conversion formula</param>
        /// <param name="level">The level value to calculate the experience from</param>
        /// <returns>The calculated experience amount at a given level value</returns>
        public static long CalculateExperience(ExperienceLevelFormula experienceLevelFormula, long level)
        {
            return experienceLevelFormula.CalculateExperience(level);
        }

        //    CALCULATE REMAINING EXPERIENCE

        /// <summary>
        /// Calculate the amount of experience needed for an character to reach the next level
        /// </summary>
        /// <param name="characterID">The character's id/param>
        /// <returns>Returns the amount of experience needed to reach the next level</returns>
        public static long CalculateRemainingExperience(string characterID)
        {
            var character = CharacterDB.FindCharacter(characterID);
            return CalculateRemainingExperience(character);
        }

        /// <summary>
        /// Calculate the amount of experience needed for an character to reach the next level
        /// </summary>
        /// <param name="character">The character</param>
        /// <returns>Returns the amount of experience needed to reach the next level</returns>
        public static long CalculateRemainingExperience(Character character)
        {
            return CalculateExperience(character.ExperienceLevelFormula, character.Level + 1) - character.Experience;
        }

        /// <summary>
        /// Calculate the amount of experience needed to reach the next level given an experience-level conversion formula and the current experience amount
        /// </summary>
        /// <param name="experienceLevelFormula">The experience-level conversion formula</param>
        /// <param name="currentExperience">The current experience amount</param>
        /// <returns>Returns the amount of experience needed to reach the next level</returns>
        public static long CalculateremainingExperience(ExperienceLevelFormula experienceLevelFormula,
            long currentExperience)
        {
            var currentLevel = CalculateLevel(experienceLevelFormula, currentExperience);
            return CalculateExperience(experienceLevelFormula, currentLevel + 1) - currentExperience;
        }

        //    CALCULATE EXPERIENCE PROGRESS

        /// <summary>
        /// Calculate an character's experience progress to reach the next level
        /// </summary>
        /// <param name="characterID">The character's id</param>
        /// <returns>Returns the progress expressed as percentage from 0 to 100</returns>
        public static long CalculateProgress(string characterID)
        {
            var character = CharacterDB.FindCharacter(characterID);
            return CalculateProgress(character);
        }

        /// <summary>
        /// Calculate an character's experience progress to reach the next level
        /// </summary>
        /// <param name="character">The character</param>
        /// <returns>Returns the progress expressed as percentage from 0 to 100</returns>
        public static long CalculateProgress(Character character)
        {
            return (character.Experience - CalculateExperience(character.ExperienceLevelFormula, character.Level)) * 100 /
                   (CalculateExperience(character, character.Level + 1) -
                    CalculateExperience(character.ExperienceLevelFormula, character.Level));
        }

        //    CALCULATE EXPERIENCE DELTA

        /// <summary>
        /// Calculate the experience delta between an character's current experience amount and an arbitrary level
        /// </summary>
        /// <param name="characterID">The character's id</param>
        /// <param name="level">The level value to calculate to</param>
        /// <returns>The experience amount delta</returns>
        public static long CalculateExperienceDelta(string characterID, long level)
        {
            var character = CharacterDB.FindCharacter(characterID);
            return CalculateExperienceDelta(character, level);
        }

        /// <summary>
        /// Calculate the experience delta between an character's current experience amount and an arbitrary level
        /// </summary>
        /// <param name="character">The character</param>
        /// <param name="level">The level value to calculate to</param>
        /// <returns>The experience amount delta</returns>
        public static long CalculateExperienceDelta(Character character, long level)
        {
            return CalculateExperience(character.ExperienceLevelFormula, level) - character.Experience;
        }

        /// <summary>
        /// Calculate the experience delta between an experience amount and a level value
        /// </summary>
        /// <param name="experienceLevelFormula">The experience-level conversion formula</param>
        /// <param name="currentExperience">The starting experience amount</param>
        /// <param name="level">The level value to calculate to</param>
        /// <returns>The experience amount delta</returns>
        public static long CalculateExperienceDelta(ExperienceLevelFormula experienceLevelFormula,
            long currentExperience, long level)
        {
            return CalculateExperience(experienceLevelFormula, level) - currentExperience;
        }

        #endregion

        #region Level Calculations

        //    CALCULATE LEVEL

        /// <summary>
        /// Calculate the level an character would have at a given experience amount
        /// </summary>
        /// <param name="characterID">The character's id</param>
        /// <param name="experience">The experience amount to calculate</param>
        /// <returns>Returns the calculated level value at a given experience amount</returns>
        public static long CalculateLevel(string characterID, long experience)
        {
            var character = CharacterDB.FindCharacter(characterID);
            return CalculateLevel(character, experience);
        }

        /// <summary>
        /// Calculate the level an character would have at a given experience amount
        /// </summary>
        /// <param name="character">The character</param>
        /// <param name="experience">The experience amount</param>
        /// <returns>Returns the calculated level value at a given experience amount</returns>
        public static long CalculateLevel(Character character, long experience)
        {
            return CalculateLevel(character.ExperienceLevelFormula, experience);
        }

        /// <summary>
        /// Calculate the level an character would have at a given experience amount
        /// </summary>
        /// <param name="experienceLevelFormula">The experience-level conversion formula</param>
        /// <param name="experience">The experience amount</param>
        /// <returns>Returns the calculated level value at a given experience amount</returns>
        public static long CalculateLevel(ExperienceLevelFormula experienceLevelFormula, long experience)
        {
            return experienceLevelFormula.CalculateLevel(experience);
        }

        #endregion

        #region Experience Change

        /// <summary>
        /// Add an experience amount to an character
        /// </summary>
        /// <param name="characterID">The character's id</param>
        /// <param name="experienceToAdd">The amount of experience to add</param>
        public static void AddExperience(string characterID, long experienceToAdd)
        {
            var character = CharacterDB.FindCharacter(characterID);
            AddExperience(character, experienceToAdd);
        }

        /// <summary>
        /// Add an experience amount to an character
        /// </summary>
        /// <param name="character">The character</param>
        /// <param name="experienceToAdd">The amount of experience to add</param>
        public static void AddExperience(Character character, long experienceToAdd)
        {
            ChangeExperience(character, experienceToAdd);
        }

        /// <summary>
        /// Subtract an experience amount from an character
        /// </summary>
        /// <param name="characterID">The character's id</param>
        /// <param name="experienceToAdd">The amount of experience to add</param>
        public static void SubtractExperience(string characterID, long experienceToRemove)
        {
            var character = CharacterDB.FindCharacter(characterID);
            SubtractExperience(character, experienceToRemove);
        }

        /// <summary>
        /// Subtract an experience amount from an character
        /// </summary>
        /// <param name="characterID">The character's id</param>
        /// <param name="experienceToAdd">The amount of experience to add</param>
        public static void SubtractExperience(Character character, long experienceToRemove)
        {
            ChangeExperience(character, -experienceToRemove);
        }

        /// <summary>
        /// Change an character's experience amount
        /// </summary>
        /// <param name="characterID">The character's id</param>
        /// <param name="experienceChange">The change of experience/param>
        public static void ChangeExperience(string characterID, long experienceChange)
        {
            var character = CharacterDB.FindCharacter(characterID);
            ChangeExperience(character, experienceChange);
        }

        /// <summary>
        /// Change an character's experience amount
        /// </summary>
        /// <param name="character">The character</param>
        /// <param name="experienceChange">The change of experience</param>
        public static void ChangeExperience(Character character, long experienceChange)
        {
            character.Experience += experienceChange;
        }

        #endregion

        #region Expreience Reset

        /// <summary>
        /// Reset an character's experience to the last level-up
        /// </summary>
        /// <param name="characterID">The character's id</param>
        public static void ResetExperience(string characterID)
        {
            var character = CharacterDB.FindCharacter(characterID);
            ResetExperience(character, character.Level);
        }

        /// <summary>
        /// Reset an character's experience to an arbitrary level
        /// </summary>
        /// <param name="characterID">The character's id</param>
        /// <param name="level">The level value to reset to</param>
        public static void ResetExperience(string characterID, long level)
        {
            var character = CharacterDB.FindCharacter(characterID);
            ResetExperience(character, level);
        }

        /// <summary>
        /// Reset an character's experience to the last level-up
        /// </summary>
        /// <param name="character">The character</param>
        public static void ResetExperience(Character character)
        {
            ResetExperience(character, character.Level);
        }

        /// <summary>
        /// Reset an character's experience to an arbitrary level
        /// </summary>
        /// <param name="character">The character</param>
        /// <param name="level">The level value to reset to</param>
        public static void ResetExperience(Character character, long level)
        {
            character.Experience = CalculateExperience(character.ExperienceLevelFormula, level);
        }

        #endregion
    }
}