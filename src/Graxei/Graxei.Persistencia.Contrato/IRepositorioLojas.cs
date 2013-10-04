using System.Collections.Generic;
using Graxei.Modelo;
namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioLojas : IRepositorioIrrestrito<Loja>
    {
        void Salvar(IList<LojaUsuario> lojasUsuarios);
        Loja Get(string nome);
    }
}
