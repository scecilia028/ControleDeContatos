using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;

namespace ControleDeContatos.Repositorios
{
    public class ContatoRepositorio : IContatoRepositorio
    {

        //injecao de dependencia via o que está declarado na Startup.cs de conexao de string
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            this._bancoContext = bancoContext;
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contato.Add(contato);
            _bancoContext.SaveChanges(); //commita as alterações

            return contato;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = BuscarPorId(id);
            if (contatoDB == null)
                throw new KeyNotFoundException("Houve erro na deleção do contato");
            
            _bancoContext.Contato.Remove(contatoDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = BuscarPorId(contato.Id);
            if(contatoDB == null)
            {
                throw new KeyNotFoundException("Houve erro na atualizacao do contato");
            }
            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _bancoContext.Contato.Update(contatoDB);
            _bancoContext.SaveChanges();
            return contatoDB;
        }

        public ContatoModel BuscarPorId(int id)
        {
            return _bancoContext.Contato.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contato.ToList();
        }
    }
}
