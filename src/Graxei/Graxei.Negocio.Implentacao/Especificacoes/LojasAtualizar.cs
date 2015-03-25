using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Especificacoes;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Implementacao.Especificacoes
{
    public class LojasAtualizar : IEspecificacao<Loja>
    {
        public LojasAtualizar(IServicoLojas servicoLojas)
        {
            _servicoLojas = servicoLojas;
        }

        public ResultadoEspecificacao Satisfeita(Loja loja)
        {
            if (loja == null)
            {
                return new ResultadoEspecificacao(false, Erros.LojaNula);
            }

            if (UtilidadeEntidades.IsTransiente(loja))
            {
                throw new OperacaoEntidadeException("Loja deve ser transiente");
            }

            if (string.IsNullOrEmpty(loja.Nome))
            {
                return new ResultadoEspecificacao(false, Erros.LojaNomeNulo);
            }

            if (loja.Usuarios == null || loja.Usuarios.Count == 0)
            {
                return new ResultadoEspecificacao(false, Erros.LojasListaUsuariosVazia);
            }

            Loja repetida = _servicoLojas.Get(loja.Nome);
            if (repetida != null && repetida.Id != loja.Id)
            {
                return new ResultadoEspecificacao(false, Erros.LojaJaExiste);
            }

            return new ResultadoEspecificacao();
        }

        private void ChecarSeguranca(Loja loja)
        {
            if (!_servicoLojas.UsuarioAtualAssociado(loja))
            {
                throw new SegurancaEntidadeException(string.Format("Usuário não tem acesso à loja {0}", loja.Nome));
            }
        }

        private IServicoLojas _servicoLojas;
    }
}
