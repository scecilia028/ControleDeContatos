using ControleDeContatos.Models;
using System;
using System.Collections.Generic;

namespace ControleDeContatos.Repositorios
{
    public interface IContatoRepositorio
    {
        ContatoModel Adicionar(ContatoModel contato);

        List<ContatoModel> BuscarTodos();

        ContatoModel BuscarPorId(int id);

        ContatoModel Atualizar(ContatoModel contato);

        bool Apagar(int id);
    }
}
