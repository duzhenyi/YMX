using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Security;

namespace D.YMX.Utils
{
    public class TlsFingerprint
    {
        public static string GetFingerprint(string hostname, int port)
        {
            TlsClientProtocol protocol = new TlsClientProtocol(new SecureRandom());

            var tls = new DefaultTlsClient()
            {
                // 设置TLS版本
                ClientVersion = ProtocolVersion.TLSv12,
                // 设置证书验证方式
                CertificateVerifyer = new AlwaysValidVerifyer(),
                // 设置SNI
                ServerName = new ServerName(HostNameType.Dns, hostname),
                // 设置支持的密码套件
                CipherSuites = new int[] {
                    CipherSuite.TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256,
                    CipherSuite.TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384,
                    CipherSuite.TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256,
                    CipherSuite.TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA384,
                    CipherSuite.TLS_RSA_WITH_AES_128_GCM_SHA256,
                    CipherSuite.TLS_RSA_WITH_AES_256_GCM_SHA384,
                    CipherSuite.TLS_RSA_WITH_AES_128_CBC_SHA256,
                    CipherSuite.TLS_RSA_WITH_AES_256_CBC_SHA256
                }
            };

            protocol.Connect(tls);

            // 获取服务器证书
            Certificate serverCert = protocol.Connection.ServerCertificate;

            // 计算SHA256指纹
            byte[] fingerprint = DigestUtilities.CalculateDigest("SHA-256", serverCert.GetEncoded());

            // 转换为十六进制字符串
            return BitConverter.ToString(fingerprint).Replace("-", "").ToLower();
        }

    }
}
