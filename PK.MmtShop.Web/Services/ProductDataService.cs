using PK.MmtShop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PK.MmtShop.Web.Models;
using IMapper = AutoMapper.IMapper;

namespace PK.MmtShop.Web.Services
{
    public class ProductDataService : IProductDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductDataService(HttpClient httpClient,
            IMapper mapper,
            IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            var apiConfig = _configuration.GetSection("ProductApi");
            var apiCall = apiConfig["DeleteItem"];
            apiCall = apiCall.Replace("{productid}", productId.ToString());

            var responses = await _httpClient.DeleteAsync(apiCall);

            return responses.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var list = new List<ProductModel>();
            var apiConfig = _configuration.GetSection("ProductsApi");
            var apiCall = apiConfig["GetAll"];

            var results = await JsonSerializer.DeserializeAsync<IEnumerable<ProductDto>>(
                await _httpClient.GetStreamAsync(apiCall),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (results == null)
                return Enumerable.Empty<ProductModel>();
            
            results.ToList().ForEach(item => { list.Add(_mapper.Map<ProductModel>(item)); });
            
            return list.AsEnumerable();
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsByCategoryAsync(int categoryId)
        {
            var list = new List<ProductModel>();
            var apiConfig = _configuration.GetSection("ProductsApi");
            var apiCall = apiConfig["GetAllByCategory"];
            apiCall = apiCall.Replace("{categoryId}", categoryId.ToString());

            var results = await JsonSerializer.DeserializeAsync<IEnumerable<ProductDto>>(
                await _httpClient.GetStreamAsync(apiCall),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
            );

            if (results == null)
                return Enumerable.Empty<ProductModel>();
            results.ToList().ForEach(item => list.Add(_mapper.Map<ProductModel>(item)));

            return list.AsEnumerable();
        }

        public async Task<int> GetNextSkuByCategoryAsync(int categoryId)
        {
            var apiConfig = _configuration.GetSection("ProductsApi");
            var apiCall = apiConfig["GetNextSku"];
            apiCall = apiCall.Replace("{categoryId}", categoryId.ToString());

            var result = await JsonSerializer.DeserializeAsync<int>(
                await _httpClient.GetStreamAsync(apiCall),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
            );

            return result;
        }

        public async Task<ProductModel> GetProductByIdAsync(Guid productId)
        {
            var apiConfig = _configuration.GetSection("ProductsApi");
            var apiCall = apiConfig["GetSingle"];
            apiCall = apiCall.Replace("{categoryId}", productId.ToString());

            var result = await JsonSerializer.DeserializeAsync<ProductDto>(
                await _httpClient.GetStreamAsync(apiCall),
                    new JsonSerializerOptions() {PropertyNameCaseInsensitive = true }
            );

            if (result == null)
                return null;
            var product = _mapper.Map<ProductModel>(result);

            return product;
        }

        public async Task<ProductModel> InsertProductAsync(ProductDto product)
        {
            var apiConfig = _configuration.GetSection("ProductsApi");
            var apiCall = apiConfig["InsertNewOne"];
            
            var productAsJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8
                , "application/json");
            var response = await _httpClient.PostAsync(apiCall, productAsJson);

            if (response.IsSuccessStatusCode)
                return await JsonSerializer.DeserializeAsync<ProductModel>(await response.Content.ReadAsStreamAsync());

            return null;
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            var apiConfig = _configuration.GetSection("ProductsApi");
            var apiCall = apiConfig["UpdateItem"];

            var productAsJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8
                , "application/json");
            await _httpClient.PutAsync(apiCall, productAsJson);
        }

    }
}
