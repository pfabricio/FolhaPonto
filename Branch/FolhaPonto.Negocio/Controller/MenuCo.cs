using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FolhaPonto.Negocio.Controller.Interface;
using FolhaPonto.Negocio.Model;

namespace FolhaPonto.Negocio.Controller
{
    public class MenuCo: Controler, IControllerBase<MenuMo>
    {
        public List<MenuMo> Listar()
        {
            return Db.Menus.ToList();
        }
        public List<MenuMo> Listar(Func<MenuMo, bool> condicao)
        {
            return Db.Menus.Where(condicao).ToList();
        }
        public MenuMo Find(Func<MenuMo, bool> condicao)
        {
            return Listar(condicao).SingleOrDefault();
        }
        public void Add(MenuMo entity)
        {
            Db.Menus.Add(entity);
        }
        public void Update(MenuMo entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }
        public void AddOrUpdate(MenuMo entity)
        {
            Db.Menus.AddOrUpdate(entity);
        }
        public void Excluir(int id)
        {
            Db.Menus.Remove(Find(x => x.Id == id));
        }
        public List<MenuMo> GetMenus(int peso)
        {
            var lst = Listar(x => peso >= x.Peso);
            return lst;
        }
    }
}
