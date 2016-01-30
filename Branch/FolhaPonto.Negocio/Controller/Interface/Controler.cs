using System;

namespace FolhaPonto.Negocio.Controller.Interface
{
    public class Controler:IDisposable
    {
        public FolhaContext Db { get; set; }

        public Controler()
        {
            Db = new FolhaContext();
        }

        public void Commit()
        {
            Db.SaveChanges();
        }
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
