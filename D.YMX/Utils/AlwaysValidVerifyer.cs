using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.YMX.Utils
{
    // 证书验证器，总是返回验证通过
    public class AlwaysValidVerifyer : ICertificateVerifyer
    {
        //string fingerprint = TlsFingerprint.GetFingerprint("www.google.com", 443);
        public bool IsValid(Uri targetUri, X509CertificateStructure[] certs)
        {
            return true;
        }
    }
}
