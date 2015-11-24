using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ecommerce.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Ecommerce.Request
{
    public class RequestTask
    {

        protected readonly string _endpoint;        

        public RequestTask(string endpoint)
        {
            _endpoint = endpoint;
        }

        private async Task<string> ConsumirServicoRest_Post<T>(T header, string uri = "")
        {
            Resultado retErro = new Resultado();
            try
            {
                using (var cliente = new HttpClient())
                {
                    System.Net.ServicePointManager.Expect100Continue = false;
                    cliente.BaseAddress = new Uri(_endpoint);                    
                    cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                   
                    var response = cliente.PostAsJsonAsync(uri, header).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        retErro.erro = true;
                        retErro.mensagem = response.RequestMessage.ToString();
                        return JsonConvert.SerializeObject(retErro);
                    }
                }
            }
            catch (Exception exception)
            {
                retErro.erro = true;
                retErro.mensagem = exception.Message;
                return JsonConvert.SerializeObject(retErro);
            }
        }

        public async Task<string> ConsumirServicoRest_Get()
        {
            Resultado retErro = new Resultado();
            try
            {
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(_endpoint);
                var response = cliente.GetAsync(string.Empty, HttpCompletionOption.ResponseHeadersRead).Result;
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    retErro.erro = true;
                    retErro.mensagem = response.RequestMessage.ToString();
                    return JsonConvert.SerializeObject(retErro);
                }

            }
            catch (Exception exception)
            {
                retErro.erro = true;
                retErro.mensagem = exception.Message;
                return JsonConvert.SerializeObject(retErro);
            }
        }

        public async Task<string> ConsumirServicoRest_Put<T>(T header, string uri = "")
        {
            Resultado retErro = new Resultado();
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(_endpoint);                    
                    var response = cliente.PutAsJsonAsync(uri, header).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        retErro.erro = true;
                        retErro.mensagem = response.RequestMessage.ToString();
                        return JsonConvert.SerializeObject(retErro);
                    }
                }
            }
            catch (Exception exception)
            {
                retErro.erro = true;
                retErro.mensagem = exception.Message;
                return JsonConvert.SerializeObject(retErro);
            }
        }

        public async Task<string> ConsumirServicoRest_Delete<T>(T header, string uri = "")
        {
            Resultado retErro = new Resultado();
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(_endpoint);
                    var response = cliente.DeleteAsync(uri).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        retErro.erro = true;
                        retErro.mensagem = response.RequestMessage.ToString();
                        return JsonConvert.SerializeObject(retErro);
                    }
                }
            }
            catch (Exception exception)
            {
                retErro.erro = true;
                retErro.mensagem = exception.Message;
                return JsonConvert.SerializeObject(retErro);
            }
        }

        public Resultado CadastrarCliente(Cliente cliente, string uri = "") {
            Resultado retorno = new Resultado();
            try
            {
                string ret = Task.Run(() => ConsumirServicoRest_Post(cliente, uri)).Result;
                retorno = JsonConvert.DeserializeObject<Resultado>(ret);
            }
            catch (Exception exception)
            {
                retorno.erro = true;
                retorno.mensagem = exception.Message;
            }            
            return retorno;            
        }

        public Resultado BuscarClientes()
        {
            Resultado retorno = new Resultado();
            try
            {
                string ret = Task.Run(() => ConsumirServicoRest_Get()).Result;
                retorno = JsonConvert.DeserializeObject<Resultado>(ret);
            }
            catch (Exception exception)
            {
                retorno.erro = true;
                retorno.mensagem = exception.Message;
            }
            return retorno;
        }

        public Resultado EditarCliente(Cliente cliente, string uri = "")
        {
            Resultado retorno = new Resultado();
            try
            {
                string ret = Task.Run(() => ConsumirServicoRest_Put(cliente, uri)).Result;
                retorno = JsonConvert.DeserializeObject<Resultado>(ret);
            }
            catch (Exception exception)
            {
                retorno.erro = true;
                retorno.mensagem = exception.Message;
            }
            return retorno;
        }

        public Resultado DeleteCliente(string uri = "")
        {
            Resultado retorno = new Resultado();
            try
            {
                string ret = Task.Run(() => ConsumirServicoRest_Delete(uri)).Result;
                retorno = JsonConvert.DeserializeObject<Resultado>(ret);
            }
            catch (Exception exception)
            {
                retorno.erro = true;
                retorno.mensagem = exception.Message;
            }
            return retorno;
        }

    }
}