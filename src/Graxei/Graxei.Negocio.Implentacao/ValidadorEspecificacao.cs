namespace Graxei.Negocio.Implementacao
{
    public class ValidadorEspecificacao
    {
        public ValidadorEspecificacao(Resultado resultado, string mensagem)
        {
            _resultado = resultado;
            _mensagem = mensagem;
        }

        public override string ToString()
        {
            if (_resultado == Resultado.OK)
            {
                return "OK";
            }
            return string.Format("Falha: {0}", _mensagem);
        }

        public string Mensagem
        {
            get { return _mensagem;  }
        }
        public bool Ok()
        {
            return _resultado == Resultado.OK;
        }
        private Resultado _resultado;
        private string _mensagem;
    }

    public enum Resultado { OK, Falha }
}
