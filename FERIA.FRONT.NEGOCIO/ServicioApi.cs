using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioApi
    {
        private RestClient client = new RestClient(ApiUrl);
        static private string apiUrl = ConfigurationManager.AppSettings.Get("ApiNegocio");
        static private string sdkVersion = "CALEME-NET-SDK-1.0.0";
        static public string ApiUrl
        {
            get
            {
                return apiUrl;
            }
            set
            {
                apiUrl = value;
            }
        }
        public ServicioApi()
        {
        }

        #region "Sincronos"
        public IRestResponse Get(string resourse)
        {
            return Get(resourse, new List<Parameter>());
        }

        public IRestResponse Get(string resourse, List<Parameter> param)
        {

            var request = new RestRequest(resourse, Method.GET);
            List<string> names = new List<string>();
            foreach (Parameter p in param)
            {
                names.Add(p.Name + "={" + p.Name + "}");
                p.Type = ParameterType.UrlSegment;
                request.AddParameter(p);
            }

            request.Resource = resourse + "?" + String.Join("&", names.ToArray());

            request.AddHeader("Accept", "application/json");

            var response = ExecuteRequest(request);

            return response;
        }
        public IRestResponse Post(string resource, List<Parameter> param, object body)
        {
           

            var request = new RestRequest(resource, Method.POST);
            List<string> names = new List<string>();
            foreach (Parameter p in param)
            {
                names.Add(p.Name + "={" + p.Name + "}");
                p.Type = ParameterType.UrlSegment;
                request.AddParameter(p);
            }

            request.Resource = resource + "?" + String.Join("&", names.ToArray());

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            request.AddBody(body);

            var response = ExecuteRequest(request);



            return response;
        }

        public IRestResponse Put(string resource, List<Parameter> param, object body)
        {
           

            var request = new RestRequest(resource, Method.PUT);
            List<string> names = new List<string>();
            foreach (Parameter p in param)
            {
                names.Add(p.Name + "={" + p.Name + "}");
                p.Type = ParameterType.UrlSegment;
                request.AddParameter(p);
            }

            request.Resource = resource + "?" + String.Join("&", names.ToArray());

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            request.AddBody(body);

            var response = ExecuteRequest(request);



            return response;
        }

        public IRestResponse Delete(string resource, List<Parameter> param)
        {
     
            var request = new RestRequest(resource, Method.DELETE);
            List<string> names = new List<string>();
            foreach (Parameter p in param)
            {
                names.Add(p.Name + "={" + p.Name + "}");
               p.Type = ParameterType.UrlSegment;
                request.AddParameter(p);
            }

            request.Resource = resource + "?" + String.Join("&", names.ToArray());

            request.AddHeader("Accept", "application/json");

            var response = ExecuteRequest(request);


            return response;
        }

        private IRestResponse ExecuteRequest(RestRequest request)
        {
            client.UserAgent = sdkVersion;
            return client.Execute(request);
        }
        #endregion 

        #region "ASINCRONOS"
        public async Task<IRestResponse> GetAsync(string resourse)
        {
            return await GetAsync(resourse, new List<Parameter>());
        }

        public async Task<IRestResponse> GetAsync(string resourse, List<Parameter> param)
        {
            var request = new RestRequest(resourse, Method.GET);
            List<string> names = new List<string>();
            foreach (Parameter p in param)
            {
                names.Add(p.Name + "={" + p.Name + "}");
                p.Type = ParameterType.UrlSegment;
                request.AddParameter(p);
            }

            request.Resource = resourse + "?" + String.Join("&", names.ToArray());

            request.AddHeader("Accept", "application/json");

            var response = await ExecuteRequestAsync(request);

            return response;
        }
        public async Task<IRestResponse> PostAsync(string resource, List<Parameter> param, object body)
        {
            var request = new RestRequest(resource, Method.POST);
            List<string> names = new List<string>();
            foreach (Parameter p in param)
            {
                names.Add(p.Name + "={" + p.Name + "}");               
                p.Type = ParameterType.UrlSegment;
                request.AddParameter(p);
            }

            request.Resource = resource + "?" + String.Join("&", names.ToArray());

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            request.AddBody(body);

            var response = await ExecuteRequestAsync(request);



            return response;
        }

        public async Task<IRestResponse> PutAsync(string resource, List<Parameter> param, object body)
        {
            var request = new RestRequest(resource, Method.PUT);
            List<string> names = new List<string>();
            foreach (Parameter p in param)
            {
                names.Add(p.Name + "={" + p.Name + "}");
                p.Type = ParameterType.UrlSegment;
                request.AddParameter(p);
            }

            request.Resource = resource + "?" + String.Join("&", names.ToArray());

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            request.AddBody(body);

            var response = await ExecuteRequestAsync(request);



            return response;
        }

        public async Task<IRestResponse> DeleteAsync(string resource, List<Parameter> param)
        {
            var request = new RestRequest(resource, Method.DELETE);
            List<string> names = new List<string>();
            foreach (Parameter p in param)
            {
                names.Add(p.Name + "={" + p.Name + "}");
                p.Type = ParameterType.UrlSegment;
                request.AddParameter(p);
            }

            request.Resource = resource + "?" + String.Join("&", names.ToArray());

            request.AddHeader("Accept", "application/json");

            var response = await ExecuteRequestAsync(request);


            return response;
        }

        private async Task<IRestResponse> ExecuteRequestAsync(RestRequest request)
        {
            client.UserAgent = sdkVersion;
            return await client.ExecuteTaskAsync(request);
        }
        #endregion
    }
}
namespace RestSharp
{
    public static class RestClientExtensions
    {
        public static Task<IRestResponse> ExecuteTaskAsync(this RestClient @this, RestRequest request)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<IRestResponse>();

            @this.ExecuteAsync(request, (response) =>
            {
                if (response.ErrorException != null)
                    tcs.TrySetException(response.ErrorException);
                else
                    tcs.TrySetResult(response);
            });

            return tcs.Task;
        }
    }
}
