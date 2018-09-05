using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace database
{
    public class Repository

    {
        private string FilePath { get; }

        public Repository(string filePath) {
            FilePath = filePath;
            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.Create(filePath).Close();
            }
        }

        private string Serialize<T>(T model) {
            return JsonConvert.SerializeObject(model);
        }

        private T Deserialize<T>(string value) {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public void Add<T>(T model)
        {
            var serializedModel = Serialize(model);
            using (StreamWriter writer = File.AppendText(FilePath))
            {
               writer.WriteLine(serializedModel);
            }
        }

        public List<T> GetAll<T>() {
            using (StreamReader reader = new StreamReader(FilePath))
            {
                var list = new List<T>();
                while(!reader.EndOfStream) {
                    var deserializedModel = Deserialize<T>(reader.ReadLine());
                    list.Add(deserializedModel);
                }
                return list;
            }
        }
    }
}
