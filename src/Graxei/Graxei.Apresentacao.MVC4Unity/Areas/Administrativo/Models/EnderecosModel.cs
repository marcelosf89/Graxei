using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    public class EnderecosModel
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

        public void AdicionarEndereco(EnderecoIndiceModel endereco, int posicao)
        {
            int ultimoId = _enderecos.Count;
            // TODO: criar exceção do Graxei no MVC4
            if (endereco.Endereco == null)
            {
                throw new Exception("Endereço deve ser adicionado");
            }
            endereco.IdLista = ultimoId;
            _enderecos.Insert(posicao, endereco);
        }

        public void RemoverEndereco(long idLista)
        {
            _enderecos.RemoveAll(p => p.IdLista == idLista);
            ReordenarIndicesLista(_enderecos);
        }

        private void ReordenarIndicesLista(List<EnderecoIndiceModel> enderecos)
        {
            int i = 0;
            foreach (EnderecoIndiceModel end in enderecos.OrderBy(p => p.IdLista))
            {
                end.IdLista = i++;
            }
        }

        public void SubstituirEndereco(EnderecoIndiceModel enderecoAnterior, EnderecoIndiceModel novoEndereco)
        {
            RemoverEndereco(enderecoAnterior.IdLista);
            AdicionarEndereco(novoEndereco, (int)novoEndereco.IdLista);
        }

        public void SubstituirEndereco(EnderecoIndiceModel novoEndereco)
        {
            RemoverEndereco(novoEndereco.IdLista);
            AdicionarEndereco(novoEndereco, (int)novoEndereco.IdLista);
        }
        private List<EnderecoIndiceModel> _enderecos = new List<EnderecoIndiceModel>();
    }
}