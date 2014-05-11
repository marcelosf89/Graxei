using System;
using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;

namespace Graxei.Aplicacao.Implementacao.Transacionais
{
    public class GerenciamentoLojas : PadraoTransacao, IGerenciamentoLojas
    {

        #region Construtor
        public GerenciamentoLojas(IServicoLojas servicoLojas, IServicoEnderecos servicoEnderecos, IServicoTelefones servicoTelefones, IServicoUsuarios servicoUsuarios, ITransformacaoMutua<Loja, LojaContrato> transformacao)
        {
            _servicoLojas = servicoLojas;
            _servicoEnderecos = servicoEnderecos;
            _servicoUsuarios = servicoUsuarios;
            _servicoTelefones = servicoTelefones;
            _transformacao = transformacao;
        }
        #endregion

        #region Implementação de IGerenciamentoLojas

        /// <summary>
        /// Cria uma nova loja e a associa ao usuário
        /// </summary>
        /// <param name="nomeLoja">O nome da nova loja</param>
        /// <param name="usuario">O usuário a ser associado à loja</param>
        /// <returns></returns>
        public LojaContrato SalvarLoja(string nomeLoja, Usuario usuario)
        {
            LojaContrato lojaContrato = new LojaContrato();
            lojaContrato.Nome = nomeLoja;
            return SalvarLoja(lojaContrato, usuario);
        }

        /// <summary>
        /// Cria uma nova loja e a associa ao usuário
        /// </summary>
        /// <param name="lojaContrato">O nome da nova loja</param>
        /// <param name="usuario">O usuário a ser associado à loja</param>
        /// <returns></returns>
        public LojaContrato SalvarLoja(LojaContrato lojaContrato, Usuario usuario)
        {
            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuario);
            Loja loja = _transformacao.Transformar(lojaContrato);
            IniciarTransacao();
            try
            {
                _servicoLojas.Salvar(loja, usuarios, usuario);
                Confirmar();
            }
            catch (OperacaoEntidadeException)
            {
                Desfazer();
                throw;
            }
            return _transformacao.Transformar(loja);
        }

        /// <summary>
        /// Atualiza a loja. Se esta for transiente, será disparada exceção
        /// </summary>
        /// <param name="lojaContrato">A loja a ser salva</param>
        /// <returns></returns>
        public LojaContrato SalvarLoja(LojaContrato lojaContrato)
        {
            Loja loja = _transformacao.Transformar(lojaContrato);
            IniciarTransacao();
            try
            {
                _servicoLojas.Salvar(loja);
                Confirmar();
            }catch(OperacaoEntidadeException oe)
            {
                Desfazer();
                throw;
            }
            return _transformacao.Transformar(loja);
        }

        public void ExcluirLoja(LojaContrato loja)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Atributos Privados
        private IServicoLojas _servicoLojas;
        private IServicoEnderecos _servicoEnderecos;
        private IServicoUsuarios _servicoUsuarios;
        private IServicoTelefones _servicoTelefones;
        private ITransformacaoMutua<Loja, LojaContrato> _transformacao;
        #endregion

    }
}
