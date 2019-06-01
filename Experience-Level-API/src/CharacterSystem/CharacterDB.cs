using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CharacterSystem
{
    public static class CharacterDB
    {
        private const string _dataBaseName = "characterDB.json";

        private static Dictionary<string, Character> _characterDictionary = new Dictionary<string, Character>();

        /// <summary>
        ///     Find a character in the character database
        /// </summary>
        /// <param name="characterID">The id of the character to find</param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown when a characterID not present in the database is passed</exception>
        public static Character FindCharacter(string characterID)
        {
            if (_characterDictionary.ContainsKey(characterID)) return _characterDictionary[characterID];

            throw new Exception($"The element with id {characterID} cannot be found");
        }

        /// <summary>
        ///     Get all the entities in the dictionary
        /// </summary>
        /// <returns>returns a list with all the dictionary values</returns>
        public static List<Character> GetAllEntities()
        {
            return _characterDictionary.Values.ToList();
        }

        /// <summary>
        ///     Delete a character from the character database
        /// </summary>
        /// <param name="characterID">The id of the character to delete from the database</param>
        /// <exception cref="Exception">Thrown when a characterID not present in the database is passed</exception>
        public static void DeleteCharacter(string characterID)
        {
            if (_characterDictionary.ContainsKey(characterID))
                _characterDictionary.Remove(characterID);
            else
                throw new Exception($"The element with id {characterID} cannot be found");
        }

        /// <summary>
        ///     Clear all the entries in the character database
        /// </summary>
        public static void ClearDatabase()
        {
            _characterDictionary.Clear();
        }

        /// <summary>
        ///     Add a character to the database
        /// </summary>
        /// <param name="character">The character to be added to the database</param>
        /// <exception cref="Exception">Thrown when a characterID already present in the database is passed</exception>
        public static void AddCharacter(Character character)
        {
            if (!_characterDictionary.ContainsKey(character.ID))
                _characterDictionary.Add(character.ID, character);
            else
                throw new Exception($"The entry with id {character.ID} already exists");
        }

        /// <summary>
        ///     Save characterDB to file (for testing purposes)
        /// </summary>
        public static void SaveToFile()
        {
            var dataAsJson = JsonConvert.SerializeObject(_characterDictionary);
            File.WriteAllText(_dataBaseName, dataAsJson);
        }

        /// <summary>
        ///     Load characterDB from file (for testing purposes)
        /// </summary>
        public static void LoadFromFile()
        {
            var dataAsJson = File.ReadAllText(_dataBaseName);

            _characterDictionary.Clear();
            _characterDictionary = JsonConvert.DeserializeObject<Dictionary<string, Character>>(dataAsJson);
        }
    }
}