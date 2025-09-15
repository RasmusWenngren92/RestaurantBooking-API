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
            //_context.MenuItems.RemoveRange(_context.MenuItems);
            //await _context.SaveChangesAsync();
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
    ImageUrl = "https://plus.unsplash.com/premium_photo-1661431423340-fa30b97312bc?q=80&w=1332&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
    IsPopular = true
},

// Main Dishes (10 items)
new MenuItem
{
    Name = "Tonkotsu Ramen",
    Description = "Rich pork bone broth with vegetables, chashu pork, and green onions",
    Price = 160.00m,
    Category = "Main Course",
    ImageUrl = "https://plus.unsplash.com/premium_photo-1723669629687-0e618541c49e?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8VG9ua290c3UlMjBSYW1lbnxlbnwwfHwwfHx8MA%3D%3D",
    IsPopular = true
},
new MenuItem
{
    Name = "Vegetarian Miso Ramen",
    Description = "Hearty miso broth with warm delicious vegetables and noodles",
    Price = 150.00m,
    Category = "Main Course",
    ImageUrl = "https://images.unsplash.com/photo-1606663765399-5179e954a3a0?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
    IsPopular = false
},
new MenuItem
{
    Name = "Shoyu Ramen",
    Description = "Clear soy sauce based broth with bamboo shoots, soft-boiled egg, and nori",
    Price = 155.00m,
    Category = "Main Course",
    ImageUrl = "https://plus.unsplash.com/premium_photo-1694628644916-2ce35cdedbd7?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8U2hveXUlMjBSYW1lbnxlbnwwfHwwfHx8MA%3D%3D",
    IsPopular = true
},
new MenuItem
{
    Name = "Spicy Miso Ramen",
    Description = "Rich miso broth with chili paste, ground pork, and bean sprouts",
    Price = 165.00m,
    Category = "Main Course",
    ImageUrl = "https://images.unsplash.com/photo-1742633882713-593c13e90231?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8U3BpY3klMjBtaXNvfGVufDB8fDB8fHww",
    IsPopular = true
},
new MenuItem
{
    Name = "Shio Ramen",
    Description = "Light salt-based clear broth with chicken, wakame seaweed, and menma",
    Price = 148.00m,
    Category = "Main Course",
    ImageUrl = "https://images.unsplash.com/photo-1680593180878-e0cd1e99486e?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTR8fFJhbWVuJTIwY2hpY2tlbnxlbnwwfHwwfHx8MA%3D%3D",
    IsPopular = false
},
new MenuItem
{
    Name = "Tantanmen",
    Description = "Japanese sesame and chili ramen with ground pork and bok choy",
    Price = 168.00m,
    Category = "Main Course",
    ImageUrl = "https://images.unsplash.com/photo-1637024698421-533d83c7b883?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8VGFudGFubWVufGVufDB8fDB8fHww",
    IsPopular = false
},
new MenuItem
{
    Name = "Chicken Paitan Ramen",
    Description = "Creamy chicken bone broth with tender chicken chashu and soft egg",
    Price = 158.00m,
    Category = "Main Course",
    ImageUrl = "https://images.unsplash.com/photo-1730613998619-d3d7fb3cea71?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OHx8UmFtZW4lMjBDaGlja2VufGVufDB8fDB8fHww",
    IsPopular = false
},
new MenuItem
{
    Name = "Tsukemen",
    Description = "Thick noodles served with concentrated tonkotsu dipping broth",
    Price = 172.00m,
    Category = "Main Course",
    ImageUrl = "https://images.unsplash.com/photo-1632440722549-692fc6af969e?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fFJhbWVufGVufDB8fDB8fHww",
    IsPopular = true
},
new MenuItem
{
    Name = "Seafood Ramen",
    Description = "Rich seafood broth with shrimp, scallops, and nori seaweed",
    Price = 175.00m,
    Category = "Main Course",
    ImageUrl = "https://images.unsplash.com/photo-1702737940705-ee75ee0481e6?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8U2VhZm9vZCUyMFJhbWVufGVufDB8fDB8fHww",
    IsPopular = false
},
new MenuItem
{
    Name = "Chicken Teriyaki Bowl",
    Description = "Grilled teriyaki chicken over steamed rice with vegetables",
    Price = 145.00m,
    Category = "Main Course",
    ImageUrl = "https://plus.unsplash.com/premium_photo-1695167739750-a1e7c856438b?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Q2hpY2tlbiUyMFRlcml5YWtpfGVufDB8fDB8fHww",
    IsPopular = false
},

