using Graxei.Modelo.Generico;
namespace Graxei.Transversais.Comum.NHibernate
{
    public static  class UtilidadeEntidades
    {
        public static bool IsTransiente(Entidade entidate)
        {
            return entidate.Id == 0;
        }
    }
}
