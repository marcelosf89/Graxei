using Graxei.FluentNHibernate.Configuracao;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.NHibernate
{

    public class FluentNHMySQLProdutos : IRepositorioProdutos
    {

        /*public FluentNHMySQLProdutos(ISession sessao)
        {
            _sessao = sessao;   
        }*/

        public void Salvar(Produto produto)
        {
            NHibernateSessionPerRequest.GetCurrentSession().SaveOrUpdate(produto);
        }

        public void Excluir(Produto produto)
        {
            NHibernateSessionPerRequest.GetCurrentSession().Delete(produto);
        }

        public Produto GetPorId(long id)
        {
            /**** TODO: Implementar */
            throw new NotImplementedException("");
        }

    }

}
