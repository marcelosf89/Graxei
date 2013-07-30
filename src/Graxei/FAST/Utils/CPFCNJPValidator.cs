using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FAST.Utils
{
    /// <summary>
    /// Classe estática para validar CPF e CNPJ
    /// </summary>
    public static class CPFCNJPValidator
    {
        #region Public Static Methods
        /// <summary>
        /// Valida CPF com ou sem formatação
        /// </summary>
        /// <param name="cpf">CPF com ou sem formatação</param>
        /// <returns>True ou False</returns>
        /// <exception cref="ArgumentNullException">CPF nulo ou vazio</exception>
        /// <exception cref="ArgumentException">CPF com formato incorreto</exception>
        public static bool CPFValidator(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                throw new ArgumentNullException("cpf", "O CPF não pode ser nulo nem vazio");
            }
            Regex regex = new Regex(@"^\d{11}|(\d{3}\.\d{3}\.\d{3}-\d{2})$");
            if (!regex.IsMatch(cpf))
            {
                throw new ArgumentException("O formato do CPF está incorreto", "cpf");
            }
            if (cpf.Length == 14)
            {
                cpf = cpf.Substring(0, 3) + cpf.Substring(4, 3) + cpf.Substring(8, 3) + cpf.Substring(12, 2);
            }
            int digitoVerificador = Convert.ToInt32(cpf.Substring(9, 2), CultureInfo.CurrentCulture);
            int digitoVerificador1 = 0;
            int digitoVerificador2 = 0;
            for (int it = 0; it < 9; it++)
            {
                int cpfDigito = Convert.ToInt32(cpf[it] - '0');
                digitoVerificador1 += (it + 1) * cpfDigito;
                digitoVerificador2 += it * cpfDigito;
            }
            digitoVerificador1 %= 11;
            digitoVerificador1 = digitoVerificador1 == 10 ? 0 : digitoVerificador1;
            digitoVerificador2 += digitoVerificador1 * 9;
            digitoVerificador2 %= 11;
            digitoVerificador2 = digitoVerificador2 == 10 ? 0 : digitoVerificador2;
            return digitoVerificador == digitoVerificador1 * 10 + digitoVerificador2;
        }

        /// <summary>
        /// Valida CNPJ com ou sem formatação
        /// </summary>
        /// <param name="cnpj">CNPJ com ou sem formatação</param>
        /// <returns>True ou False</returns>
        /// <exception cref="ArgumentNullException">CNPJ nulo ou vazio</exception>
        /// <exception cref="ArgumentException">CNPJ com formato incorreto</exception>
        public static bool CNPJValidator(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                throw new ArgumentNullException("cnpj", "O CNPJ não pode ser nulo nem vazio");
            }
            Regex regex = new Regex(@"^\d{14}|(\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2})$");
            if (!regex.IsMatch(cnpj))
            {
                throw new ArgumentException("O formato do CNPJ está incorreto", "cnpj");
            }
            if (cnpj.Length == 18)
            {
                cnpj = cnpj.Substring(0, 2) + cnpj.Substring(3, 3) + cnpj.Substring(7, 3) + cnpj.Substring(11, 4) + cnpj.Substring(16, 2);
            }
            int digitoVerificador = Convert.ToInt32(cnpj.Substring(12, 2), CultureInfo.CurrentCulture);
            int digitoVerificador1 = 0;
            int digitoVerificador2 = 0;
            for (int it1 = 0, it2 = 5; it1 < 12; it1++)
            {
                int cnpjDigito = Convert.ToInt32(cnpj[it1] - '0');
                digitoVerificador2 += it2 * cnpjDigito;
                digitoVerificador1 += (it2 = ++it2 == 10 ? 2 : it2) * cnpjDigito;
            }
            digitoVerificador1 %= 11;
            digitoVerificador1 = digitoVerificador1 == 10 ? 0 : digitoVerificador1;
            digitoVerificador2 += digitoVerificador1 * 9;
            digitoVerificador2 %= 11;
            digitoVerificador2 = digitoVerificador2 == 10 ? 0 : digitoVerificador2;
            return digitoVerificador == digitoVerificador1 * 10 + digitoVerificador2;
        }
        #endregion
    }
}
