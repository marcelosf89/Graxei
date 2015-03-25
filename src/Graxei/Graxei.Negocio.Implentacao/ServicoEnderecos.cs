using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Comportamento;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;
using Graxei.Negocio.Implementacao.Especificacoes;
using Graxei.Negocio.Contrato.Especificacoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoEnderecos : GravacaoTemplateMethod<Endereco>, IServicoEnderecos
    {

        public ServicoEnderecos(IRepositorioEnderecos repositorioEnderecos, IGerenciadorAutenticacao gerenciadorAutenticacao)
        {
            _repositorioEnderecos = repositorioEnderecos;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
        }
        
        public enum AtributosOrdem { Sigla, Nome }

        public bool UsuarioAtualAssociado(Endereco endereco)
        {
            return _repositorioEnderecos.UsuarioAssociado(endereco, _gerenciadorAutenticacao.Get());
        }

        public override Endereco Salvar(Endereco endereco)
        {
            if (especificacaoAlterar == null)
            {
                especificacaoAlterar = new EnderecosAtualizar(this);
            }
            if (especificacaoSalvar == null)
            {
                especificacaoSalvar = new EnderecosSalvar(this);
            }

            PreGravar(endereco);
            return _repositorioEnderecos.Salvar(endereco);
        }

        public override IEspecificacao<Endereco> GetEspecificacaoSalvarPadrao()
        {
            return new EnderecosSalvar(this);
        }

        public override IEspecificacao<Endereco> GetEspecificacaoAlterarPadrao()
        {
            return new EnderecosAtualizar(this);
        }

        public override Endereco GetPorId(long id)
        {
            return _repositorioEnderecos.GetPorId(id);
        }

        public void PreExcluir(Endereco t)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Endereco t)
        {
            throw new NotImplementedException();
        }

        public Endereco Get(long id)
        {
            return _repositorioEnderecos.Get(id);
        }

        public Endereco Get(long idLoja, string logradouro, string numero, string complemento, long idBairro){
            return _repositorioEnderecos.Get(idLoja, logradouro, numero, complemento, idBairro);
        }
        public List<Endereco> GetPorLoja(long idLoja)
        {
            return _repositorioEnderecos.GetPorLoja(idLoja);
        }

        private IRepositorioEnderecos _repositorioEnderecos;
        private IGerenciadorAutenticacao _gerenciadorAutenticacao;
    }
}