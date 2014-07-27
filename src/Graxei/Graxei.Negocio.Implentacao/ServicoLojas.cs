using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoLojas : ServicoPadraoEntidades<Loja>, IServicoLojas
    {

        #region Construtor
        public ServicoLojas(IRepositorioLojas repositorioLojas, IServicoLojaUsuario servicoLojaUsuario, IServicoUsuarios servicoUsuarios, IServicoEnderecos servicoEnderecos)
        {
            RepositorioEntidades = repositorioLojas;
            _servicoLojaUsuario = servicoLojaUsuario;
            _servicoUsuarios = servicoUsuarios;
            _servicoEnderecos = servicoEnderecos;
        }
        #endregion

        #region Métodos de sobrescrita
        public new void Validar(Loja loja)
        {
            if (string.IsNullOrEmpty(loja.Nome))
            {
                throw new ValidacaoEntidadeException(Validacoes.NomeLojaObrigatorio);
            }
        }

        public void ValidarSeNulo(Loja loja)
        {
            if (loja == null)
            {
                throw new ArgumentException(Erros.LojaNula);
            }
        }

        public void ValidarSeNulo(Loja loja, IList<Usuario> usuarios, Usuario usuarioLog)
        {
            ValidarSeNulo(loja);
            if (loja == null)
            {
                throw new ArgumentException(Erros.LojaNula);
            }
            if (usuarios == null || !usuarios.Any())
            {
                throw new ArgumentNullException(Erros.LojasListaUsuariosVazia);
            }
            if (usuarioLog == null)
            {
                throw new ArgumentNullException(Erros.UsuarioLogNulo);
            }
        }

        public override void PreSalvar(Loja loja)
        {
            Validar(loja);
            Loja repetida = Get(loja.Nome);
            if (repetida != null)
            {
                throw new ObjetoJaExisteException(Erros.LojaJaExiste);
            }
        }

        private void ChecarEnderecos(IList<Endereco> endRepetidos)
        {
            if (endRepetidos == null || !endRepetidos.Any())
            {
                return;
            }
            string mensagem = Erros.HaEnderecosRepetidos;
            string msgEndRepetidos = string.Empty;
            foreach (Endereco endRepetido in endRepetidos)
            {
                msgEndRepetidos = msgEndRepetidos + endRepetido.ToString() + (char)10;
            }
            msgEndRepetidos = msgEndRepetidos.Substring(0, msgEndRepetidos.Length - 1);
            mensagem = string.Format(mensagem, msgEndRepetidos);
            throw new RepetidoEmColecaoException(mensagem);
        }

        public override void PreAtualizar(Loja loja)
        {
            Validar(loja);
            Loja repetida = Get(loja.Nome);
            if (repetida != null && repetida.Id != loja.Id)
            {
                throw new ObjetoJaExisteException(Erros.LojaJaExiste);
            }
            if (loja.Enderecos != null && loja.Enderecos.Any())
            {
                foreach (Endereco e in loja.Enderecos)
                {
                    _servicoEnderecos.PreAtualizar(e);
                }
            }
        }
        #endregion

        #region Implementação de IServicoLojas

        public Loja Get(string nome)
        {
            return RepositorioLojas.Get(nome);
        }

        public new void Salvar(Loja loja)
        {
            if (UtilidadeEntidades.IsTransiente(loja))
            {
                throw new InvalidOperationException(Erros.LojaSalvarInvalido);
            }
            PreAtualizar(loja);
            base.Salvar(loja);
        }

        public void Salvar(Loja loja, IList<Usuario> usuarios, Usuario usuarioLog)
        {
            ValidarSeNulo(loja, usuarios, usuarioLog);
            if (UtilidadeEntidades.IsTransiente(loja))
            {
                PreSalvar(loja);
            }
            else
            {
                PreAtualizar(loja);
            }

            //Associando usuários à loja
            IList<Usuario> usuariosNaoAssociados = new List<Usuario>();

            /*** TODO: Refatorar - Método muito grande */
            //
            foreach (Usuario usuario in usuarios)
            {
                Usuario usuarioFor = usuario;
                if (UtilidadeEntidades.IsTransiente(usuario))
                {
                    usuarioFor = _servicoUsuarios.GetPorLogin(usuario.Login);
                    if (usuarioFor == null)
                    {
                        throw new ObjetoNaoEncontradoException(String.Format(Erros.UsuarioNaoEncontrado, usuario.Login));
                    }
                }
                if (!_servicoLojaUsuario.Existe(loja, usuarioFor))
                {
                    usuariosNaoAssociados.Add(usuarioFor);
                }
            }
            RepositorioLojas.Salvar(loja);

            IList<LojaUsuario> lojaUsuarios = new List<LojaUsuario>();
            foreach (Usuario usuariosNaoAssociado in usuariosNaoAssociados)
            {
                LojaUsuario lojaUsuario = new LojaUsuario();
                lojaUsuario.Loja = loja;
                lojaUsuario.Usuario = usuariosNaoAssociado;
                lojaUsuario.DataRegistro = DateTime.Now;
                lojaUsuario.UsuarioLog = usuarioLog;
                lojaUsuarios.Add(lojaUsuario);
            }
            RepositorioLojas.Salvar(lojaUsuarios);
        }

        public IServicoEnderecos ServicoEnderecos { get { return _servicoEnderecos; } }
        #endregion

        #region Atributos Privados
        private IRepositorioLojas RepositorioLojas { get { return (IRepositorioLojas)RepositorioEntidades; } }
        #endregion

        #region Implementation of IExcluirEntidade<Loja>

        public new void Excluir(Loja loja)
        {
            /* TODO: implementar restrições de exclusão */
            RepositorioLojas.Excluir(loja);
        }

        #endregion

        #region Implementação de IServicoEntidades<Loja>

        public Loja GetPorId(long id)
        {
            return RepositorioLojas.GetPorId(id);
        }

        public IList<Loja> Todos()
        {
            throw new NotImplementedException();
        }

        public IRepositorioEntidades<Loja> RepositorioEntidades { get; private set; }

        #endregion

        #region Atributos Privados

        private IServicoEnderecos _servicoEnderecos;
        private IServicoUsuarios _servicoUsuarios;
        private IServicoLojaUsuario _servicoLojaUsuario;

        #endregion

    }
}
