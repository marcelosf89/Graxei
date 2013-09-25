using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Models
{
    public class LojaNovosEnderecosModel
    {
        public Loja Loja { get; set; }
        public IList<ItemListaNovosEnderecosModel> NovosEnderecoModel { get; set; }

    }
}