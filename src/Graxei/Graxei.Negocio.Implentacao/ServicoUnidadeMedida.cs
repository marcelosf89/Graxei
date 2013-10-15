using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoUnidadeMedida : ServicoPadraoSomenteLeitura<UnidadeMedida>, IServicoUnidadeMedida
    {
        #region Implementação of IServicoUnidadeMedida

        public void PreSalvar(UnidadeMedida unidade)
        {
            if (unidade == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(unidade.Sigla))
            {
                throw new ValidacaoEntidadeException(Erros.SiglaUnidadeMedidaNula);
            }
            if (string.IsNullOrEmpty(unidade.Descricao))
            {
                throw new ValidacaoEntidadeException(Erros.DescricaoUnidadeMedidaNula);
            }
        }

        public void PreAtualizar(UnidadeMedida unidade)
        {
            PreSalvar(unidade);
        }

        #endregion
    }
}