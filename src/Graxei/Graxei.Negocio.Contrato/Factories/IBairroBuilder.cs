using Graxei.Modelo;
using System;
namespace Graxei.Negocio.Contrato.Factories
{
    public interface IBairroBuilder
    {
        Bairro Build();
        IBairroBuilder SetCidade(string cidade);
        IBairroBuilder SetIdEstado(long id);
        IBairroBuilder SetNome(string nome);
        void Validar();
    }
}
