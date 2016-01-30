using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Negocio.Model
{
    public class MenuMo
    {
        [Key]
        public int Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Imagem { get; set; }
        public int Peso { get; set; }
    }
}
