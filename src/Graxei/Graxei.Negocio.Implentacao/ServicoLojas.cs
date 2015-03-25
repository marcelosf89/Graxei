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
using Graxei.Negocio.Contrato.Especificacoes;

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
            ResultadoEspecificacao resultadoEspecificacao = PreGravar(loja);
            if (!resultadoEspecificacao.Ok)
            {
                throw new ValidacaoEntidadeException(resultadoEspecificacao.Mensagem);
            }
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
            loja.AdicionarUsuario(usuario);
            loja.Plano = _servicoPlanos.GetPorId(1);
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

        public void PreExcluir(Loja t)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Loja t)
        {
            throw new NotImplementedException();
        }

        public override IEspecificacao<Loja> GetEspecificacaoSalvarPadrao()
        {
            return new LojasSalvar(this);
        }

        public override IEspecificacao<Loja> GetEspecificacaoAlterarPadrao()
        {
            return new LojasSalvar(this);
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

        private IRepositorioLojas RepositorioLojas { get { return (IRepositorioLojas)RepositorioEntidades; } }

    }
}
