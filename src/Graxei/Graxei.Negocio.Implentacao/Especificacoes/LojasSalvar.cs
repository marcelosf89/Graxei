using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Especificacoes;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Implementacao.Especificacoes
{
    public class LojasSalvar : IEspecificacao<Loja>
    {
        public LojasSalvar(IServicoLojas servicoLojas)
        {
            _servicoLojas = servicoLojas;
        }

        public ResultadoEspecificacao Satisfeita(Loja loja)
        {
            if (loja == null)
            {
                return new ResultadoEspecificacao(false, Erros.LojaNula);
            }

            if (!UtilidadeEntidades.IsTransiente(loja))
            {
                throw new OperacaoEntidadeException("Não foi possível salvar os dados para esta loja. Por favor, contate o suporte.");
            }

            if (string.IsNullOrEmpty(loja.Nome))
            {
                return new ResultadoEspecificacao(false, Erros.LojaNomeNulo);
            }

            if (loja.Usuarios == null || loja.Usuarios.Count == 0)
            {
                return new ResultadoEspecificacao(false, Erros.LojasListaUsuariosVazia);
            }

            if (_servicoLojas.Get(loja.Nome) != null)
            {
                return new ResultadoEspecificacao(false, Erros.LojaJaExiste);
            }

            return new ResultadoEspecificacao();
        }

        private IServicoLojas _servicoLojas;


    }
}