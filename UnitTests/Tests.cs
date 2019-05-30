using System;
using System.IO;
using Entity;
using ExperienceSystem;
using NUnit.Framework;

namespace UnityTests
{
    [TestFixture]
    public class Tests
    {
        public void Setup()
        {
            EntityDB.ClearDatabase();
        }

        [Test]
        public void TestAddEntitiesToDatabase()
        {
            Setup();
            var experienceLevelFormula = new LinearExperienceLevelFormula();

            EntityDB.AddEntity(new BaseEntity("Matthew", 1, experienceLevelFormula));
            EntityDB.AddEntity(new BaseEntity("Ezikiel", 1, experienceLevelFormula));
            EntityDB.AddEntity(new BaseEntity("Gabriel", 1, experienceLevelFormula));

            Assert.AreEqual(EntityDB.GetAllEntities().Count, 3);
        }

        [Test]
        public void TestSaveDatabase()
        {
            Setup();
            var experienceLevelFormula = new LinearExperienceLevelFormula();

            EntityDB.AddEntity(new BaseEntity("Matthew", 1, experienceLevelFormula));
            EntityDB.AddEntity(new BaseEntity("Ezikiel", 1, experienceLevelFormula));
            EntityDB.AddEntity(new BaseEntity("Gabriel", 1, experienceLevelFormula));

            Assert.DoesNotThrow(EntityDB.SaveToFile);
        }

        [Test]
        public void TestLoadDataBase()
        {
            Setup();
            var experienceLevelFormula = new LinearExperienceLevelFormula();

            EntityDB.AddEntity(new BaseEntity("Matthew", 1, experienceLevelFormula));
            EntityDB.AddEntity(new BaseEntity("Ezikiel", 1, experienceLevelFormula));
            EntityDB.AddEntity(new BaseEntity("Gabriel", 1, experienceLevelFormula));

            EntityDB.SaveToFile();

            EntityDB.ClearDatabase();

            EntityDB.LoadFromFile();

            Assert.AreEqual(EntityDB.GetAllEntities().Count, 3);
        }

        [Test]
        public void TestSquareExperienceLevelFormula()
        {
            var experienceLevelFormula = new SquareExperienceLevelFormula(.1);

            Assert.AreEqual(experienceLevelFormula.CalculateExperience(2), 400);
            Assert.AreEqual(experienceLevelFormula.CalculateLevel(400), 2);
        }

        [Test]
        public void TestLinearExperienceLevelFormula()
        {
            var experienceLevelFormula = new LinearExperienceLevelFormula();

            Assert.AreEqual(experienceLevelFormula.CalculateExperience(1), 100);
            Assert.AreEqual(experienceLevelFormula.CalculateLevel(100), 1);
        }

        [Test]
        public void TestExperienceGetSetLevel()
        {
            var experienceLevelFormula = new LinearExperienceLevelFormula();
            var entity = new BaseEntity("Matthew", 1, experienceLevelFormula);
            var currentLevel = entity.Level;

            EntityDB.AddEntity(entity);

            ExperienceAPI.SetCurrentLevel(entity.Id, 10);
            Assert.AreNotEqual(ExperienceAPI.GetCurrentLevel(entity.Id), currentLevel);
        }

        [Test]
        public void TestCalculateExperience()
        {
            var experienceLevelFormula = new LinearExperienceLevelFormula();
            var entity = new BaseEntity("Gabriel", 1, experienceLevelFormula);

            EntityDB.AddEntity(entity);

            Assert.AreEqual(ExperienceAPI.CalculateExperience(entity.Id, entity.Level),
                entity.Experience);
        }

        [Test]
        public void TestCalculateExperienceDelta()
        {
            var experienceLevelFormula = new LinearExperienceLevelFormula();
            var entity = new BaseEntity("Matthew", 1, experienceLevelFormula);

            EntityDB.AddEntity(entity);

            var experienceDelta = experienceLevelFormula.CalculateExperience(10) - entity.Experience;

            Assert.AreEqual(ExperienceAPI.CalculateExperienceDelta(entity.Id, 10), experienceDelta);
        }

        [Test]
        public void TestCalculateRemainingExperience()
        {
            var experienceLevelFormula = new LinearExperienceLevelFormula();
            var entity = new BaseEntity("Matthew", 1, experienceLevelFormula);

            EntityDB.AddEntity(entity);

            var remainingExperience = experienceLevelFormula.CalculateExperience(entity.Level + 1) - entity.Experience;

            Assert.AreEqual(ExperienceAPI.CalculateRemainingExperience(entity.Id), remainingExperience);
        }

        [Test]
        public void TestCalculateProgress()
        {
            var experienceLevelFormula = new SquareExperienceLevelFormula(0.1);
            var entity = new BaseEntity("Matthew", 1, experienceLevelFormula);

            EntityDB.AddEntity(entity);

            var nextLevelExperience = experienceLevelFormula.CalculateExperience(entity.Level + 1);
            var lastLevelExperience = experienceLevelFormula.CalculateExperience(entity.Level);

            ExperienceAPI.AddExperience(entity.Id, 33);

            Assert.AreEqual(ExperienceAPI.CalculateProgress(entity.Id),
                (entity.Experience - lastLevelExperience) * 100 / (nextLevelExperience - lastLevelExperience));
        }

        [Test]
        public void TestAddSubtractExperience()
        {
            var experienceLevelFormula = new SquareExperienceLevelFormula(0.1);
            var entity = new BaseEntity("Matthew", 1, experienceLevelFormula);

            var currentExperience = entity.Experience;

            EntityDB.AddEntity(entity);

            ExperienceAPI.AddExperience(entity.Id, 10);
            Assert.AreEqual(currentExperience + 10, entity.Experience);
            
            ExperienceAPI.SubtractExperience(entity.Id, 20);
            Assert.AreEqual(currentExperience - 10, entity.Experience);
        }
    }
}