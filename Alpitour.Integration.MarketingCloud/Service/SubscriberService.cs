using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Alpitour.Integration.MarketingCloud.Model.Subscriber;
using Newtonsoft.Json;
using RestSharp;
using ServiceReference1;

namespace Alpitour.Integration.MarketingCloud.Service
{
    public class SubscriberService : BaseService
    {
        //https://mcqtvlg3hv4lyvz2nhqb0-r5jspq.soap.marketingcloudapis.com/Service.asmx
        public async Task<string> FindSubscriber(SubscriberRequest request)
        {
            var soapString = ConstructSoapRequest(request.Email, request.Access_Token);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("SOAPAction", "Retrieve");
                var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                using (var response = await client.PostAsync(request.URI, content))
                {
                    var soapResponse = await response.Content.ReadAsStringAsync();
                    return ParseSoapResponse(soapResponse);
                }
            }

        }


        private string ConstructSoapRequest(string email, string access_token)
        {
            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                    <soapenv:Header>
                        <fueloauth>{access_token}</fueloauth>
                    </soapenv:Header>
                    <soapenv:Body>
                        <RetrieveRequestMsg xmlns=""http://exacttarget.com/wsdl/partnerAPI"">
                            <RetrieveRequest>
                                <ClientIDs>
                                    <ClientID>510001496</ClientID>
                                </ClientIDs>
                                <ObjectType>DataExtensionObject[TransactionalMap]</ObjectType>
                                <Properties>SubscriberKey</Properties>
                                <Properties>Email</Properties>
                                <Filter xsi:type=""SimpleFilterPart"">
                                    <Property>Email</Property>
                                    <SimpleOperator>equals</SimpleOperator>
                                    <Value>{email}</Value>
                                </Filter>
                            </RetrieveRequest>
                        </RetrieveRequestMsg>
                    </soapenv:Body>
                </soapenv:Envelope>";

        }

        private string ParseSoapResponse(string response)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);
            XmlNodeList parentNode = xmlDoc.GetElementsByTagName("Properties");
            if (parentNode.Count <= 0)
                return string.Empty;
            foreach (XmlNode childrenNode in parentNode)
            {
                return Regex.Match(childrenNode.InnerText.ToString(), @"SubscriberKey(.+?)Email").Groups[1].Value;

            }
            return string.Empty;
        }


    }
}




//var soap = XDocument.Parse(response);
//var rawRetriveMsg = soap.Descendants("RetrieveResponseMsg").FirstOrDefault();
//if (rawRetriveMsg == null)
//    throw new InvalidOperationException("No RetriveResponseMsgFound");

// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(RetrieveResponseMsg));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (RetrieveResponseMsg)serializer.Deserialize(reader);
// }

//XmlDocument xmlDoc = new XmlDocument();
//xmlDoc.LoadXml(response);
//XmlNodeList parentNode = xmlDoc.GetElementsByTagName("soap:Envelope");
//foreach (XmlNode childrenNode in parentNode)
//{
//    ServiceReference1.RetrieveResponse responseMsg;
//    var serializer = new XmlSerializer(typeof(ServiceReference1.RetrieveResponse));
//    var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>" + childrenNode.InnerXml;
//    using (TextReader reader = new StringReader(xml))
//    {
//        responseMsg = (ServiceReference1.RetrieveResponse)serializer.Deserialize(reader);
//    }
//    return responseMsg.RetrieveResponseMsg;
//}
//XNamespace ns = "http://exacttarget.com/wsdl/partnerAP/";
//var result = soap.Descendants(ns + "RetrieveResponseMsg").FirstOrDefault().Value;
//return result;







//XmlSerializer serializer = new XmlSerializer(typeof(RetrieveResponse));
//using (StringReader reader = new StringReader(response))
//{
//    var test = (RetrieveResponse)serializer.Deserialize(reader);
//}



//XmlDocument xmlDoc = new XmlDocument();
//xmlDoc.LoadXml(response);
//XmlNodeList parentNode = xmlDoc.GetElementsByTagName("soap:Body");
//foreach (XmlNode childrenNode in parentNode)
//{
//    var xml =  childrenNode.InnerXml;
//    XmlSerializer serializer = new XmlSerializer(typeof(RetrieveResponse));
//    using (StringReader reader = new StringReader(xml))
//    {
//        var test = (RetrieveResponse)serializer.Deserialize(reader);
//    }
//}


//XmlDocument xmlDoc = new XmlDocument();
//xmlDoc.LoadXml(response);
//XmlNodeList parentNode = xmlDoc.GetElementsByTagName("Property");

//foreach (XmlNode childrenNode in parentNode)
//{
//   if(child)
//}


//var client = new RestClient("https://mcqtvlg3hv4lyvz2nhqb0-r5jspq.soap.marketingcloudapis.com/Service.asmx");
//client.Timeout = -1;
//var request = new RestRequest(Method.POST);
//request.AddHeader("SOAPAction", "Retrieve");
//request.AddHeader("Content-Type", "text/xml");
//request.AddParameter("text/xml", "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n    <soapenv:Header>\r\n        <fueloauth>eyJhbGciOiJIUzI1NiIsImtpZCI6IjQiLCJ2ZXIiOiIxIiwidHlwIjoiSldUIn0.eyJhY2Nlc3NfdG9rZW4iOiJYa1BzMmJ3V2g0SFVhd1RGdXJGYm52SjAiLCJjbGllbnRfaWQiOiIwY3g1dXd4YXFtbnVqbDRweG15ZzEyNmgiLCJlaWQiOjUxMDAwMTQ5Niwic3RhY2tfa2V5IjoiUzUwIiwicGxhdGZvcm1fdmVyc2lvbiI6MiwiY2xpZW50X3R5cGUiOiJTZXJ2ZXJUb1NlcnZlciJ9.qv5kOEJJeClovS-4qkWbHYY_N2MuEaA5hkEpfJ81VBo.f72b2FmcOtzK9pQmh4QIyTSEg2kuFIX1_MRwyqaRYG8mIyTZbeqgP2RYwQ55VtbxlF8sINuFlGo4ptyUK282OZYl_2NgLltK5EME1VN3e6XufjZGZUCUoI-6ocYNCxP0Ioh3VFZykWUD4TuXgb8XwSo2pq058NRWkF_Upo2VPNgvD7LK_yd</fueloauth>\r\n    </soapenv:Header>\r\n    <soapenv:Body>\r\n        <RetrieveRequestMsg xmlns=\"http://exacttarget.com/wsdl/partnerAPI\">\r\n            <RetrieveRequest>\r\n                <ClientIDs>\r\n                    <ClientID>510001496</ClientID>\r\n                </ClientIDs>\r\n                <ObjectType>DataExtensionObject[TransactionalMap]</ObjectType>\r\n                <Properties>SubscriberKey</Properties>\r\n                <Properties>Email</Properties>\r\n                <Filter xsi:type=\"SimpleFilterPart\">\r\n                    <Property>Email</Property>\r\n                    <SimpleOperator>equals</SimpleOperator>\r\n                    <Value>gtessitore@atlantic-technologies.com</Value>\r\n                </Filter>\r\n            </RetrieveRequest>\r\n        </RetrieveRequestMsg>\r\n    </soapenv:Body>\r\n</soapenv:Envelope>", ParameterType.RequestBody);
//IRestResponse response = client.Execute(request);
//Console.WriteLine(response.Content);














