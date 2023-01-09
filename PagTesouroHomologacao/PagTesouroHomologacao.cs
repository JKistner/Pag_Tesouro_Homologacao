using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Http;

namespace PagTesouroHomologacao
{
    public class PagTesouroHomologacao
    {
        private string PagTesouro(string Doc, string Nome, string Valor)
        {
            try
            {
                //Homologação
                string token = "Sua_Chave_Token";
                string Url = "https://valpagtesouro.tesouro.gov.br/api/gru/solicitacao-pagamento";

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var requestBody = new
                {
                    codigoServico = "",
                    referencia = "",
                    competencia = "",
                    vencimento = "",
                    cnpjCpf = Doc,
                    nomeContribuinte = Nome,
                    valorPrincipal = Valor,
                    valorDescontos = "",
                    valorOutrasDeducoes = "",
                    valorMulta = "",
                    valorJuros = "",
                    valorOutrosAcrescimos = "",
                    urlRetorno = "",
                    urlNotificacao = "",
                    modoNavegacao = "2",
                    expiracaoPix = ""
                };
                var requestJson = JsonConvert.SerializeObject(requestBody);

                var request = new HttpRequestMessage(HttpMethod.Post, Url)
                {
                    Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
                };

                var response = client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    dynamic responseData = JsonConvert.DeserializeObject(responseContent);

                    string Link = responseData.proximaUrl;
                    return Link;
                }

            }
            catch
            {
                return "";
            }
            return "";
        }
    }
}