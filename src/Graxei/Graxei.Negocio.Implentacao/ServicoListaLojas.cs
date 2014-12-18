using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoListaLojas : IServicoListaLojas
    {
        private IGerenciadorAutenticacao _gerenciadorAutenticacao;
        private IRepositorioListaLojas _repositorioListaLojas;

        public ServicoListaLojas(IRepositorioListaLojas repositorioListaLojas,
            IGerenciadorAutenticacao gerenciadorAutenticacao)
        {
            _repositorioListaLojas = repositorioListaLojas;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
        }

        public ListaLojas Get(int pagina, int tamanhoPagina)
        {
            Usuario usuario = _gerenciadorAutenticacao.Get();
            return _repositorioListaLojas.Get(pagina, tamanhoPagina, usuario);
        }
    }
}