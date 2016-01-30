using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome da Action")]
        [Required(ErrorMessage = "Action é obrigatório", AllowEmptyStrings = false)]
        public string Action { get; set; }

        [Display(Name = "Nome do Controller")]
        [Required(ErrorMessage = "Controller é obrigatório", AllowEmptyStrings = false)]
        public string Controller { get; set; }

        [Required(ErrorMessage = "Peso é obrigatório", AllowEmptyStrings = false)]
        public int Peso { get; set; }

        [Required(ErrorMessage = "Imagem é obrigatório", AllowEmptyStrings = false)]
        public string Imagem { get; set; }
    }
}