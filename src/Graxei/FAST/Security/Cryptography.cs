using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FAST.Security
{
    /// <summary>
    /// Classe para tratamento de segurança da informação
    /// </summary>
    public static class Cryptography
    {
        #region Private Static Methods
        /// <summary>
        /// Converte um array de bytes em uma string hexadecimal
        /// </summary>
        /// <param name="input">Array de bytes</param>
        /// <returns>Representação em hexadecimal do array de bytes</returns>
        private static string toHexadecimalString(byte[] input)
        {
            StringBuilder hexadecimal = new StringBuilder();
            for (int it = 0; it < input.Length; it++)
            {
                hexadecimal.Append(input[it].ToString("x2", CultureInfo.CurrentCulture));
            }
            return hexadecimal.ToString();
        }

        /// <summary>
        /// Converte uma string em hexadecimal em um array de bytes
        /// </summary>
        /// <param name="input">String hexadecimal</param>
        /// <returns>Array de bytes da string hexadecimal</returns>
        private static byte[] toByteArray(string input)
        {
            int arraySize = input.Length / 2;
            byte[] byteArray = new byte[arraySize];
            for (int it = 0; it < arraySize; it++)
            {
                byteArray[it] = Convert.ToByte(input.Substring(it * 2, 2), 16);
            }
            return byteArray;
        }
        #endregion

        #region Public Static Methods

        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        // Function to Generate a 128 bits Key.
        public static string GenerateKey(SizeCryptography size)
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.                
            SymmetricAlgorithm desCrypto;
            switch (size)
            {
                case SizeCryptography.Size64:
                    desCrypto = new DESCryptoServiceProvider();
                    break;
                case SizeCryptography.Size128:
                    desCrypto = new RijndaelManaged();
                    desCrypto.KeySize = 128;
                    break;
                case SizeCryptography.Size256:
                    desCrypto = new RijndaelManaged();
                    break;
                default:
                    throw new Exception("This encryption length is not set");
            }
            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        /// <summary>
        /// Retorna a representação em MD5 da string
        /// </summary>
        /// <param name="input">String de entrada</param>
        /// <returns>MD5 em hexadecimal com 32 caracteres da string de entrada</returns>
        /// <exception cref="ArgumentNullException">Argumento de entrada nulo</exception>
        public static string HasherMD5(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "A string de entrada não pode ser nula");
            }
            byte[] inputBytes = Encoding.Default.GetBytes(input);
            MD5 md5 = MD5.Create();
            return toHexadecimalString(md5.ComputeHash(inputBytes));
        }

        /// <summary>
        /// Retorna a representação em SHA384 da string
        /// </summary>
        /// <param name="input">String de entrada</param>
        /// <returns>SHA384 em hexadecimal com 96 caracteres da string de entrada</returns>
        /// <exception cref="ArgumentNullException">Argumento de entrada nulo</exception>
        public static string HasherSha384(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "A string de entrada não pode ser nula");
            }
            byte[] inputBytes = Encoding.Default.GetBytes(input);
            SHA384 sha384 = SHA384.Create();
            return toHexadecimalString(sha384.ComputeHash(inputBytes));
        }

        /// <summary>
        /// Encriptografa uma string usando uma chave
        /// </summary>
        /// <param name="input">String de entrada</param>
        /// <param name="key">Chave para ser usada no processo de encriptação</param>
        /// <returns>String encriptografada com a chave</returns>
        /// <exception cref="ArgumentNullException">Argumento de entrada nulo</exception>
        public static string EncryptRijndael(string input, string key)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "A string de entrada não pode ser nula");
            }
            if (key == null)
            {
                throw new ArgumentNullException("key", "A chave não pode ser nula");
            }
            byte[] keyHashedSHA384 = toByteArray(HasherSha384(key));
            byte[] keyPartHashedSHA384 = new byte[32];
            byte[] ivPartHashedSHA384 = new byte[16];
            Array.Copy(keyHashedSHA384, 0, keyPartHashedSHA384, 0, 32);
            Array.Copy(keyHashedSHA384, 32, ivPartHashedSHA384, 0, 16);
            byte[] inputBytes = Encoding.Default.GetBytes(input);
            Rijndael rijndael = Rijndael.Create();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(keyPartHashedSHA384, ivPartHashedSHA384), CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();
            return toHexadecimalString(memoryStream.ToArray());
        }

        /// <summary>
        /// Descriptografa uma string usando uma chave
        /// </summary>
        /// <param name="input">String encriptografada</param>
        /// <param name="key">Chave para ser usada no processo de descriptação</param>
        /// <returns>String descriptografada com a chave</returns>
        /// <exception cref="ArgumentNullException">Argumento de entrada nulo</exception>
        public static string DecryptRijndael(string input, string key)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "A string de entrada não pode ser nula");
            }
            if (key == null)
            {
                throw new ArgumentNullException("key", "A chave não pode ser nula");
            }
            byte[] keyHashedSHA384 = toByteArray(HasherSha384(key));
            byte[] keyPartHashedSHA384 = new byte[32];
            byte[] ivPartHashedSHA384 = new byte[16];
            Array.Copy(keyHashedSHA384, 0, keyPartHashedSHA384, 0, 32);
            Array.Copy(keyHashedSHA384, 32, ivPartHashedSHA384, 0, 16);
            byte[] inputBytes = toByteArray(input);
            Rijndael rijndael = Rijndael.Create();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(keyPartHashedSHA384, ivPartHashedSHA384), CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }

        //  Call this function to remove the key from memory after use for security
        public static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey, SizeCryptography size)
        {
            using (FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read))
            {

                using (FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write))
                {

                    SymmetricAlgorithm DES;
                    switch (size)
                    {
                        case SizeCryptography.Size64:
                            DES = new DESCryptoServiceProvider();
                            break;
                        case SizeCryptography.Size128:
                            DES = new RijndaelManaged();
                            DES.KeySize = 128;
                            break;
                        case SizeCryptography.Size256:
                            DES = new RijndaelManaged();
                            break;
                        default:
                            throw new Exception("This encryption length is not set");
                    }
                    DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                    DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                    ICryptoTransform desencrypt = DES.CreateEncryptor();
                    using (CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write))
                    {
                        byte[] bytearrayinput = new byte[fsInput.Length];
                        fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                        cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                    }
                }
            }
        }

        public static void DecryptFile(string sInputFilename, string sOutputFilename, string sKey, SizeCryptography size)
        {
            SymmetricAlgorithm DES;
            switch (size)
            {
                case SizeCryptography.Size64:
                    DES = new DESCryptoServiceProvider();
                    break;
                case SizeCryptography.Size128:
                    DES = new RijndaelManaged();
                    DES.KeySize = 128;
                    break;
                case SizeCryptography.Size256:
                    DES = new RijndaelManaged();
                    break;
                default:
                    throw new Exception("This encryption length is not set");
            }

            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            using (Stream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read))
            {
                //Create a DES decryptor from the DES instance.
                ICryptoTransform desdecrypt = DES.CreateDecryptor();
                //Create crypto stream set to read and do a 
                //DES decryption transform on incoming bytes.
                using (CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read))
                {
                    //Print the contents of the decrypted file.
                    using (StreamWriter fsDecrypted = new StreamWriter(sOutputFilename))
                    {
                        fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
                        fsDecrypted.Flush();
                    }
                }
            }
        }
        #endregion




    }
    public enum SizeCryptography
    {
        Size64 = 1,
        Size128 = 2,
        Size256 = 3
    }
}
