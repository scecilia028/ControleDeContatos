using ControleDeContatos.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ControleDeContatos.Helper
{
    public class Sessao : ISessao
    {
        //é necessario injetar esta variavel de dependencia para contexto
        //É usada a interface para injeção de dependencia
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        UsuarioModel ISessao.BuscarSessaoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        void ISessao.CriarSessaoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario); //biblioteca newtonSoft. json
            //o json é convertido de um objeto. Tudo de usuarioModel é trazido para cá convertido para uma string serializada
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        void ISessao.RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
