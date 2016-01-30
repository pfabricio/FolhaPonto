using System;
using System.Collections.Generic;

namespace FolhaPonto.Negocio.Controller.Interface
{
    /// <summary>
    /// Interface para auxiliar a criação das classes concretas.
    /// </summary>
    /// <typeparam name="T">Classe generica que pode ser qualquer uma classe do Model</typeparam>
    public interface IControllerBase<T> where T : class
    {
        List<T> Listar();
        List<T> Listar(Func<T, bool> condicao);
        T Find(Func<T, bool> condicao);
        void Add(T entity);
        void Update(T entity);
        void AddOrUpdate(T entity);
        void Excluir(int id);
        void Commit();
    }
}
