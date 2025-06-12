using Entities.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace TestProjectAPI
{
    [TestClass]
    public class Test1
    {

        public static string Token {  get; set; }

         
        [TestMethod]
        public void TestMethod1()
        {
            var result = ChamaApiPost("urlApi").Result;

            var listaMessage = JsonConvert.DeserializeObject<Message[]>(result).ToList();

            Assert.IsTrue(listaMessage.Any());
        }

        public void GetToken()
        {
            string urlApiGeraToken = "UrlApiToken";

            using (var cliente = new HttpClient())
            {
                string login = "string.Empty";
                string senhha = "string.Empty";

                var dados = new
                {
                    email = login,
                    senha = senhha,
                    cpf = string.Empty,
                };

                string JsonObjeto = JsonConvert.SerializeObject(dados);
                var content = new StringContent(JsonObjeto, Encoding.UTF8, "aplication/json");
                
                var resultado = cliente.PostAsync(urlApiGeraToken, content);

                resultado = cliente.PostAsync(urlApiGeraToken, content);
                resultado.Wait();
                if (resultado.Result.IsSuccessStatusCode)
                {
                    var tokenJson = resultado.Result.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();

                }
            }
        }

        public string ChamaApiGet(string url)
        {
            GetToken();
            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Clear();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = cliente.GetStringAsync(url);

                    return response.Result;
                }
            }

            return null;
        }

        public async Task<string> ChamaApiPost(string url, object dados = null)
        {
            string JsonObjeto = dados != null ? JsonConvert.SerializeObject(dados) : "";
            var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");

            GetToken();

            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = client.PostAsync(url, content);
                    response.Wait();

                    if (response.Result.IsSuccessStatusCode)
                    {
                        var retorno = await response.Result.Content.ReadAsStringAsync();

                        return retorno;
                    }
                }
            }

            return null;
        }
    }
}
