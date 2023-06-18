using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite a senha atual do usuário")]
        public string SenhaAtual { get; set; }
        [Required(ErrorMessage = "Digite a nova senha do usuário")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "Confirme nova senha")]
        [Compare("NovaSenha", ErrorMessage = "Senha não coinide com a nova senha")]
        public string ConfirmarNovaSenha { get; set; }

    }
}
