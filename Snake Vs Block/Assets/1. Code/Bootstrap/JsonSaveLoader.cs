using System;
using System.IO;
using Snake.Domain;
using UnityEngine;

namespace Snake.Boot
{
    public class JsonSaveLoader<T> : ISaveLoader<T> where T : new()
    {
        private readonly string _filePath = Application.persistentDataPath + "Save.json";

        public void Save(T data)
        {
            string json = JsonUtility.ToJson(data);
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.WriteLine(json);
            }
        }

        public T Load()
        {
            if (File.Exists(_filePath) == false)
                return new T();

            string json = String.Empty;

            using (StreamReader streamReader = new StreamReader(_filePath))
                json = streamReader.ReadToEnd();

            if (string.IsNullOrEmpty(json))
                return new T();

            return JsonUtility.FromJson<T>(json);
        }
    }
}