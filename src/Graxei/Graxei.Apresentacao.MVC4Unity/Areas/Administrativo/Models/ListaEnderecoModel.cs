using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    /// <summary>
    /// Contrato de dados para cadastro de Endereços
    /// </summary>
    public class ListaEnderecoModel
    {
        public List<Graxei.Transversais.ContratosDeDados.EnderecoListaContrato> Enderecos { get; set; }

        public long IdLoja { get; set; }
        public long QuantidadeEndereco { get; set; }
    }
}
