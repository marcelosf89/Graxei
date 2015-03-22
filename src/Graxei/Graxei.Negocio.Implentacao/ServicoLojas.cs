using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Comportamento;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Negocio.Implementacao.Especificacoes;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoLojas : GravacaoTemplateMethod<Loja>, IServicoLojas
    {
        public ServicoLojas(IRepositorioLojas repositorioLojas, IServicoPlanos servicoPlanos, IGerenciadorAutenticacao gerenciadorAutenticacao)
        {
            RepositorioEntidades = repositorioLojas;
            _servicoPlanos = servicoPlanos;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
        }
        
        public override Loja Salvar(Loja loja)
        {
            if (especificacaoAlterar == null)
            {
                especificacaoAlterar = new LojasAtualizar(this);
            }
            if (especificacaoSalvar == null)
            {
                especificacaoSalvar = new LojasSalvar(this);
            }

            PreGravar(loja);
            loja = this.RepositorioLojas.Salvar(loja);
            return loja;
        }

        public bool UsuarioAtualAssociado(Loja loja)
        {
            return RepositorioLojas.UsuarioAssociado(loja, _gerenciadorAutenticacao.Get());
        }

        public Loja Get(string nome)
        {
            return RepositorioLojas.Get(nome);
        }

        public Loja Salvar(Loja loja, Usuario usuario)
        {
            _usuario = usuario;
            return Salvar(loja);
        }

        public Loja GetComEnderecos(long id)
        {
            return RepositorioLojas.GetComEnderecos(id);
        }

        public Loja GetPorUrl(String nome)
        {
            return RepositorioLojas.GetPorUrl(nome);
        }

        public IList<long> GetIdsEnderecos(long idLoja)
        {
            return RepositorioLojas.GetIdsEnderecos(idLoja);
        }

        private IRepositorioLojas RepositorioLojas { get { return (IRepositorioLojas)RepositorioEntidades; } }


        public void PreExcluir(Loja t)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Loja t)
        {
            throw new NotImplementedException();
        }

        public override Loja GetPorId(long id)
        {
            return RepositorioLojas.GetPorId(id);
        }

        public IList<Loja> Todos()
        {
            throw new NotImplementedException();
        }

        public IRepositorioEntidades<Loja> RepositorioEntidades { get; private set; }

        private Usuario _usuario;

        private IServicoPlanos _servicoPlanos;

        private IGerenciadorAutenticacao _gerenciadorAutenticacao;

    }
}