// Desserts (3 items)
new MenuItem
{
    Name = "Mochi Ice Cream",
    Description = "Sweet rice dough filled with creamy ice cream (vanilla, strawberry, or green tea)",
    Price = 48.00m,
    Category = "Dessert",
    ImageUrl = "https://images.unsplash.com/photo-1635355347994-b79177b77e5c?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8TW9jaGl8ZW58MHx8MHx8fDA%3D",
    IsPopular = true
},
new MenuItem
{
    Name = "Dorayaki",
    Description = "Fluffy pancake sandwich filled with sweet red bean paste",
    Price = 52.00m,
    Category = "Dessert",
    ImageUrl = "https://images.unsplash.com/photo-1626497132810-f38eb29c5385?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8RG9yYXlha2l8ZW58MHx8MHx8fDA%3D",
    IsPopular = false
},
new MenuItem
{
    Name = "Matcha Cheesecake",
    Description = "Creamy cheesecake infused with premium Japanese green tea powder",
    Price = 58.00m,
    Category = "Dessert",
    ImageUrl = "https://plus.unsplash.com/premium_photo-1694599324074-d5479407e7c7?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8TWF0Y2hhJTIwQ2hlZXNlY2FrZXxlbnwwfHwwfHx8MA%3D%3D",
    IsPopular = true
},

// Beverages (6 items - mix of alcoholic and non-alcoholic)
new MenuItem
{
    Name = "Asahi Beer",
    Description = "Classic Japanese lager beer, crisp and refreshing",
    Price = 45.00m,
    Category = "Beverage",
    ImageUrl = "https://images.unsplash.com/photo-1552853041-59e6f459f83b?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8QXNhaGklMjBCZWVyfGVufDB8fDB8fHww",
    IsPopular = true
},
new MenuItem
{
    Name = "Sake (Hot or Cold)",
    Description = "Traditional Japanese rice wine served at your preferred temperature",
    Price = 65.00m,
    Category = "Beverage",
    ImageUrl = "https://images.unsplash.com/photo-1571762450239-f0f047321444?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8U2FrZXxlbnwwfHwwfHx8MA%3D%3D",
    IsPopular = true
},
new MenuItem
{
    Name = "Matcha Latte",
    Description = "Ceremonial grade matcha powder whisked with steamed milk",
    Price = 42.00m,
    Category = "Beverage",
    ImageUrl = "https://images.unsplash.com/photo-1575487426366-079595af2247?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fE1hdGNoYSUyMExhdHRlfGVufDB8fDB8fHww",
    IsPopular = false
},
new MenuItem
{
    Name = "Sencha Green Tea",
    Description = "Premium Japanese green tea with a delicate, refreshing taste",
    Price = 35.00m,
    Category = "Beverage",
    ImageUrl = "https://images.unsplash.com/photo-1566221280196-43e76121ff51?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8U2VuY2hhJTIwR3JlZW4lMjBUZWF8ZW58MHx8MHx8fDA%3D",
    IsPopular = false
},
new MenuItem
{
    Name = "Plum Wine (Umeshu)",
    Description = "Sweet Japanese plum wine, perfect as an aperitif or dessert drink",
    Price = 58.00m,
    Category = "Beverage",
    ImageUrl = "https://images.unsplash.com/photo-1651330395599-bd00a1d0a213?q=80&w=1471&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
    IsPopular = false
},
new MenuItem
{
    Name = "Japanese Whisky Highball",
    Description = "Premium Japanese whisky with sparkling water and fresh lemon",
    Price = 72.00m,
    Category = "Beverage",
    ImageUrl = "https://plus.unsplash.com/premium_photo-1694630656076-00b1def5dae4?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTd8fEphcGFuZXNlJTIwV2hpc2t5JTIwSGlnaGJhbGx8ZW58MHx8MHx8fDA%3D",
    IsPopular = true
},
new MenuItem
{
    Name = "Edamame",
    Description = "Steamed and salted young soybeans, a classic Japanese appetizer.",
    Price = 55.00m,
    Category = "Starter",
    ImageUrl = "https://plus.unsplash.com/premium_photo-1666318300348-a4d0226d81ad?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8RWRhbWFtZXxlbnwwfHwwfHx8MA%3D%3D",
    IsPopular = false
},
new MenuItem
{
    Name = "Takoyaki",
    Description = "Crispy octopus balls topped with takoyaki sauce, mayo, and bonito flakes.",
    Price = 78.00m,
    Category = "Starter",
    ImageUrl = "https://images.unsplash.com/photo-1652752731860-ef0cf518f13a?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8VGFrb3lha2l8ZW58MHx8MHx8fDA%3D",
    IsPopular = true
},
new MenuItem
{
    Name = "Agedashi Tofu",
    Description = "Lightly fried soft tofu in a delicate dashi broth with grated daikon.",
    Price = 68.00m,
    Category = "Starter",
    ImageUrl = "https://images.unsplash.com/photo-1706468238718-bba7e9b63ad2?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTh8fEFnZWRhc2hpJTIwVG9mdXxlbnwwfHwwfHx8MA%3D%3D",
    IsPopular = false
},
new MenuItem
{
    Name = "Chicken Karaage",
    Description = "Japanese-style fried chicken pieces with a crispy coating and tangy sauce.",
    Price = 89.00m,
    Category = "Starter",
    ImageUrl = "https://images.unsplash.com/photo-1745914415360-353407fab30b?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
    IsPopular = true
}
            };
            _context.MenuItems.AddRange(menuItems);
            var result = await _context.SaveChangesAsync();
            return result; // Return the number of records added
        }
    }
}
