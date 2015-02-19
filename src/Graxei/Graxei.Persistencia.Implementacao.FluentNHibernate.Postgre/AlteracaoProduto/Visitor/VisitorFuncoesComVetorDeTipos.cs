using Graxei.Transversais.Utilidades;
using Graxei.Transversais.Utilidades.Data;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor
{
    public class VisitorFuncoesComVetorDeTipos : IVisitorCriacaoFuncao
    {
        [Dependency]
        public IDataSistema DataSistema { get; set; }

        public string GetResultado()
        {
            string resultado = _selectCriar + _selectAlterar + _selectExcluir;

            int index = resultado.Length - 7;
            if (resultado.LastIndexOf(" UNION") == index)
            {
                resultado = resultado.Remove(index);
            }

            return resultado;
        }

        public string Visit(CriarProdutoVendedor funcao)
        {
            _criar.Append(string.Format("row({0}, {1}, '{2}', {3}, {4}, '{5}')::produto_criacao, ",
                                        funcao.IdProduto,
                                        funcao.IdEndereco,
                                        funcao.DescricaoProdutoVendedor,
                                        funcao.Preco.ToString("0.00", CultureInfo.InvariantCulture),
                                        funcao.Usuario.Id,
                                        PostgresComum.DataValida(DataSistema.Agora)));
            _selectCriar = Formatar(_selectCriar, _criar, "criar_produto_vendedor") + " UNION ";
            return _selectCriar;

        }

        public string Visit(AlterarProdutoVendedor funcao)
        {
            _alterar.Append(string.Format("row({0}, '{1}', {2}, {3}, '{4}')::produto_modificacao, ",
                                          funcao.IdProdutoVendedor,
                                          funcao.DescricaoProdutoVendedor,
                                          funcao.Preco.ToString("0.00", CultureInfo.InvariantCulture),
                                          funcao.Usuario.Id,
                                          PostgresComum.DataValida(DataSistema.Agora)));

            _selectAlterar = Formatar(_selectAlterar, _alterar, "alterar_produto_vendedor") + " UNION ";
            return _selectAlterar;
        }

        public string Visit(ExcluirProdutoVendedor funcao)
        {
            _excluir.Append(string.Format("row({0}, {1}, '{2}')::produto_exclusao, ",
                                          funcao.IdProdutoVendedor,
                                          funcao.Usuario.Id,
                                          PostgresComum.DataValida(DataSistema.Agora)));
            _selectExcluir = Formatar(_selectExcluir, _excluir, "excluir_produto_vendedor");
            return _selectExcluir;
        }

        private string Formatar(string select, StringBuilder row, string funcao)
        {
            select = row.ToString();
            select = select.Remove(row.Length - 2);
            select = string.Format("SELECT {0}(array[{1}])", funcao, select);
            return select;
        }

        private string _resultadoChamadaFuncoes;

        private StringBuilder _criar = new StringBuilder();

        private StringBuilder _alterar = new StringBuilder();

        private StringBuilder _excluir = new StringBuilder();

        private string _selectCriar;

        private string _selectAlterar;

        private string _selectExcluir;
    }
}
