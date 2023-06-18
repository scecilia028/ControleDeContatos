using ControleDeContatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace ControleDeContatos.Filters
{
    public class PaginaUsuarioLogado : ActionFilterAttribute
    {
        //bloquear acesso restrito pela rota do url
        //OnActionExecuted é executado toda vez que uma action for pretendida a ser chamada

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //verificar se está logado
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) //se nao estiver logado
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller", "Login"}, {"action", "Index" } });
            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);  

                if (usuario == null)  //por algum motivo nao conseguiu se logar na sessao
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }
            //chama o metodo herdado
            base.OnActionExecuting(context);

            //o filtro precisa ser usado em cada controller a qual está sujeito
        }
    }
}
