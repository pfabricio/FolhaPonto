using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FolhaPonto.Negocio.Controller.Interface;
using FolhaPonto.Negocio.Model;

namespace FolhaPonto.Negocio.Controller
{
    public class FrequenciaCo: Controler, IControllerBase<FrequenciaMo>
    {
        public List<FrequenciaMo> Listar()
        {
            return Db.Frequencias.Include(x => x.Funcionario).ToList();
        }
        public List<FrequenciaMo> Listar(Func<FrequenciaMo, bool> condicao)
        {
            return Db.Frequencias.Include(x => x.Funcionario).Where(condicao).ToList();
        }
        public FrequenciaMo Find(Func<FrequenciaMo, bool> condicao)
        {
            return Listar(condicao).SingleOrDefault();
        }
        public void Add(FrequenciaMo entity)
        {
            Db.Frequencias.Add(entity);
        }
        public void Update(FrequenciaMo entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }
        public void AddOrUpdate(FrequenciaMo entity)
        {
            Db.Frequencias.AddOrUpdate(entity);
        }
        public void Excluir(int id)
        {
            Db.Frequencias.Remove(Find(x => x.Id == id));
        }
        public void Excluir(Func<FrequenciaMo, bool> condicao)
        {
            Db.Frequencias.RemoveRange(Listar(condicao));
        }
    }
}
