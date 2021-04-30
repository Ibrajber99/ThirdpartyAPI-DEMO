using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ThirdPartyAPI_NET_Core.Models;

namespace ThirdPartyAPI_NET_Core.NASAClient
{
    public class NASAPicAPIClient : HttpClient
    {
        private readonly string BasePath;

        private readonly string MEDIA_TYPE;

        private readonly string APIKEY;

        public NASAPicAPIClient
            (string baseAddress, string basePath,string mediaType,string apiKey)
        {
            BaseAddress = new Uri(baseAddress);
            BasePath = basePath;
            MEDIA_TYPE = mediaType;
            APIKEY = apiKey;
        }

        public async Task<PicOfTheDay> Get()
        {
            try
            {
                SetupHeaders();
        
                var response = await GetAsync(BasePath + $"?api_key={APIKEY}");
        
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
        
                    var returnModel = JsonConvert.DeserializeObject
                        <PicOfTheDay>(result);
        
                    return returnModel;
                }
                else
                {
                    throw new Exception
                        ($"Failed to retrieve items returned {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        protected virtual void SetupHeaders()
        {
            DefaultRequestHeaders.Clear();

            //Define request data format  
            DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue
                (MEDIA_TYPE));
        }

    }
}
