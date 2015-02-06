using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Comportamento;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoLojas : GravacaoTemplateMethod<Loja>, IServicoLojas
    {
        private Usuario _usuario;
        private IServicoPlanos _servicoPlanos;
        #region Construtor
        public ServicoLojas(IRepositorioLojas repositorioLojas, IServicoPlanos servicoPlanos)
        {
            RepositorioEntidades = repositorioLojas;
            _servicoPlanos = servicoPlanos;
        }
        #endregion

        #region Métodos de sobrescrita

        public void Validar(Loja loja)
        {
            if (string.IsNullOrEmpty(loja.Nome))
            {
                throw new ValidacaoEntidadeException(Validacoes.NomeLojaObrigatorio);
            }
        }

        public override void PreSalvar(Loja loja)
        {
            if (_usuario == null)
            {
                throw new ValidacaoEntidadeException(Erros.UmUsuarioAoMenos);
            }
            loja.Plano = _servicoPlanos.GetPorId(1);
            Validar(loja);
            loja.AdicionarUsuario(_usuario);
            Loja repetida = Get(loja.Nome);
            if (repetida != null)
            {
                throw new ObjetoJaExisteException(Erros.LojaJaExiste);
            }
        }

        public override void PreAtualizar(Loja loja)
        {
            Validar(loja);
            Loja repetida = Get(loja.Nome);
            if (repetida != null && repetida.Id != loja.Id)
            {
                throw new ObjetoJaExisteException(Erros.LojaJaExiste);
            }
        }

        public override Loja Salvar(Loja loja)
        {
            PreGravar(loja);
            loja = this.RepositorioLojas.Salvar(loja);
            return loja;
        }

        #endregion

        #region Implementação de IServicoLojas

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

        #endregion

        #region Atributos Privados
        private IRepositorioLojas RepositorioLojas { get { return (IRepositorioLojas)RepositorioEntidades; } }
        #endregion

        #region Implementation of IExcluirEntidade<Loja>

        public void PreExcluir(Loja t)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Loja t)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementação de IServicoEntidades<Loja>

        public override Loja GetPorId(long id)
        {
            return RepositorioLojas.GetPorId(id);
        }

        public IList<Loja> Todos()
        {
            throw new NotImplementedException();
        }

        public IRepositorioEntidades<Loja> RepositorioEntidades { get; private set; }

        #endregion

    }
}
