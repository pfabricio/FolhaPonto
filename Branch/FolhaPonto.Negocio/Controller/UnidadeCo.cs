using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FolhaPonto.Negocio.Controller.Interface;
using FolhaPonto.Negocio.Model;

namespace FolhaPonto.Negocio.Controller
{
    public class UnidadeCo : Controler, IControllerBase<UnidadeMo>
    {
        public List<UnidadeMo> Listar()
        {
            return Db.Unidades.Include(x=>x.Funcionarios).ToList();
        }
        public List<UnidadeMo> Listar(Func<UnidadeMo, bool> condicao)
        {
            return Db.Unidades.Include(x => x.Funcionarios).Where(condicao).ToList();
        }
        public UnidadeMo Find(Func<UnidadeMo, bool> condicao)
        {
            return Listar(condicao).SingleOrDefault();
        }
        public void Add(UnidadeMo entity)
        {
            Db.Unidades.Add(entity);
        }
        public void Update(UnidadeMo entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }
        public void AddOrUpdate(UnidadeMo entity)
        {
            Db.Unidades.AddOrUpdate(entity);
        }
        public void Excluir(int id)
        {
            Db.Unidades.Remove(Find(x => x.Id == id));
        }
    }
}
