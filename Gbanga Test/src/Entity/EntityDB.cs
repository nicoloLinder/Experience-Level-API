using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace Entity
{
    public class EntityDB
    {
        
        private const string _dataBaseName = "entityDB.json";
        
        private static Dictionary<string, BaseEntity> _entityDictionary = new Dictionary<string, BaseEntity>();

        /// <summary>
        /// Find an entity in the entity database
        /// </summary>
        /// <param name="entityID">The id of the entity to find</param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown when an entityID not present in the database is passed</exception>
        public static BaseEntity FindEntity(string entityID)
        {
            if (_entityDictionary.ContainsKey(entityID))
            {
                return _entityDictionary[entityID];
            }

            throw new Exception($"The element with id {entityID} cannot be found");
        }

        /// <summary>
        /// Get all the entities in the dictionary
        /// </summary>
        /// <returns>returns a list with all the dictionary values</returns>
        public static List<BaseEntity> GetAllEntities()
        {
            return _entityDictionary.Values.ToList();
        }
        
        /// <summary>
        /// Delete an entity from the entity database
        /// </summary>
        /// <param name="entityID">The id of the entity to delete from the database</param>
        /// <exception cref="Exception">Thrown when an entityID not present in the database is passed</exception>
        public static void DeleteEntity(string entityID)
        {
            if (_entityDictionary.ContainsKey(entityID))
            {
                _entityDictionary.Remove(entityID);
            }
            else
            {
                throw new Exception($"The element with id {entityID} cannot be found");
            }
        }

        /// <summary>
        /// Clear all the entries in the entity database
        /// </summary>
        public static void ClearDatabase()
        {
            _entityDictionary.Clear();
        }

        /// <summary>
        /// Add an entity to the database
        /// </summary>
        /// <param name="baseEntity">The entity to be added to the database</param>
        /// <exception cref="Exception">Thrown when an entityID already present in the database is passed</exception>
        public static void AddEntity(BaseEntity baseEntity)
        {
            if (!_entityDictionary.ContainsKey(baseEntity.Id))
            {
                _entityDictionary.Add(baseEntity.Id, baseEntity);
            }
            else
            {
                throw new Exception($"The entry with id {baseEntity.Id} already exists");
            }
        }
        
        /// <summary>
        /// Save entityDB to file (for testing purposes)
        /// </summary>
        public static void SaveToFile()
        {
            var dataAsJson = JsonConvert.SerializeObject(_entityDictionary);
            File.WriteAllText(_dataBaseName, dataAsJson);
        }

        /// <summary>
        /// Load entityDB from file (for testing purposes)
        /// </summary>
        public static void LoadFromFile()
        {
            var dataAsJson = File.ReadAllText(_dataBaseName);
            
            _entityDictionary.Clear();
            _entityDictionary = JsonConvert.DeserializeObject<Dictionary<string, BaseEntity>>(dataAsJson);
        }
    }
}