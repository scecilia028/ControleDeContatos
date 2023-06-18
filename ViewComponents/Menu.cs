using ControleDeContatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ControleDeContatos.ViewComponents
{
    public class Menu : ViewComponent
    {

        /* Mesmo que tenha sido criado os metodos referente a sessão: criar, buscar e remover, ainda assim 
         * é necessário fazer com que seja recuperado nas paginas da view. Com isso, este método
         * permite recuperar de forma dinamica os dados da sessao, colocando em um componente. 
         * A criação de um componente evita chamar diversas vezes este codigo. Sem falar que 
         * usando componente torna mais seguro e pratico.
         * 
         * Éste metodo recupera dados do usuario para ser usado nas views (componente menu) de forma simples
         * e reutilizavel para outras paginas.        * 
         * */

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if(string.IsNullOrEmpty(sessaoUsuario) )
            {
                return null;
            }

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);  

            //retorna a view padrao do component Menu (padrao default)
            return View(usuario);
        }
    }
}
