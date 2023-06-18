using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    //login nao usa o filtro pq eh redirecionado para ele
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                               ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;        
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            //se usuario estiver logado, redirecionar para home
            if(_sessao.BuscarSessaoUsuario() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid) //verifica os campos da model se estao todos preenchidos
                {
                    UsuarioModel usuario = _usuarioRepositorio.buscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha do usuário é inválida.";
                    }

                    TempData["MensagemErro"] = $"Usuário e/ou Senha inválido(s).";

                }

                return View("Index"); //se der erro vai pra index  do login 
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível realizar o login, detalhe do erro:{erro.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid) //verifica os campos da model se estao todos preenchidos
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha(); //nova senha que ira para o disparo do email
                       
                        string mensagem = $"Sua nova senha é: {novaSenha}";
                        //enviar email com senha 
                        bool emailEnviado = _email.Enviar(usuario.Email, "Nova Senha - Sistema de Contatos", mensagem);

                        if (emailEnviado)
                        {
                            //update no banco a senha hash
                            _usuarioRepositorio.Atualizar(usuario); //senha sem hash
                            TempData["MensagemSucesso"] = $"Enviamos para seu email cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o email.";
                        }
                        
                        return RedirectToAction("Index", "Login");                
                    }
                TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha, verifique os dados informados.";
                }

                return View("Index"); //se der erro vai pra index  do login 
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua senha, detalhe do erro:{erro.Message} ";
                return RedirectToAction("Index");
            }
        }



    }
}
