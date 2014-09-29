using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class EnderecosNHibernateMySQL : PadraoNHibernateMySQL<Endereco>, IRepositorioEnderecos
    {

        #region Construtor
        public EnderecosNHibernateMySQL(IRepositorioEstados repoEstados, IRepositorioCidades repoCidades, IRepositorioBairros repoBairros)
        {
            _repoEstados = repoEstados;
            _repoCidades = repoCidades;
            _repoBairros = repoBairros;
        }
        #endregion

        #region Implementation of IRepositorioEnderecos
        public IList<Endereco> Todos(Loja loja)
        {
            return SessaoAtual.Query<Endereco>()
                              .Where(p => p.Loja.Equals(loja))
                              .ToList();
        }

        public IList<Endereco> Todos(long idLoja)
        {
            return SessaoAtual.Query<Endereco>()
                              .Where(p => p.Loja != null && p.Loja.Id == idLoja)
                              .ToList();
        }

        public Endereco Get(long idLoja, string logradouro, string numero, string complemento, long idBairro)
        {
            return
                SessaoAtual.Query<Endereco>().SingleOrDefault(p => p.Loja.Id == idLoja && p.Logradouro == logradouro && p.Numero == numero &&
                                                                   p.Complemento == complemento && p.Bairro.Id == idBairro);
        }

        public List<Endereco> GetPorLoja(long idLoja)
        {
            return SessaoAtual.Query<Endereco>().Where(p => p.Loja.Id == idLoja).ToList();
        }

        public Endereco Get(long idEndereco)
        {
            return SessaoAtual.Query<Endereco>().FirstOrDefault(p => p.Id == idEndereco);
        }

        #endregion

        #region Atributos Privados
        private readonly IRepositorioEstados _repoEstados;
        private readonly IRepositorioCidades _repoCidades;
        private readonly IRepositorioBairros _repoBairros;
        #endregion
    }
}