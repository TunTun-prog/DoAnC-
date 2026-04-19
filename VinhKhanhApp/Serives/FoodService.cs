using VinhKhanhApp.Models;

namespace VinhKhanhApp.Services;

public class FoodService
{
    public async Task<List<FoodPlace>> GetFoods()
    {
        await Task.Delay(100);

        return new List<FoodPlace>()
        {
            new FoodPlace{
                Name="Ốc Oanh",
                Address="534 Vĩnh Khánh, Quận 4",
                Description="Quán ốc nổi tiếng với nhiều món như ốc hương, sò điệp, nghêu hấp sả. Hương vị đậm đà, giá bình dân.",
                Latitude=10.7593,
                Longitude=106.7045,
                Rating=4.6
            },
            new FoodPlace{
                Name="Ốc Đào",
                Address="212 Vĩnh Khánh, Quận 4",
                Description="Ốc tươi, chế biến nhanh, nổi bật với sốt bơ tỏi và trứng muối. Phù hợp ăn tối cùng bạn bè.",
                Latitude=10.7602,
                Longitude=106.7052,
                Rating=4.7
            },
            new FoodPlace{
                Name="Bún bò Huế Cô Ba",
                Address="45 Vĩnh Khánh, Quận 4",
                Description="Nước lèo đậm đà, thơm mùi sả, thịt bò mềm, chả cua ngon. Phù hợp ăn sáng và tối.",
                Latitude=10.7589,
                Longitude=106.7040,
                Rating=4.5
            },
            new FoodPlace{
                Name="Sinh tố Bé Mười",
                Address="300 Vĩnh Khánh, Quận 4",
                Description="Sinh tố trái cây tươi, vị ngọt thanh, giá rẻ, thích hợp ngồi chill.",
                Latitude=10.7598,
                Longitude=106.7058,
                Rating=4.3
            }
        };
    }
}