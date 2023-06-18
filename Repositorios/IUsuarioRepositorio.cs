using ControleDeContatos.Models;
using System;
using System.Collections.Generic;

namespace ControleDeContatos.Repositorios
{
    public interface IUsuarioRepositorio
    {

        UsuarioModel buscarPorLogin(string login);

        UsuarioModel Adicionar(UsuarioModel usuario);

        List<UsuarioModel> BuscarTodos();

        UsuarioModel BuscarPorId(int id);

        UsuarioModel Atualizar(UsuarioModel usuario);

        bool Apagar(int id);

        UsuarioModel BuscarPorEmailLogin(string email, string login);

        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
    }
}
