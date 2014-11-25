using System.Collections.Generic;
using System.Linq;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class ListaLojasNHibernateMySQL : IRepositorioListaLojas
    {
        public ListaLojas Get(int pagina, int tamanhoPagina, Usuario usuario)
        {
            int total = SessaoAtual
                .QueryOver<Loja>().JoinQueryOver<Usuario>(p => p.Usuarios).Where(q => q.Id == usuario.Id).RowCount();
            IList<ListaLojasContrato> lista =
                (from l in SessaoAtual.Query<Loja>()
                 from u in l.Usuarios
                 where u.Id == usuario.Id
                 select new ListaLojasContrato()
                 {
                     Id = l.Id,
                     Nome = l.Nome,
                     NomePlano = l.Plano.Nome
                 }
                     )
                     .Skip(pagina * tamanhoPagina)
                    .Take(tamanhoPagina).ToList<ListaLojasContrato>();

            //SessaoAtual.QueryOver<Loja>()
            //    .JoinQueryOver<Usuario>(p => p.Usuarios)
            //    .Where(q => q.Id == usuario.Id)
            //    .Select(Projections.ProjectionList()
            //            .Add(Projections.Property("Id"), "Id")
            //            .Add(Projections.Property("Nome"), "Nome"))
            //    .TransformUsing(Transformers.AliasToBean<ListaLojasContrato>())
            //    .Skip(pagina * tamanhoPagina)
            //    .Take(tamanhoPagina)
            //    .List<ListaLojasContrato>();
            ListaTotalElementos totalElementos = new ListaTotalElementos(total);
            ListaElementoAtual elementoAtual = new ListaElementoAtual(pagina);
            return new ListaLojas(lista, totalElementos, elementoAtual);
        }

        public ISession SessaoAtual
        {
            get { return UnitOfWorkNHibernate.GetInstancia().SessaoAtual; }
        }
    }
}
