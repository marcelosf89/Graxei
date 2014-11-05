using System.Collections.Generic;
using Graxei.Modelo;
namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioLojas : IRepositorioIrrestrito<Loja>
    {
        Loja Get(string nome);
        List<Usuario> GetUsuarios(Loja loja);
        List<Usuario> GetUsuarios(long idLoja);
        Loja GetComEnderecos(long id); 
    }
}
