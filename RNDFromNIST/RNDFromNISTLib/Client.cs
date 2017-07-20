using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RNDFromNISTLib
{
    public class Client
    {
        #region Properties



        #endregion

        

        public Record GetCurrent(long timestamp)
        {
            var url = "https://beacon.nist.gov/rest/record/" + timestamp;

            return Get(url);
        }

        public Record GetPrevious(long timestamp)
        {
            var url = "https://beacon.nist.gov/rest/record/previous/" + timestamp;

            return Get(url);
        }

        public Record GetNext(long timestamp)
        {
            var url = "https://beacon.nist.gov/rest/record/next/" + timestamp;

            return Get(url);
        }

        public Record GetLast()
        {
            var url = "https://beacon.nist.gov/rest/record/last";

            return Get(url);
        }

        public Record GetStartChain(long timestamp)
        {
            var url = "https://beacon.nist.gov/rest/record/start-chain/" + timestamp;

            return Get(url);
        }

        public Record Get(string url)
        { 
            var record = new Record();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Method = "GET";

                var r = request.GetResponse();
                var xDoc = new System.Xml.XmlDocument();
                xDoc.Load(r.GetResponseStream());

                var ns = new System.Xml.XmlNamespaceManager(xDoc.NameTable);
                ns.AddNamespace("rcrd", "http://beacon.nist.gov/record/0.1/");
                var recordNode = xDoc.SelectSingleNode("/rcrd:record", ns);

                record.Version = GetElmStr(recordNode, ns, "version");
                record.Frequency = GetElmInt(recordNode, ns, "frequency");
                record.TimeStamp = GetElmLong(recordNode, ns, "timeStamp");
                record.SeedValue = GetElmStr(recordNode, ns, "seedValue");
                record.PreviousOutputValue = GetElmStr(recordNode, ns, "previousOutputValue");
                record.SignatureValue = GetElmStr(recordNode, ns, "signatureValue");
                record.OutputValue = GetElmStr(recordNode, ns, "outputValue");
                
                record.StatusCode = GetElmStr(recordNode, ns, "statusCode");
            }
            catch(Exception)
            {
                
            }
            

            return record;
        }


        #region Internal Methods

        protected string GetElmStr(System.Xml.XmlNode recordNode, System.Xml.XmlNamespaceManager ns, string q)
        {
            var t = "rcrd:" + q;

            var node = recordNode.SelectSingleNode(t, ns);
            if (node != null)
            {
                return node.InnerText;
            }
            return "";
        }

        protected int GetElmInt(System.Xml.XmlNode recordNode, System.Xml.XmlNamespaceManager ns, string q)
        {
            var str = GetElmStr(recordNode, ns, q);
            int i = 0;
            int.TryParse(str, out i);
            return i;
        }

        protected long GetElmLong(System.Xml.XmlNode recordNode, System.Xml.XmlNamespaceManager ns, string q)
        {
            var str = GetElmStr(recordNode, ns, q);
            long i = 0;
            long.TryParse(str, out i);
            return i;
        }

        #endregion


        #region Constructor(s)

        public Client()
        {
            
        }

        #endregion
    }
}
