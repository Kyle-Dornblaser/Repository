using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Repository
{
    public class IORepository<T>: IRepository<T> where T : IModel

    {
        private string FilePath { get; }

        public IORepository(string filePath) {
            FilePath = filePath;
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        private string Serialize(T model) {
            return JsonConvert.SerializeObject(model);
        }

        private T Deserialize(string value) {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public void Create(T model)
        {
            List<T> allModels = List();
            if (allModels.Count == 0) 
            {
                model.Id = 1;
            } else
            {
                var highestId = List().OrderBy(x => x.Id).Last().Id;
                model.Id = highestId + 1;
            }

            var serializedModel = Serialize(model);
            using (StreamWriter writer = File.AppendText(FilePath))
            {
               writer.WriteLine(serializedModel);
            }
        }

        public void Update(T model)
        {
            var deserializedList = List();
            var lines = new List<String>();
            foreach(T modelInList in deserializedList)
            {
                if(modelInList.Id == model.Id)
                {
                    lines.Add(Serialize(model));
                }
                else
                {
                    lines.Add(Serialize(modelInList));
                }
            }

            File.WriteAllLines(FilePath, lines);

        }

        public void Delete(T model)
        {
            var deserializedList = List();
            var lines = new List<String>();
            foreach(T modelInList in deserializedList)
            {
                if(modelInList.Id != model.Id)
                {
                    lines.Add(Serialize(modelInList));
                }
            }

            File.WriteAllLines(FilePath, lines);
        }

        public List<T> List(Func<T, bool> filter = null)
        {
            using (StreamReader reader = new StreamReader(FilePath))
            {
                var list = new List<T>();
                while(!reader.EndOfStream) 
                {
                    var deserializedModel = Deserialize(reader.ReadLine());
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

        public T Single(Func<T, bool> filter)
        {
            return List(filter).FirstOrDefault();
        }
    }
}
