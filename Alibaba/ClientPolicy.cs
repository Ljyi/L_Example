using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba
{
    public class ClientPolicy
    {
        private string serverHost = "gw.open.1688.com";
        public string ServerHost
        {
            get { return serverHost; }
            set { serverHost = value; }
        }
        private int httpPort = 80;

        public int HttpPort
        {
            get { return httpPort; }
            set { httpPort = value; }
        }
        private int httpsPort = 443;

        public int HttpsPort
        {
            get { return httpsPort; }
            set { httpsPort = value; }
        }
        private string appKey;

        public string AppKey
        {
            get { return appKey; }
            set { appKey = value; }
        }
        private string secretKey;

        public string SecretKey
        {
            get { return secretKey; }
            set { secretKey = value; }
        }
        private int defaultTimeout = 5000;

        public int DefaultTimeout
        {
            get { return defaultTimeout; }
            set { defaultTimeout = value; }
        }
        private string defaultContentCharset = "UTF-8";

        public string DefaultContentCharset
        {
            get { return defaultContentCharset; }
            set { defaultContentCharset = value; }
        }
        private bool defaultUseHttps = false;

        public bool DefaultUseHttps
        {
            get { return defaultUseHttps; }
            set { defaultUseHttps = value; }
        }
    }
}
