using System.Collections.Generic;

public interface IRepository {
    void Create<T>(T model) where T: IModel;
    void Update<T>(T model) where T: IModel;
    void Delete<T>(T model) where T: IModel;
    List<T> List<T>() where T: IModel;
    T Single<T>(int id) where T: IModel;
}