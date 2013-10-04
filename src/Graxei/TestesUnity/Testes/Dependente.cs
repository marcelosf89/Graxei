namespace TestesUnity.Testes
{
    public class Dependente : IDependente
    {
        private IAgregador _agregador;

        public Dependente(IAgregador agregador)
        {
            _agregador = agregador;
        }

        public string ExibirValor()
        {
            return _agregador.Valor.ToString();
        }
    }
}