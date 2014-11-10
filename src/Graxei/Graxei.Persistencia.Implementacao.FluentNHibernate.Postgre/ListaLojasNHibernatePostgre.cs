using System.Collections.Generic;
using FluentNHibernate.Utils;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.ContratosDeDados;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class ListaLojasNHibernatePostgre : IRepositorioListaLojas
    {
        public ListaLojas Get(int pagina, int tamanhoPagina, Usuario usuario)
        {
            int total = SessaoAtual
                .QueryOver<Loja>().JoinQueryOver<Usuario>(p => p.Usuarios).Where(q => q.Id == usuario.Id).RowCount();
            ListaLojasContrato listaLojasContrato = null;
            IList<ListaLojasContrato> lista =
                SessaoAtual.QueryOver<Loja>()
                    .JoinQueryOver<Usuario>(p => p.Usuarios)
                    .Where(q => q.Id == usuario.Id)
                    .Select(Projections.ProjectionList()
                            .Add(Projections.Property("Id"), "Id")
                            .Add(Projections.Property("Nome"), "Nome"))
                    .TransformUsing(Transformers.AliasToBean<ListaLojasContrato>())
                    .Skip(pagina)
                    .Take(tamanhoPagina)
                    .List<ListaLojasContrato>();
                    ////.Skip(pagina)
                    ////.Take(tamanhoPagina).List<Loja>()
                    
                    
                    
            return new ListaLojas(lista, total);
        }
        
        public ISession SessaoAtual
        {
            get { return UnitOfWorkNHibernate.GetInstancia().SessaoAtual; }
        }
    }
}
