using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FolhaPonto.Negocio.Controller.Interface;
using FolhaPonto.Negocio.Model;
using System.Data.Entity.Migrations;

namespace FolhaPonto.Negocio.Controller
{
    public class JustificavaCo : Controler, IControllerBase<JustificativaMo>
    {
        public void Add(JustificativaMo entity)
        {
            Db.Justificativas.Add(entity);
        }
        public void AddOrUpdate(JustificativaMo entity)
        {
            Db.Justificativas.AddOrUpdate(entity);
        }
        public void Excluir(int id)
        {
            Db.Justificativas.Remove(Find(x => x.Id == id));
        }
        public JustificativaMo Find(Func<JustificativaMo, bool> condicao)
        {
            return Listar(condicao).SingleOrDefault();
        }
        public List<JustificativaMo> Listar()
        {
            return Db.Justificativas.Include(x=>x.Funcionario).ToList();
        }
        public List<JustificativaMo> Listar(Func<JustificativaMo, bool> condicao)
        {
            return Db.Justificativas.Include(x => x.Funcionario).Where(condicao).ToList();
        }
        public void Update(JustificativaMo entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }
    }
}
