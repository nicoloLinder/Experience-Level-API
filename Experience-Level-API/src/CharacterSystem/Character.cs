using System;
using ExperienceSystem;

namespace CharacterSystem
{
    [Serializable]
    public class Character
    {
        #region Methods

        public string ToString()
        {
            return Id + "\n" + _name + "\n" + _level + "\n" + _experience;
        }

        #endregion

        #region Variables

        private string _name;
        private long _level;
        private long _experience;
        private ExperienceLevelFormula _experienceLevelFormula;

        #endregion

        #region Properties

        public string Id { get; } = Guid.NewGuid().ToString();

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

        public Character()
        {
            _name = "";
            _experienceLevelFormula = new LinearExperienceLevelFormula();
        }

        public Character(string name, ExperienceLevelFormula experienceLevelFormula)
        {
            _name = name;
            _experienceLevelFormula = experienceLevelFormula;
        }

        public Character(string name, long level, ExperienceLevelFormula experienceLevelFormula)
        {
            _name = name;
            _experienceLevelFormula = experienceLevelFormula;
            _level = level;
            _experience = _experienceLevelFormula.CalculateExperience(_level);
        }

        #endregion
    }
}