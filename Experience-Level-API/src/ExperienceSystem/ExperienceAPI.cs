using Entity;

namespace ExperienceSystem
{
    public static class ExperienceAPI
    {
        #region Get / Set Level

        /// <summary>
        /// Get the current level of an entity
        /// </summary>
        /// <param name="entityID">The id of the entity</param>
        /// <returns>Returns the current level of the entity</returns>
        public static long GetCurrentLevel(string entityID)
        {
            return EntityDB.FindEntity(entityID).Level;
        }

        /// <summary>
        /// Set the current level of an entity
        /// </summary>
        /// <param name="entityID">The id of the entity</param>
        /// <param name="level">The level value to set</param>
        public static void SetCurrentLevel(string entityID, long level)
        {
            EntityDB.FindEntity(entityID).Level = level;
        }

        #endregion

        #region Experience Calculations

        /// <summary>
        /// Calculate the experience amount at a given level
        /// </summary>
        /// <param name="entityID">The ID of the entity</param>
        /// <param name="level">The level value to calculate the experience from</param>
        /// <returns>Returns the calculated experience amount at a given level value</returns>
        public static long CalculateExperience(string entityID, long level)
        {
            var entity = EntityDB.FindEntity(entityID);
            return CalculateExperience(entity, level);
        }

        /// <summary>
        /// Calculate the experience amount at a given level
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="level">The level value to calculate the experience from</param>
        /// <returns>The calculated experience amount at a given level value</returns>
        public static long CalculateExperience(BaseEntity entity, long level)
        {
            return CalculateExperience(entity.ExperienceLevelFormula, level);
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
        /// Calculate the amount of experience needed for an entity to reach the next level
        /// </summary>
        /// <param name="entityID">The entity's id/param>
        /// <returns>Returns the amount of experience needed to reach the next level</returns>
        public static long CalculateRemainingExperience(string entityID)
        {
            var entity = EntityDB.FindEntity(entityID);
            return CalculateRemainingExperience(entity);
        }

        /// <summary>
        /// Calculate the amount of experience needed for an entity to reach the next level
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Returns the amount of experience needed to reach the next level</returns>
        public static long CalculateRemainingExperience(BaseEntity entity)
        {
            return CalculateExperience(entity.ExperienceLevelFormula, entity.Level + 1) - entity.Experience;
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
        /// Calculate an entity's experience progress to reach the next level
        /// </summary>
        /// <param name="entityID">The entity's id</param>
        /// <returns>Returns the progress expressed as percentage from 0 to 100</returns>
        public static long CalculateProgress(string entityID)
        {
            var entity = EntityDB.FindEntity(entityID);
            return CalculateProgress(entity);
        }

        /// <summary>
        /// Calculate an entity's experience progress to reach the next level
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Returns the progress expressed as percentage from 0 to 100</returns>
        public static long CalculateProgress(BaseEntity entity)
        {
            return (entity.Experience - CalculateExperience(entity.ExperienceLevelFormula, entity.Level)) * 100 /
                   (CalculateExperience(entity, entity.Level + 1) -
                    CalculateExperience(entity.ExperienceLevelFormula, entity.Level));
        }

        //    CALCULATE EXPERIENCE DELTA

        /// <summary>
        /// Calculate the experience delta between an entity's current experience amount and an arbitrary level
        /// </summary>
        /// <param name="entityID">The entity's id</param>
        /// <param name="level">The level value to calculate to</param>
        /// <returns>The experience amount delta</returns>
        public static long CalculateExperienceDelta(string entityID, long level)
        {
            var entity = EntityDB.FindEntity(entityID);
            return CalculateExperienceDelta(entity, level);
        }

        /// <summary>
        /// Calculate the experience delta between an entity's current experience amount and an arbitrary level
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="level">The level value to calculate to</param>
        /// <returns>The experience amount delta</returns>
        public static long CalculateExperienceDelta(BaseEntity entity, long level)
        {
            return CalculateExperience(entity.ExperienceLevelFormula, level) - entity.Experience;
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
        /// Calculate the level an entity would have at a given experience amount
        /// </summary>
        /// <param name="entityID">The entity's id</param>
        /// <param name="experience">The experience amount to calculate</param>
        /// <returns>Returns the calculated level value at a given experience amount</returns>
        public static long CalculateLevel(string entityID, long experience)
        {
            var entity = EntityDB.FindEntity(entityID);
            return CalculateLevel(entity, experience);
        }

        /// <summary>
        /// Calculate the level an entity would have at a given experience amount
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="experience">The experience amount</param>
        /// <returns>Returns the calculated level value at a given experience amount</returns>
        public static long CalculateLevel(BaseEntity entity, long experience)
        {
            return CalculateLevel(entity.ExperienceLevelFormula, experience);
        }

        /// <summary>
        /// Calculate the level an entity would have at a given experience amount
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
        /// Add an experience amount to an entity
        /// </summary>
        /// <param name="entityID">The entity's id</param>
        /// <param name="experienceToAdd">The amount of experience to add</param>
        public static void AddExperience(string entityID, long experienceToAdd)
        {
            var entity = EntityDB.FindEntity(entityID);
            AddExperience(entity, experienceToAdd);
        }

        /// <summary>
        /// Add an experience amount to an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="experienceToAdd">The amount of experience to add</param>
        public static void AddExperience(BaseEntity entity, long experienceToAdd)
        {
            ChangeExperience(entity, experienceToAdd);
        }

        /// <summary>
        /// Subtract an experience amount from an entity
        /// </summary>
        /// <param name="entityID">The entity's id</param>
        /// <param name="experienceToAdd">The amount of experience to add</param>
        public static void SubtractExperience(string entityID, long experienceToRemove)
        {
            var entity = EntityDB.FindEntity(entityID);
            SubtractExperience(entity, experienceToRemove);
        }

        /// <summary>
        /// Subtract an experience amount from an entity
        /// </summary>
        /// <param name="entityID">The entity's id</param>
        /// <param name="experienceToAdd">The amount of experience to add</param>
        public static void SubtractExperience(BaseEntity entity, long experienceToRemove)
        {
            ChangeExperience(entity, -experienceToRemove);
        }

        /// <summary>
        /// Change an entity's experience amount
        /// </summary>
        /// <param name="entityID">The entity's id</param>
        /// <param name="experienceChange">The change of experience/param>
        public static void ChangeExperience(string entityID, long experienceChange)
        {
            var entity = EntityDB.FindEntity(entityID);
            ChangeExperience(entity, experienceChange);
        }

        /// <summary>
        /// Change an entity's experience amount
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="experienceChange">The change of experience</param>
        public static void ChangeExperience(BaseEntity entity, long experienceChange)
        {
            entity.Experience += experienceChange;
        }

        #endregion

        #region Expreience Reset

        /// <summary>
        /// Reset an entity's experience to the last level-up
        /// </summary>
        /// <param name="entityID">The entity's id</param>
        public static void ResetExperience(string entityID)
        {
            var entity = EntityDB.FindEntity(entityID);
            ResetExperience(entity, entity.Level);
        }

        /// <summary>
        /// Reset an entity's experience to an arbitrary level
        /// </summary>
        /// <param name="entityID">The entity's id</param>
        /// <param name="level">The level value to reset to</param>
        public static void ResetExperience(string entityID, long level)
        {
            var entity = EntityDB.FindEntity(entityID);
            ResetExperience(entity, level);
        }

        /// <summary>
        /// Reset an entity's experience to the last level-up
        /// </summary>
        /// <param name="entity">The entity</param>
        public static void ResetExperience(BaseEntity entity)
        {
            ResetExperience(entity, entity.Level);
        }

        /// <summary>
        /// Reset an entity's experience to an arbitrary level
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="level">The level value to reset to</param>
        public static void ResetExperience(BaseEntity entity, long level)
        {
            entity.Experience = CalculateExperience(entity.ExperienceLevelFormula, level);
        }

        #endregion
    }
}