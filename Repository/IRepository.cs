using System;
using System.Collections.Generic;

public interface IRepository<T> where T: IModel {
    void Create(T model);
    void Update(T model);
    void Delete(T model);
    List<T> List(Func<T, bool> filter = null);
    T Single(Func<T, bool> filter);
}