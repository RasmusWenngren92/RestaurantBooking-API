using RestaurantBookingAPI.Services.IServices;
using RestaurantBookingAPI.Data;
using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Services
{
    public class MenuSeedService : IMenuSeedService
    {
        private readonly RestaurantDBContext _context;
       
        public MenuSeedService(RestaurantDBContext context)
        {
          
            _context = context;
        }

        public async Task<int> SeedMenu()
        {
            // Check if menu items already exist
            if (_context.MenuItems.Any())
            {
                return 0; // Menu items already seeded
            }
            var menuItems = new List<MenuItem>
            {
// Starters (5 items) - Swedish Krona pricing
// Complete Menu Items for Ramen Restaurant - Swedish Krona pricing

// Starters (5 items)
new MenuItem
{
    Name = "Gyoza (Pork Dumplings)",
    Description = "Pan-fried pork and vegetable dumplings served with a savory dipping sauce.",
    Price = 85.00m,
    Category = "Starter",
    ImageUrl = "https://example.com/images/gyoza.jpg",
    IsPopular = true
},

// Main Dishes (10 items)
new MenuItem
{
    Name = "Tonkotsu Ramen",
    Description = "Rich pork bone broth with vegetables, chashu pork, and green onions",
    Price = 160.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/tonkotsu_ramen.jpg",
    IsPopular = true
},
new MenuItem
{
    Name = "Vegetarian Miso Ramen",
    Description = "Hearty miso broth with warm delicious vegetables and noodles",
    Price = 150.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/miso_ramen.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Shoyu Ramen",
    Description = "Clear soy sauce based broth with bamboo shoots, soft-boiled egg, and nori",
    Price = 155.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/shoyu_ramen.jpg",
    IsPopular = true
},
new MenuItem
{
    Name = "Spicy Miso Ramen",
    Description = "Rich miso broth with chili paste, ground pork, and bean sprouts",
    Price = 165.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/spicy_miso_ramen.jpg",
    IsPopular = true
},
new MenuItem
{
    Name = "Shio Ramen",
    Description = "Light salt-based clear broth with chicken, wakame seaweed, and menma",
    Price = 148.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/shio_ramen.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Tantanmen",
    Description = "Japanese sesame and chili ramen with ground pork and bok choy",
    Price = 168.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/tantanmen.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Chicken Paitan Ramen",
    Description = "Creamy chicken bone broth with tender chicken chashu and soft egg",
    Price = 158.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/chicken_paitan.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Tsukemen",
    Description = "Thick noodles served with concentrated tonkotsu dipping broth",
    Price = 172.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/tsukemen.jpg",
    IsPopular = true
},
new MenuItem
{
    Name = "Seafood Ramen",
    Description = "Rich seafood broth with shrimp, scallops, and nori seaweed",
    Price = 175.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/seafood_ramen.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Chicken Teriyaki Bowl",
    Description = "Grilled teriyaki chicken over steamed rice with vegetables",
    Price = 145.00m,
    Category = "Main Course",
    ImageUrl = "https://example.com/images/teriyaki_bowl.jpg",
    IsPopular = false
},

// Desserts (3 items)
new MenuItem
{
    Name = "Mochi Ice Cream",
    Description = "Sweet rice dough filled with creamy ice cream (vanilla, strawberry, or green tea)",
    Price = 48.00m,
    Category = "Dessert",
    ImageUrl = "https://example.com/images/mochi_ice_cream.jpg",
    IsPopular = true
},
new MenuItem
{
    Name = "Dorayaki",
    Description = "Fluffy pancake sandwich filled with sweet red bean paste",
    Price = 52.00m,
    Category = "Dessert",
    ImageUrl = "https://example.com/images/dorayaki.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Matcha Cheesecake",
    Description = "Creamy cheesecake infused with premium Japanese green tea powder",
    Price = 58.00m,
    Category = "Dessert",
    ImageUrl = "https://example.com/images/matcha_cheesecake.jpg",
    IsPopular = true
},

// Beverages (6 items - mix of alcoholic and non-alcoholic)
new MenuItem
{
    Name = "Asahi Beer",
    Description = "Classic Japanese lager beer, crisp and refreshing",
    Price = 45.00m,
    Category = "Beverage",
    ImageUrl = "https://example.com/images/asahi_beer.jpg",
    IsPopular = true
},
new MenuItem
{
    Name = "Sake (Hot or Cold)",
    Description = "Traditional Japanese rice wine served at your preferred temperature",
    Price = 65.00m,
    Category = "Beverage",
    ImageUrl = "https://example.com/images/sake.jpg",
    IsPopular = true
},
new MenuItem
{
    Name = "Matcha Latte",
    Description = "Ceremonial grade matcha powder whisked with steamed milk",
    Price = 42.00m,
    Category = "Beverage",
    ImageUrl = "https://example.com/images/matcha_latte.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Sencha Green Tea",
    Description = "Premium Japanese green tea with a delicate, refreshing taste",
    Price = 35.00m,
    Category = "Beverage",
    ImageUrl = "https://example.com/images/sencha.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Plum Wine (Umeshu)",
    Description = "Sweet Japanese plum wine, perfect as an aperitif or dessert drink",
    Price = 58.00m,
    Category = "Beverage",
    ImageUrl = "https://example.com/images/umeshu.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Japanese Whisky Highball",
    Description = "Premium Japanese whisky with sparkling water and fresh lemon",
    Price = 72.00m,
    Category = "Beverage",
    ImageUrl = "https://example.com/images/whisky_highball.jpg",
    IsPopular = true
},
new MenuItem
{
    Name = "Edamame",
    Description = "Steamed and salted young soybeans, a classic Japanese appetizer.",
    Price = 55.00m,
    Category = "Starter",
    ImageUrl = "https://example.com/images/edamame.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Takoyaki",
    Description = "Crispy octopus balls topped with takoyaki sauce, mayo, and bonito flakes.",
    Price = 78.00m,
    Category = "Starter",
    ImageUrl = "https://example.com/images/takoyaki.jpg",
    IsPopular = true
},
new MenuItem
{
    Name = "Agedashi Tofu",
    Description = "Lightly fried soft tofu in a delicate dashi broth with grated daikon.",
    Price = 68.00m,
    Category = "Starter",
    ImageUrl = "https://example.com/images/agedashi_tofu.jpg",
    IsPopular = false
},
new MenuItem
{
    Name = "Chicken Karaage",
    Description = "Japanese-style fried chicken pieces with a crispy coating and tangy sauce.",
    Price = 89.00m,
    Category = "Starter",
    ImageUrl = "https://example.com/images/karaage.jpg",
    IsPopular = true
}
            };
            _context.MenuItems.AddRange(menuItems);
            var result = await _context.SaveChangesAsync();
            return result; // Return the number of records added
        }
    }
}
