using PK.MmtShop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PK.MmtShop.Web.Models;
using IMapper = AutoMapper.IMapper;

namespace PK.MmtShop.Web.Services
{
    public class CategoryDataService : ICategoryDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public CategoryDataService(HttpClient htpClient,
            IMapper mapper,
            IConfiguration configuration)
        {
            _httpClient = htpClient ?? throw new ArgumentNullException(nameof(htpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriiesAsync()
        {
            var list = new List<CategoryModel>();
            var apiCallConfig = _configuration.GetSection("CategoryApis");
            var apiCall = apiCallConfig["GetAll"];

            var results = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryDto>>(
                await _httpClient.GetStreamAsync(apiCall), new JsonSerializerOptions() 
                    {PropertyNameCaseInsensitive = true } );

            if (results == null)
                return Enumerable.Empty<CategoryModel>();

            results.ToList().ForEach(cat => list.Add(_mapper.Map<CategoryModel>(cat)));

            return list.AsEnumerable();
        }

        public async Task<IEnumerable<CategoryRangeModel>> GetAllCategoryRanges()
        {
            var list = new List<CategoryRangeModel>();
            var apiCallConfig = _configuration.GetSection("CategoryRangesApi");
            var apiCall = apiCallConfig["GetAll"];
            
            var results = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryRangeDto>>(
                await _httpClient.GetStreamAsync(apiCall), new JsonSerializerOptions() 
                    {PropertyNameCaseInsensitive = true } );

            if (results == null)
                return Enumerable.Empty<CategoryRangeModel>();
            results.ToList().ForEach(item =>
            {
                list.Add(_mapper.Map<CategoryRangeModel>(item));
            });

            return list.AsEnumerable();
        }

        public async Task<CategoryModel> GetCategoryAsync(int categoryId)
        {
            var apiCallConfig = _configuration.GetSection("CategoryApis");
            var apiCall = apiCallConfig["GetSingle"];
            apiCall = apiCall.Replace("{categoryid}", categoryId.ToString());

            var result = await JsonSerializer.DeserializeAsync<CategoryDto>(
                await _httpClient.GetStreamAsync(apiCall),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
            if (result == null)
                return null;
            var category = _mapper.Map<CategoryModel>(result);

            return category;
        }

        public async Task<CategoryRangeModel> GetCategoryRange(int categoryId)
        {
           var apiCallConfig = _configuration.GetSection("CategoryRangesApi");
            var apiCall = apiCallConfig["GetByCategory"];
            apiCall = apiCall.Replace("{categoryid}", categoryId.ToString());

            var result = await JsonSerializer.DeserializeAsync<CategoryRangeDto>(
                await _httpClient.GetStreamAsync(apiCall),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
            );

            if (result == null)
                return null;

            var range = _mapper.Map<CategoryRangeModel>(result);

            return range;
        }
    }
}
