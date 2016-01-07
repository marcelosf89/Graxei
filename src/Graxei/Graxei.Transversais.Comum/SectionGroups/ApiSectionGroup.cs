using System.Configuration;

namespace Graxei.Transversais.Comum.SectionGroups
{
    public class ApiSectionGroup : ConfigurationSection
    {
        [ConfigurationProperty(ConstServidor, IsRequired = true)]
        public string Servidor
        {
            get
            {
                return this[ConstServidor].ToString();
            }
            set
            {
                this[ConstServidor] = value;
            }
        }

        [ConfigurationProperty(ConstRotas, IsRequired = true)]
        public NameValueConfigurationCollection Rotas
        {
            get
            {
                return (NameValueConfigurationCollection)this[ConstRotas];
            }
            set
            {
                this[ConstRotas] = value;
            }
        }

        public string GetRotaTratandoBarraNoInicio(string chave)
        {
            string retorno = Rotas[chave].Value;
            if (!retorno.StartsWith("/"))
            {
                retorno = "/" + retorno;
            }

            return retorno;
        }

        private const string ConstServidor = "servidor";

        private const string ConstRotas = "rotas";
    }
}
