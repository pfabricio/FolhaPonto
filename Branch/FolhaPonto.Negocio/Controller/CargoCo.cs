using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FolhaPonto.Negocio.Controller.Interface;
using FolhaPonto.Negocio.Model;

namespace FolhaPonto.Negocio.Controller
{
    /// <summary>
    /// Classe Cargos para realizar o Crud completo.
    /// </summary>
    public class CargoCo: Controler, IControllerBase<CargoMo>
    {
        public List<CargoMo> Listar()
        {
            return Db.Cargos.Include(x => x.Funcionarios).Include(x => x.Jornadas).ToList();
        }
        public List<CargoMo> Listar(Func<CargoMo, bool> condicao)
        {
            return Db.Cargos.Include(x => x.Funcionarios).Include(x => x.Jornadas).Where(condicao).ToList();
        }
        public CargoMo Find(Func<CargoMo, bool> condicao)
        {
            return Listar(condicao).SingleOrDefault();
        }
        public void Add(CargoMo entity)
        {
            Db.Cargos.Add(entity);
        }
        public void Update(CargoMo entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }
        public void AddOrUpdate(CargoMo entity)
        {
            Db.Cargos.AddOrUpdate(entity);
        }
        public void Excluir(int id)
        {
            Db.Cargos.Remove(Find(x => x.Id == id));
        }
    }
}
