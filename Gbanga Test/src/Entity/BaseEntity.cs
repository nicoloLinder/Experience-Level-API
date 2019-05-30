using System;
using ExperienceAPI;

namespace Entity
{
    [Serializable]
    public class BaseEntity
    {
        #region Variables
        
        private readonly string _id = Guid.NewGuid().ToString();
        private string _name;
        private long _level;
        private long _experience;
        private ExperienceLevelFormula _experienceLevelFormula;
        
        #endregion

        #region Properties

        public string Id => _id;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public long Level
        {
            get => _level;
            set
            {
                _level = value;
                _experience = _experienceLevelFormula.CalculateExperience(value);
            }
        }

        public long Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                _level = _experienceLevelFormula.CalculateLevel(value);
            } 
        }

        public ExperienceLevelFormula ExperienceLevelFormula
        {
            get => _experienceLevelFormula;
            set => _experienceLevelFormula = value;
        }

        #endregion

        #region Constuctors

        public BaseEntity()
        {
            _name = "";
            _experienceLevelFormula = new LinearExperienceLevelFormula();
        }

        public BaseEntity(string name, ExperienceLevelFormula experienceLevelFormula)
        {
            _name = name;
            _experienceLevelFormula = experienceLevelFormula;
        }

        public BaseEntity(string name, long level, ExperienceLevelFormula experienceLevelFormula)
        {
            _name = name;
            _experienceLevelFormula = experienceLevelFormula;
            _level = level;
            _experience = _experienceLevelFormula.CalculateExperience(_level);
        }

        #endregion

        #region Methods

        public string ToString()
        {
            return _id + "\n" + _name + "\n" + _level + "\n" + _experience;
        }

        #endregion
    }
}