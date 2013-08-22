using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using System.IO;
using System.Collections;
using iTextSharp.text.pdf;
using Org.BouncyCastle.X509;

namespace FAST.Security
{
            /// <summary>
        /// This class hold the certificate and extract private key needed for e-signature 
        /// </summary>
        public class Certificate
        {
            #region Attributes

            private String fileCertificate;
            private String passwordCertificate;
            private AsymmetricKeyParameter akp;
            private Org.BouncyCastle.X509.X509Certificate[] chain;

            #endregion

            #region Accessors
            public Org.BouncyCastle.X509.X509Certificate[] Chain
            {
                get { return chain; }
            }
            public AsymmetricKeyParameter Akp
            {
                get { return akp; }
            }

            #endregion

            #region Helpers

            private void processCert()
            {
                string alias = null;
                Pkcs12Store pk12;

                //First we'll read the certificate file
                using (FileStream file = new FileStream(this.fileCertificate, FileMode.Open, FileAccess.Read))
                {

                    pk12 = new Pkcs12Store(file, this.passwordCertificate.ToCharArray());

                    //then Iterate throught certificate entries to find the private key entry
                    IEnumerator i = pk12.Aliases.GetEnumerator();
                    while (i.MoveNext())
                    {
                        alias = ((string)i.Current);
                        if (pk12.IsKeyEntry(alias))
                            break;
                    }

                    this.akp = pk12.GetKey(alias).Key;
                    X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
                    this.chain = new Org.BouncyCastle.X509.X509Certificate[ce.Length];
                    for (int k = 0; k < ce.Length; ++k)
                        chain[k] = ce[k].Certificate;
                }
            }

            public void VerifyFull(PdfReader reader)
            {
                AcroFields af = reader.AcroFields;
                List<String> names = af.GetSignatureNames();
                foreach (String name in names)
                {
                    PdfPKCS7 pk = af.VerifySignature(name);
                    DateTime cal = pk.SignDate;
                    X509Certificate[] pkc = pk.Certificates;

                    Object[] fails = PdfPKCS7.VerifyCertificates(pkc, this.Chain.ToList(), null, cal);
                    if (fails != null)
                    {
                        throw new Exception("FAIL");
                    }

                    if (!pk.Verify())
                    {
                        throw new Exception("FAIL");
                    }
                }
            }

            public static void Verify(PdfReader reader)
            {
                AcroFields af = reader.AcroFields;
                List<String> names = af.GetSignatureNames();
                foreach (String name in names)
                {
                    PdfPKCS7 pk = af.VerifySignature(name);
                    DateTime cal = pk.SignDate;
                    X509Certificate[] pkc = pk.Certificates;

                    if (!pk.Verify())
                    {
                        throw new Exception("FAIL");
                    }
                }
            }
            #endregion

            #region Constructors
            public Certificate( String fileCertificate, String passwordCertificate)
            {
                this.fileCertificate = fileCertificate;
                this.passwordCertificate = passwordCertificate;
                this.processCert();
            }
            #endregion

        }
}

