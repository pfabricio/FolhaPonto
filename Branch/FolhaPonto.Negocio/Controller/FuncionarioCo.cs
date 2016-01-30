using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FolhaPonto.Negocio.Controller.Interface;
using FolhaPonto.Negocio.Model;

namespace FolhaPonto.Negocio.Controller
{
    public class FuncionarioCo: Controler, IControllerBase<FuncionarioMo>
    {
        public List<FuncionarioMo> Listar()
        {
            return Db.Funcionarios.Include(x => x.Cargo)
                .Include(x => x.Unidade)
                .Include(x => x.Jornada)
                .Include(x => x.Perfil)
                .Include(x => x.Frequencias)
                .ToList();
        }
        public List<FuncionarioMo> Listar(Func<FuncionarioMo, bool> condicao)
        {
            return Db.Funcionarios.Include(x => x.Cargo)
                    .Include(x => x.Unidade)
                    .Include(x => x.Jornada)
                    .Include(x => x.Perfil)
                    .Include(x => x.Frequencias)
                    .Where(condicao)
                    .ToList();
        }
        public FuncionarioMo Find(Func<FuncionarioMo, bool> condicao)
        {
            return Listar(condicao).SingleOrDefault();
        }
        public void Add(FuncionarioMo entity)
        {
            Db.Funcionarios.Add(entity);
        }
        public void Update(FuncionarioMo entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }
        public void AddOrUpdate(FuncionarioMo entity)
        {
            Db.Funcionarios.AddOrUpdate(entity);
        }
        public void Excluir(int id)
        {
            Db.Funcionarios.Remove(Find(x => x.Id == id));
        }
        public bool IsAutenticado(string login, string senha)
        {
            var usu = Find(x => x.Login.Equals(login) && x.Senha.Equals(senha));
            return usu != null;
        }
    }
}
