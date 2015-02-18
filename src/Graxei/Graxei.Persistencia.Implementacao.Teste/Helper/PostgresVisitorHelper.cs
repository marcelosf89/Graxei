using Graxei.Modelo;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Transversais.Utilidades.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.Teste.Helper
{
    public class PostgresVisitorHelper
    {
        public static ExcluirProdutoVendedor GetExcluirPadrao()
        {
            ExcluirProdutoVendedor excluirProdutoVendedor = new ExcluirProdutoVendedor(1, GetUsuario(10));
            return excluirProdutoVendedor;
        }

        public static CriarProdutoVendedor GetCriarPadrao()
        {
            CriarProdutoVendedor criarProdutoVendedor = new CriarProdutoVendedor(1, "NovaDescricao", 110.25, 2, GetUsuario(1));
            return criarProdutoVendedor;
        }

        public static AlterarProdutoVendedor GetAlterarPadrao()
        {
            AlterarProdutoVendedor alterarProdutoVendedor = new AlterarProdutoVendedor(1, "DescricaoModificada", 15.33, GetUsuario(25));
            return alterarProdutoVendedor;
        }

        public static string GetLimpo(string str)
        {
            if (str.Contains("{0}"))
            {
                str = string.Format(str, string.Empty);
            }
            return str.Remove(str.Length - 7);
        }

        public static Usuario GetUsuario(int id)
        {
            return new Usuario() { Id = id };
        }

        public static VisitorFuncoesComVetorDeTipos GetVisitorAndVisit(params IMudancaProdutoVendedorFuncao[] produtos)
        {
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(_data);
            for (int i = 0; i < produtos.Length; i++)
            {
                if (produtos[i] is CriarProdutoVendedor)
                {
                    visitor.Visit((CriarProdutoVendedor)produtos[i]);
                }
                else if (produtos[i] is AlterarProdutoVendedor)
                {
                    visitor.Visit((AlterarProdutoVendedor)produtos[i]);
                }
                else
                {
                    visitor.Visit((ExcluirProdutoVendedor)produtos[i]);
                }
            }
            return visitor;
        }

        public static DateTime _data = new DateTime(2010, 11, 12, 13, 39, 36, 123);

        private class DataSistemaTeste : IDataSistema
        {
            private DateTime _data;
            public DataSistemaTeste(DateTime data)
            {
                _data = data;
            }

            public DateTime Agora
            {
                get
                {
                    return _data;
                }
            }
        }
    }
}
