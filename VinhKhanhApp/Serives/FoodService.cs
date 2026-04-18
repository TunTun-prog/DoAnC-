using System.Net.Http.Json;
using VinhKhanhApp.Models;

namespace VinhKhanhApp.Services;

public class FoodService
{
    HttpClient client = new HttpClient();

    string baseUrl = "http://192.168.1.10:5000/api/food";

    public async Task<List<FoodPlace>> GetFoods()
    {
        try
        {
            var data = await client.GetFromJsonAsync<List<FoodPlace>>(baseUrl);
            return data ?? new List<FoodPlace>();
        }
        catch
        {
            return new List<FoodPlace>();
        }
    }
}