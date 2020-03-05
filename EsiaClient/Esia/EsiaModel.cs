using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Security.Cryptography.Pkcs;
using Microsoft.AspNetCore.Authentication;

namespace EsiaClient.Esia
{
    public class EsiaModel
    {
        public string EsiaHost { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
        public string ResponseType { get; set; }
        public string State { get; set; }
        public string TimeStamp { get; set; }
        public string AccessType { get; set; }
        
        public EsiaModel()
        {
            EsiaHost = "https://esia.gosuslugi.ru/aas/oauth2/ac";
            ClientId = "";
            Scope = "openid fullname snils email mobile";
            ClientSecret = CreateClientSecret();
            State = Guid.NewGuid().ToString();
            RedirectUri = "https://localhost:5601/home/GetEsiaResponse";
            ResponseType = "code";//"token"; //code
            TimeStamp = DateTime.UtcNow.ToString("yyyy.MM.dd HH:mm:ss +0000");
            AccessType = "offline"; //online
        }

        public string GetEsiaUri()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(EsiaHost);
            stringBuilder.AppendFormat($"?client_id={Uri.EscapeDataString(ClientId)}");
            stringBuilder.AppendFormat($"&client_secret={Uri.EscapeDataString(CreateClientSecret())}");
            stringBuilder.AppendFormat($"&redirect_uri={Uri.EscapeDataString(RedirectUri)}");
            stringBuilder.AppendFormat($"&scope={Uri.EscapeDataString(Scope)}");
            stringBuilder.AppendFormat($"&response_type={Uri.EscapeDataString(ResponseType)}");
            stringBuilder.AppendFormat($"&state={Uri.EscapeDataString(State)}");
            stringBuilder.AppendFormat($"&timestamp={Uri.EscapeDataString(TimeStamp)}");
            stringBuilder.AppendFormat($"&access_type={Uri.EscapeDataString(AccessType)}");

            return stringBuilder.ToString();
        }
        
        private string CreateClientSecret()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.OpenExistingOnly);
            var signCert = store.Certificates.Find(X509FindType.FindByThumbprint, "", false)[0];
            store.Close();

            var bytes = Encoding.UTF8.GetBytes(string.Format($"{Scope}{TimeStamp}{ClientId}{State}"));

            var signResult = SignBytes(bytes, signCert);

            return Base64UrlTextEncoder.Encode(signResult);
        }

        private byte[] SignBytes(byte[] bytes, X509Certificate2 signCert)
        {
            var signedCms = new SignedCms(new ContentInfo(bytes));
            signedCms.ComputeSignature(new CmsSigner(signCert));

            return signedCms.Encode();
        }
    }
}