using Graxei.Modelo;
namespace Graxei.Negocio.Contrato.Factories
{
    public interface IBairrosBuilder
    {
        Bairro Build();
        IBairrosBuilder SetCidade(string cidade);
        IBairrosBuilder SetIdEstado(long id);
        IBairrosBuilder SetNome(string nome);
        void Validar();
    }
}
