using System;
using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Models
{
    public class NovosEnderecosModel
    {
        public IList<EnderecoIndiceModel> Enderecos
        {
            get
            {
                List<EnderecoIndiceModel> retorno = new List<EnderecoIndiceModel>();
                retorno.AddRange(_enderecos);
                return retorno;
            }
        }

        public void AdicionarEndereco(EnderecoIndiceModel endereco)
        {
            int ultimoId = _enderecos.Count;
            // TODO: criar exceção do Graxei no MVC4
            if (endereco.Endereco == null)
            {
                throw new Exception("Endereço deve ser adicionado");
            }
            endereco.IdLista = ultimoId;
            _enderecos.Add(endereco);
        }

        public void RemoverEndereco(int idLista)
        {
            _enderecos.RemoveAll(p => p.IdLista == idLista);
        }
        private List<EnderecoIndiceModel> _enderecos = new List<EnderecoIndiceModel>();
    }
}