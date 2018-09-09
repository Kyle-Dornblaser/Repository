using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Repository
{
    public class MemoryRepository<T>: IRepository<T> where T: IModel

    {
         
        private List<T> _allModels;

        public MemoryRepository() 
        {
            _allModels = new List<T>();
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

            _allModels.Add(model);
        }

        public void Update(T model)
        {
            var tempList = new List<T>();
            
            foreach(T modelInList in _allModels.ToList())
            {
                if(modelInList.Id == model.Id)
                {
                    tempList.Add(model);
                }
                else
                {
                    tempList.Add(modelInList);
                }
            }

            _allModels = tempList;

        }

        public void Delete(T model)
        {
            _allModels.Remove(model);
        }

        public List<T> List(Func<T, bool> filter = null)
        {
            if (filter == null)
            {
                return _allModels;
            }
            else
            {
                return _allModels.Where(filter).ToList();
            }   
        }

        public T Single(Func<T, bool> filter)
        {
            return List(filter).FirstOrDefault();
        }
    }
}
