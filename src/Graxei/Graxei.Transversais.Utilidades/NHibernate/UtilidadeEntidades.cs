using Graxei.Modelo.Generico;
namespace Graxei.Transversais.Utilidades.NHibernate
{
    public static  class UtilidadeEntidades
    {
        public static bool IsTransiente(Entidade entidate)
        {
            return entidate.Id == 0;
        }
    }
}
