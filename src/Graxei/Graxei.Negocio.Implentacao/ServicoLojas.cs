using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Comportamento;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Comum.Excecoes;
using Graxei.Negocio.Implementacao.Especificacoes;
using Graxei.Transversais.Comum.Autenticacao.Interfaces;
using Graxei.Negocio.Contrato.Especificacoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoLojas : GravacaoTemplateMethod<Loja>, IServicoLojas
    {
        public ServicoLojas(IRepositorioLojas repositorioLojas, IServicoPlanos servicoPlanos, IGerenciadorAutenticacao gerenciadorAutenticacao, IRepositorioPlanos repositorioPlanos)
        {
            RepositorioEntidades = repositorioLojas;
            _servicoPlanos = servicoPlanos;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
            _repositorioPlanos = repositorioPlanos;
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

        public Loja GetComEnderecosPlanos(long id)
        {
            return RepositorioLojas.GetComEnderecosPlanos(id);
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

        public Plano GetPlano(long idLoja)
        {
            return _repositorioPlanos.GetPlano(idLoja);
        }

        public IList<Loja> Todos()
        {
            throw new NotImplementedException();
        }

        public Endereco GetEnderecoComTelefones(long idEndereco)
        {
            return RepositorioLojas.GetEnderecoComTelefones(idEndereco);
        }

        public IRepositorioEntidades<Loja> RepositorioEntidades { get; private set; }

        private Usuario _usuario;

        private IServicoPlanos _servicoPlanos;

        private IRepositorioPlanos _repositorioPlanos;

        private IGerenciadorAutenticacao _gerenciadorAutenticacao;

        private IRepositorioLojas RepositorioLojas { get { return (IRepositorioLojas)RepositorioEntidades; } }

    }
}
