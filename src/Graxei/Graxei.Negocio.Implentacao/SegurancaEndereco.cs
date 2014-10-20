using System;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;

namespace Graxei.Negocio.Contrato
{
    public class SegurancaEndereco : ISegurancaEndereco
    {
        public SegurancaEndereco(IRepositorioEnderecos repositorioEnderecos)
        {
            _repositorioEnderecos = repositorioEnderecos;
        }

        public bool PermitidoAlterar(Endereco endereco, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        private IRepositorioEnderecos _repositorioEnderecos;
    }
}