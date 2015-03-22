using Graxei.Modelo.Generico;
using Graxei.Negocio.Contrato.Especificacoes;
using Graxei.Transversais.Utilidades.NHibernate;

namespace Graxei.Negocio.Contrato.Comportamento
{
    /// <summary>
    /// Padrão de projetos: Template Method
    ///              Papel: Template Method
    ///           Objetivo: Cria o template para que o serviço de domínio decida se é uma criação ou uma atualização da entidade
    /// </summary>
    public abstract class GravacaoTemplateMethod<T> : IEntidadesSalvar<T> where T : Entidade
    {
        public ResultadoEspecificacao PreGravar(T entidade)
        {
            ResultadoEspecificacao resultado;
            if (UtilidadeEntidades.IsTransiente(entidade))
            {
                resultado = especificacaoSalvar.Satisfeita(entidade);
            }
            else
            {
                resultado = especificacaoAlterar.Satisfeita(entidade);
            }

            return resultado;
        }

        public abstract T Salvar(T t);

        public abstract T GetPorId(long id);

        protected IEspecificacao<T> especificacaoSalvar;

        protected IEspecificacao<T> especificacaoAlterar;

    }
}
