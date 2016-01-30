using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class FuncionarioListViewModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }
        public string Cargo { get; set; }
        public string Unidade { get; set; }
        public string Perfil { get; set; }

        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}