using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Comportamento;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;

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
        
        public override void PreSalvar(Endereco endereco)
        {
            ValidarEspecificacao(endereco);
            ChecarSeguranca(endereco);
            Endereco enderecoRepetido = _repositorioEnderecos.Get(endereco.Loja.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro.Id);
            if (enderecoRepetido != null)
            {
                throw new OperacaoEntidadeException("Já existe um endereço para esta loja");
            }
        }

        private void ChecarSeguranca(Endereco endereco)
        {
            Usuario usuarioLogado = _gerenciadorAutenticacao.Get();
            bool associado = _repositorioEnderecos.UsuarioAssociado(endereco, usuarioLogado);
            if (!associado)
            {
                throw new SegurancaEntidadeException(string.Format("Usuário {0} não tem acesso à loja {1}", usuarioLogado.Nome,
                    endereco.Loja.Nome));
            }
        }

        public override void PreAtualizar(Endereco endereco)
        {
            ValidarEspecificacao(endereco);
            ChecarSeguranca(endereco);
            Endereco enderecoRepetido = _repositorioEnderecos.Get(endereco.Loja.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro.Id);
            if (enderecoRepetido != null && enderecoRepetido.Id != endereco.Id)
            {
                throw new OperacaoEntidadeException("Já existe um endereço para esta loja");
            }
        }

        public override Endereco Salvar(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new NullReferenceException("Endereço não pode ser nulo");
            }

            PreGravar(endereco);
            return _repositorioEnderecos.Salvar(endereco);
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

        public List<Endereco> GetPorLoja(long idLoja)
        {
            return _repositorioEnderecos.GetPorLoja(idLoja);
        }

        private void ValidarEspecificacao(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new ArgumentNullException("endereco", "Endereço não pode ser nulo");
            }
            if (endereco.Loja == null || UtilidadeEntidades.IsTransiente(endereco.Loja))
            {
                throw new OperacaoEntidadeException("O endereço deve estar associado a uma loja");
            }
            if (string.IsNullOrEmpty(endereco.Logradouro))
            {
                throw new OperacaoEntidadeException("O endereço deve ter um logradouro");
            }
            if (string.IsNullOrEmpty(endereco.Numero))
            {
                throw new OperacaoEntidadeException("O endereço deve ter um número");
            }
            if (endereco.Bairro == null)//// || UtilidadeEntidades.IsTransiente(endereco.Bairro))
            {
                throw new OperacaoEntidadeException("O endereço deve possuir um bairro");
            }
        }

        private IRepositorioEnderecos _repositorioEnderecos;
        private IGerenciadorAutenticacao _gerenciadorAutenticacao;
    }
}