using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FolhaPonto.Negocio.Controller.Interface;
using FolhaPonto.Negocio.Model;

namespace FolhaPonto.Negocio.Controller
{
    public class PerfilCo: Controler, IControllerBase<PerfilMo>
    {
        public List<PerfilMo> Listar()
        {
            return Db.Perfis.Include(x=>x.Funcionarios).ToList();
        }
        public List<PerfilMo> Listar(Func<PerfilMo, bool> condicao)
        {
            return Db.Perfis.Include(x => x.Funcionarios).Where(condicao).ToList();
        }
        public PerfilMo Find(Func<PerfilMo, bool> condicao)
        {
            return Listar(condicao).SingleOrDefault();
        }
        public void Add(PerfilMo entity)
        {
            Db.Perfis.Add(entity);
        }
        public void Update(PerfilMo entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }
        public void AddOrUpdate(PerfilMo entity)
        {
            Db.Perfis.AddOrUpdate(entity);
        }
        public void Excluir(int id)
        {
            Db.Perfis.Remove(Find(x => x.Id == id));
        }
    }
}
