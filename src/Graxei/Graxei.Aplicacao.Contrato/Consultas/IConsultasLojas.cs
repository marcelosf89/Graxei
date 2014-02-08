using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasLojas
    {
        Loja Get(int id);
        IList<Endereco> EnderecosRepetidos(Loja loja);
    }
}
