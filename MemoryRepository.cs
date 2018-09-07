using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace database
{
    public class MemoryRepository: IRepository

    {
        // Not sure why I need to use a singleton
        private static MemoryRepository _instance;
        public static MemoryRepository Instance 
        {
         get {
            if(_instance == null){
                _instance = new MemoryRepository();
            }
            return _instance;
         }   
        }

        // How can I use here <T>? 
        private List<T> _allModels;


        private MemoryRepository() 
        {
            _allModels = new List<T>();
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

            _allModels.Add(model);
        }

        public void Update<T>(T model) where T: IModel
        {
            var tempList = List<T>();
            
            foreach(T modelInList in _allModels)
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

        public void Delete<T>(T model) where T: IModel
        {
            _allModels.Remove(model);
        }

        public List<T> List<T>(Func<T, bool> filter = null) where T: IModel
        {
            return _allModels.Where(filter).ToList();   
        }

        public T Single<T>(Func<T, bool> filter) where T: IModel
        {
            return List<T>(filter).FirstOrDefault();
        }
    }
}
