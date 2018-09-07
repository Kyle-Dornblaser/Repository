using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace database
{
    public class IORepository: IRepository

    {
        private string FilePath { get; }

        public IORepository(string filePath) {
            FilePath = filePath;
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        private string Serialize<T>(T model) {
            return JsonConvert.SerializeObject(model);
        }

        private T Deserialize<T>(string value) {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public void Create<T>(T model) where T: IModel
        {
            List<T> allModels = List<T>();
            if (allModels.Count == 0) 
            {
                model.Id = 1;
            } else
            {
                var highestId = List<T>().OrderBy(x => x.Id).Last().Id;
                model.Id = highestId + 1;
            }

            var serializedModel = Serialize(model);
            using (StreamWriter writer = File.AppendText(FilePath))
            {
               writer.WriteLine(serializedModel);
            }
        }

        public void Update<T>(T model) where T: IModel
        {
            var deserializedList = List<T>();
            var lines = new List<String>();
            foreach(T modelInList in deserializedList)
            {
                if(modelInList.Id == model.Id)
                {
                    lines.Add(Serialize<T>(model));
                }
                else
                {
                    lines.Add(Serialize<T>(modelInList));
                }
            }

            File.WriteAllLines(FilePath, lines);

        }

        public void Delete<T>(T model) where T: IModel
        {
            var deserializedList = List<T>();
            var lines = new List<String>();
            foreach(T modelInList in deserializedList)
            {
                if(modelInList.Id != model.Id)
                {
                    lines.Add(Serialize<T>(modelInList));
                }
            }

            File.WriteAllLines(FilePath, lines);
        }

        public List<T> List<T>(Func<T, bool> filter = null) where T: IModel
        {
            using (StreamReader reader = new StreamReader(FilePath))
            {
                var list = new List<T>();
                while(!reader.EndOfStream) 
                {
                    var deserializedModel = Deserialize<T>(reader.ReadLine());
                    list.Add(deserializedModel);
                }
                if (filter == null)
                {
                    return list;
                }
                else 
                {
                    return list.Where(filter).ToList();
                }
            }
        }

        public T Single<T>(Func<T, bool> filter) where T: IModel
        {
            return List<T>(filter).FirstOrDefault();
        }
    }
}
