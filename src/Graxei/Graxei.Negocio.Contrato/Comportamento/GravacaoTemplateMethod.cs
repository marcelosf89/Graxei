using FAST.Modelo;
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
        public void PreGravar(T entidade)
        {
            if (UtilidadeEntidades.IsTransiente(entidade))
            {
                PreSalvar(entidade);
            }
            else
            {
                PreAtualizar(entidade);    
            }
        }

        public abstract void PreSalvar(T t);

        public abstract void PreAtualizar(T t);

        public abstract T Salvar(T t);

        public abstract T GetPorId(long id);

    }
}
