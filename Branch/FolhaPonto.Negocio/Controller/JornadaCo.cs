using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FolhaPonto.Negocio.Controller.Interface;
using FolhaPonto.Negocio.Model;

namespace FolhaPonto.Negocio.Controller
{
    public class JornadaCo: Controler, IControllerBase<JornadaMo>
    {
        public List<JornadaMo> Listar()
        {
            return Db.Jornadas.Include(x => x.Cargo).Include(x => x.Funcionarios).ToList();
        }
        public List<JornadaMo> Listar(Func<JornadaMo, bool> condicao)
        {
            return Db.Jornadas.Include(x => x.Cargo).Include(x => x.Funcionarios).Where(condicao).ToList();
        }
        public JornadaMo Find(Func<JornadaMo, bool> condicao)
        {
            return Listar(condicao).SingleOrDefault();
        }
        public void Add(JornadaMo entity)
        {
            Db.Jornadas.Add(entity);
        }
        public void Update(JornadaMo entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }
        public void AddOrUpdate(JornadaMo entity)
        {
            Db.Jornadas.AddOrUpdate(entity);
        }
        public void Excluir(int id)
        {
            Db.Jornadas.Remove(Find(x => x.Id == id));
        }
    }
}
