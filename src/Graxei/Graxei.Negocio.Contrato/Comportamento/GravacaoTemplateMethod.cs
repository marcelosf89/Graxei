using Graxei.Modelo.Generico;
using Graxei.Negocio.Contrato.Especificacoes;
using Graxei.Transversais.Comum.NHibernate;

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
                resultado = GetEspecificacaoSalvar().Satisfeita(entidade);
            }
            else
            {
                resultado = GetEspecificacaoAlterar().Satisfeita(entidade);
            }

            return resultado;
        }

        public abstract IEspecificacao<T> GetEspecificacaoSalvarPadrao();

        public abstract IEspecificacao<T> GetEspecificacaoAlterarPadrao();

        public IEspecificacao<T> GetEspecificacaoSalvar()
        {
            if (especificacaoSalvar == null)
            {
                return GetEspecificacaoSalvarPadrao();
            }
            return especificacaoSalvar;
        }

        public IEspecificacao<T> GetEspecificacaoAlterar()
        {
            if (especificacaoAlterar == null)
            {
                return GetEspecificacaoAlterarPadrao();
            }
            return especificacaoAlterar;
        }

        public void SetEspecificacaoSalvar(IEspecificacao<T> especificacao)
        {
            especificacaoSalvar = especificacao;
        }

        public void SetEspecificacaoAlterar(IEspecificacao<T> especificacao)
        {
            especificacaoAlterar = especificacao;
        }

        public abstract T Salvar(T t);

        public abstract T GetPorId(long id);

        protected IEspecificacao<T> especificacaoSalvar;

        protected IEspecificacao<T> especificacaoAlterar;

    }
}
