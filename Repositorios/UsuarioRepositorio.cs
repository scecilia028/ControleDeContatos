using ControleDeContatos.Data;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorios;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;

namespace ControleDeContatos.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        //injecao de dependencia via o que está declarado na Startup.cs de conexao de string
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            this._bancoContext = bancoContext;
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro =  DateTime.Now;
            usuario.SetSenhaHash();
            _bancoContext.Usuario.Add(usuario);
            _bancoContext.SaveChanges(); //commita as alterações

            return usuario;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = BuscarPorId(id);
            if (usuarioDB == null)
                throw new KeyNotFoundException("Houve erro na deleção do Usuario");
            
            _bancoContext.Usuario.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);
            if(usuarioDB == null)
            {
                throw new KeyNotFoundException("Houve erro na atualizacao do Usuario");
            }
            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuario.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = BuscarPorId(alterarSenhaModel.Id);
            if(usuarioDB == null) 
                throw new Exception("Houve erro na atualização da senha. Não encontramos o usuario.");
            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual))
                throw new Exception("Senha atual nao confere");
            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) 
                throw new Exception("Senha igual a anterior, devem ser diferentes.");//senhas iguais

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuario.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public UsuarioModel BuscarPorEmailLogin(string email, string login)
        {
            return _bancoContext.Usuario.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper()); //jogar todos pra caixa alta
        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _bancoContext.Usuario.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioModel buscarPorLogin(string login)
        {
            return _bancoContext.Usuario.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper()); //jogar todos pra caixa alta
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuario.ToList();
        }
    }
}
