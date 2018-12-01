using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FourHealth.Domain.Entities;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Proxy
    {

        public static ServiceBroker.WebService1SoapClient  client = new ServiceBroker.WebService1SoapClient();

        public static Beneficiario ConsultarBeneficiarioPorId(int id)
        {
            try
            {
                string ApiBaseUrl;
                ApiBaseUrl = client.Broker_BuscarServidor();
                
                string MetodoPath = "/api/Beneficiario/" + id.ToString(); //caminho do método a ser chamado

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiBaseUrl + MetodoPath);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var valor = streamReader.ReadToEnd();
                    if (valor.Substring(1, 1) == "[")
                    {
                        var retorno = JsonConvert.DeserializeObject<List<Beneficiario>>(valor);
                        return retorno.FirstOrDefault();
                    }
                    else
                    {
                        var retorno = JsonConvert.DeserializeObject<Beneficiario>(valor);
                        return retorno;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public static string IncluirBeneficiario(Beneficiario beneficiario)
        {
            try
            {
                string ApiBaseUrl;
                ApiBaseUrl = client.Broker_BuscarServidor();
                string MetodoPath = "/api/Beneficiario/"; //caminho do método a ser chamado

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiBaseUrl + MetodoPath);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(beneficiario));
                    streamWriter.Flush();
                    streamWriter.Close();
                }


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var valor = streamReader.ReadToEnd();

                    return valor.ToString();

                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
    }
}
