using System;
using System.Collections.Generic;

public interface IRepository {
    void Create<T>(T model) where T: IModel;
    void Update<T>(T model) where T: IModel;
    void Delete<T>(T model) where T: IModel;
    List<T> List<T>(Func<T, bool> filter = null) where T: IModel;
    T Single<T>(Func<T, bool> filter) where T: IModel;
}