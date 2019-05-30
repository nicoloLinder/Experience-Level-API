﻿using System;
using System.IO;
using CharacterSystem;
using ExperienceSystem;
using NUnit.Framework;

namespace UnityTests
{
    [TestFixture]
    public class Tests
    {
        public void Setup()
        {
            CharacterDB.ClearDatabase();
        }

        [Test]
        public void TestAddEntitiesToDatabase()
        {
            Setup();
            var experienceLevelFormula = new LinearExperienceLevelFormula();

            CharacterDB.AddCharacter(new Character("Matthew", 1, experienceLevelFormula));
            CharacterDB.AddCharacter(new Character("Ezikiel", 1, experienceLevelFormula));
            CharacterDB.AddCharacter(new Character("Gabriel", 1, experienceLevelFormula));

            Assert.AreEqual(CharacterDB.GetAllEntities().Count, 3);
        }

        [Test]
        public void TestSaveDatabase()
        {
            Setup();
            var experienceLevelFormula = new LinearExperienceLevelFormula();

            CharacterDB.AddCharacter(new Character("Matthew", 1, experienceLevelFormula));
            CharacterDB.AddCharacter(new Character("Ezikiel", 1, experienceLevelFormula));
            CharacterDB.AddCharacter(new Character("Gabriel", 1, experienceLevelFormula));

            Assert.DoesNotThrow(CharacterDB.SaveToFile);
        }

        [Test]
        public void TestLoadDataBase()
        {
            Setup();
            var experienceLevelFormula = new LinearExperienceLevelFormula();

            CharacterDB.AddCharacter(new Character("Matthew", 1, experienceLevelFormula));
            CharacterDB.AddCharacter(new Character("Ezikiel", 1, experienceLevelFormula));
            CharacterDB.AddCharacter(new Character("Gabriel", 1, experienceLevelFormula));

            CharacterDB.SaveToFile();

            CharacterDB.ClearDatabase();

            CharacterDB.LoadFromFile();

            Assert.AreEqual(CharacterDB.GetAllEntities().Count, 3);
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
            var character = new Character("Matthew", 1, experienceLevelFormula);
            var currentLevel = character.Level;

            CharacterDB.AddCharacter(character);

            ExperienceAPI.SetCurrentLevel(character.Id, 10);
            Assert.AreNotEqual(ExperienceAPI.GetCurrentLevel(character.Id), currentLevel);
        }

        [Test]
        public void TestCalculateExperience()
        {
            var experienceLevelFormula = new LinearExperienceLevelFormula();
            var character = new Character("Gabriel", 1, experienceLevelFormula);

            CharacterDB.AddCharacter(character);

            Assert.AreEqual(ExperienceAPI.CalculateExperience(character.Id, character.Level),
                character.Experience);
        }

        [Test]
        public void TestCalculateExperienceDelta()
        {
            var experienceLevelFormula = new LinearExperienceLevelFormula();
            var character = new Character("Matthew", 1, experienceLevelFormula);

            CharacterDB.AddCharacter(character);

            var experienceDelta = experienceLevelFormula.CalculateExperience(10) - character.Experience;

            Assert.AreEqual(ExperienceAPI.CalculateExperienceDelta(character.Id, 10), experienceDelta);
        }

        [Test]
        public void TestCalculateRemainingExperience()
        {
            var experienceLevelFormula = new LinearExperienceLevelFormula();
            var character = new Character("Matthew", 1, experienceLevelFormula);

            CharacterDB.AddCharacter(character);

            var remainingExperience = experienceLevelFormula.CalculateExperience(character.Level + 1) - character.Experience;

            Assert.AreEqual(ExperienceAPI.CalculateRemainingExperience(character.Id), remainingExperience);
        }

        [Test]
        public void TestCalculateProgress()
        {
            var experienceLevelFormula = new SquareExperienceLevelFormula(0.1);
            var character = new Character("Matthew", 1, experienceLevelFormula);

            CharacterDB.AddCharacter(character);

            var nextLevelExperience = experienceLevelFormula.CalculateExperience(character.Level + 1);
            var lastLevelExperience = experienceLevelFormula.CalculateExperience(character.Level);

            ExperienceAPI.AddExperience(character.Id, 33);

            Assert.AreEqual(ExperienceAPI.CalculateProgress(character.Id),
                (character.Experience - lastLevelExperience) * 100 / (nextLevelExperience - lastLevelExperience));
        }

        [Test]
        public void TestAddSubtractExperience()
        {
            var experienceLevelFormula = new SquareExperienceLevelFormula(0.1);
            var character = new Character("Matthew", 1, experienceLevelFormula);

            var currentExperience = character.Experience;

            CharacterDB.AddCharacter(character);

            ExperienceAPI.AddExperience(character.Id, 10);
            Assert.AreEqual(currentExperience + 10, character.Experience);
            
            ExperienceAPI.SubtractExperience(character.Id, 20);
            Assert.AreEqual(currentExperience - 10, character.Experience);
        }
    }
}