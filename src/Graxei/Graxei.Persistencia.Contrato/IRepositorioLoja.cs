using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Contrato
{
    public class IRepositorioLoja : IRepositorioEntidades<Loja>
    {
        Loja Get(string nome);
    }
}
