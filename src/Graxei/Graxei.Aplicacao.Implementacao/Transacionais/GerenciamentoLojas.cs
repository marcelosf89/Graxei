using System.Collections.Generic;
using Graxei.Aplicacao.Contrato;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;

namespace Graxei.Aplicacao.Implementacao.Transacionais
{
    public class GerenciamentoLojas : PadraoTransacao, IGerenciamentoLojas
    {

        #region Construtor
        public GerenciamentoLojas(IServicoLojas servicoLojas, IServicoEnderecos servicoEnderecos, IServicoTelefones servicoTelefones, IServicoUsuarios servicoUsuarios)
        {
            _servicoLojas = servicoLojas;
            _servicoEnderecos = servicoEnderecos;
            _servicoUsuarios = servicoUsuarios;
            _servicoTelefones = servicoTelefones;
        }
        #endregion

        #region Implementação de IGerenciamentoLojas

        public void SalvarLoja(string nomeLoja, string loginUsuario)
        {
            Loja loja = new Loja() {Nome = nomeLoja };
            Usuario usuario = _servicoUsuarios.GetPorLogin(loginUsuario);
            if (usuario == null)
            {
                throw new OperacaoEntidadeException(string.Format(Erros.UsuarioNaoEncontrado, loginUsuario));
            }
            SalvarLoja(loja, usuario);
        }

        public void SalvarLoja(Loja loja, Usuario usuario)
        {
            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuario);
            IniciarTransacao();
            try
            {
                ServicoLojas.Salvar(loja, usuarios, usuario);
                Confirmar();
            }
            catch (OperacaoEntidadeException oe)
            {
                Desfazer();
                throw oe;
            }
        }


        public void SalvarLoja(Loja loja, IList<Usuario> usuarios, Usuario usuario)
        {
            IniciarTransacao();
            try
            {
                ServicoLojas.Salvar(loja, usuarios, usuario);
                Confirmar();
            } catch (OperacaoEntidadeException ex)
            {
                Desfazer();
                throw ex;
            }
        }

        public void SalvarLoja(Loja loja)
        {
            IniciarTransacao();
            try
            {
                ServicoLojas.Salvar(loja);
                Confirmar();
            }catch(OperacaoEntidadeException oe)
            {
                Desfazer();
                throw oe;
            }
        }

        public void ExcluirLoja(Loja loja)
        {
            throw new System.NotImplementedException();
        }

        public IServicoLojas ServicoLojas { get { return _servicoLojas; } }
        public IServicoEnderecos ServicoEnderecos { get { return _servicoEnderecos; } }
        public IServicoUsuarios ServicoUsuarios { get { return _servicoUsuarios; } }
        public IServicoTelefones ServicoTelefones { get { return _servicoTelefones; } }

        #endregion

        #region Atributos Privados
        private IServicoLojas _servicoLojas;
        private IServicoEnderecos _servicoEnderecos;
        private IServicoUsuarios _servicoUsuarios;
        private IServicoTelefones _servicoTelefones;
        #endregion

    }
}
