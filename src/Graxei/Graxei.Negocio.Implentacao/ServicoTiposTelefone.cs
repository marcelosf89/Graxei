using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoTiposTelefone : IServicoTiposTelefone
    {
        public ServicoTiposTelefone(IRepositorioTiposTelefone repositorioTiposTelefone)
        {
            _repositorioTiposTelefone = repositorioTiposTelefone;
        }
        public TipoTelefone Get(string tipo)
        {
            return _repositorioTiposTelefone.Get(tipo);
        }

        private IRepositorioTiposTelefone _repositorioTiposTelefone;
    }
}