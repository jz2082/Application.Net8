using InMemoryDb.DataEntity;

namespace Demo.InMemoryDb.Repository;

public static class DataSeedRepository 
{

    public static async Task DataSeedAsync(string databaseName)
    {
        var dbContext = new InMemoryDbContext(databaseName); 
        try
        {
            var houseList = new List<HouseEntity>() 
            {
                new () 
                {
                    Id = 1,
                    Address = "12 Valley of Kings, Geneva",
                    Country = "Switzerland",
                    Description = "A superb detached Victorian property on one of the town's finest roads, within easy reach of Barnes Villagx. The property has in excess of 6000 sq/ft of accommodation, a driveway and landscaped garden.",
                    Price = 900000
                },
                new ()
                {
                    Id = 2,
                    Address = "89 Road of Forks, Bern",
                    Country = "Switzerland",
                    Description = "This impressive family home, which dates back to approximately 1840, offers original period features throughout and is set back from the road with off street parking for up to six cars and an original Coach House, which has been incorporated into the main house to provide further accommodation. ",
                    Price = 500000
                },
                new ()
                {
                    Id = 3,
                    Address = "Grote Hof 12, Amsterdam",
                    Country = "The Netherlands",
                    Description = "This house has been designed and built to an impeccable standard offering luxurious and elegant living. The accommodation is arranged over four floors comprising a large entrance hall, living room with tall sash windows, dining room, study and WC on the ground floor.",
                    Price = 200500
                },
                new ()
                {
                    Id = 4,
                    Address = "Meel Kade 321, The Hague",
                    Country = "The Netherlands",
                    Description = "Discreetly situated a unique two storey period home enviably located on the corner of Krom Road and Recht Road offering seclusion and privacy. The house features a magnificent double height reception room with doors leading directly out onto a charming courtyard garden.",
                    Price = 259500
                },
                new ()
                {
                    Id = 5,
                    Address = "Oude Gracht 32, Utrecht",
                    Country = "The Netherlands",
                    Description = "This luxurious three bedroom flat is contemporary in style and benefits from the use of a gymnasium and a reserved underground parking spacx.",
                    Price = 400500
                }
            };
            await dbContext.Houses.AddRangeAsync(houseList).ConfigureAwait(false);
            var bidList = new List<BidEntity>()
            {
                new () { Id = 1, HouseId = 1, Bidder = "Sonia Reading", Amount = 200000 },
                new () { Id = 2, HouseId = 1, Bidder = "Dick Johnson", Amount = 202400 },
                new () { Id = 3, HouseId = 2, Bidder = "Mohammed Vahls", Amount = 302400 },
                new () { Id = 4, HouseId = 2, Bidder = "Jane Williams", Amount = 310500 },
                new () { Id = 5, HouseId = 2, Bidder = "John Kepler", Amount = 315400 },
                new () { Id = 6, HouseId = 3, Bidder = "Bill Mentor", Amount = 201000 },
                new () { Id = 7, HouseId = 4, Bidder = "Melissa Kirk", Amount = 410000 },
                new () { Id = 8, HouseId = 4, Bidder = "Scott Max", Amount = 450000 },
                new () { Id = 9, HouseId = 4, Bidder = "Christine James", Amount = 470000 },
                new () { Id = 10, HouseId = 5, Bidder = "Omesh Carim", Amount = 450000 },
                new () { Id = 11, HouseId = 5, Bidder = "Charlotte Max", Amount = 150000 },
                new () { Id = 12, HouseId = 5, Bidder = "Marcus Scott", Amount = 170000 }
            };
            await dbContext.Bids.AddRangeAsync(bidList).ConfigureAwait(false);
            var userList = new List<UserEntity>()
            {
                new() 
                {
                    Id = 3522,
                    Name = "Roland",
                    Password = "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=",
                    FavoriteColor = "blue",
                    Role = "Admin",
                    GoogleId = "101517359495305583936"
                },
                new()
                {
                    Id = 3523,
                    Name = "Test",
                    Password = "Test",
                    FavoriteColor = "blue",
                    Role = "Admin",
                    GoogleId = "101517359495305583937"
                }
            };
            await dbContext.Users.AddRangeAsync(userList).ConfigureAwait(false);
            var speakerList = new List<SpeakerEntity>()
            {
                new()
                {
                    Id = 1269,
                    FirstName = "Arun",
                    LastName = "Gupta",
                    Sat = false,
                    Sun = true,
                    Favorite = false,
                    Bio = "Arun Gupta is a Principal Open Source Technologist at Amazon Web Services. He has built and led developer communities for 12+ years at Sun, Oracle, Red Hat and Couchbasx.",
                    TwitterHandle = "arungupta",
                    UserBioShort = "Arun Gupta is a Principal Open Source Technologist at Amazon Web Services. ",
                    ImageUrl = "/images/Speaker-1269.jpg",
                    Email = "Arun.Gupta@codecamp.net"
                },
                new()  
                {
                    Id = 5996,
                    FirstName = "Craig",
                    LastName = "Berntson",
                    Sat = true,
                    Sun = true,
                    Favorite = true,
                    Bio = "Craig has a passion for community and helping other developers improve their skills. He writes the column \"Software Gardening\" in DotNet Curry Magazine and is the co-author of \"Continuous Integration in .NET\" available from Manning.",
                    Company = "HealthEquity",
                    TwitterHandle = "craigber",
                    UserBioShort = "Speaker, author, architect, and engineer. Currently he's a Senior Software Engineer at HealthEquity.",
                    ImageUrl = "/images/Speaker-5996.jpg",
                    Email = "Craig.Berntson@codecamp.net"
                },
                new() 
                {
                    Id = 187,
                    FirstName = "Dave",
                    LastName = "Nielsen",
                    Sat = true,
                    Sun = true,
                    Favorite = false,
                    Bio = "As Head of Ecosystem Programs, Dave uses emerging technologies and open source projects like Microservices, Serverless & Kubernetes to bring the magic of Redis to the broader community.",
                    Company = "Intel",
                    TwitterHandle = "davenielsen",
                    UserBioShort = "I head up ecosystem programs at Redis Labs. I'm also the co-founder of CloudCamp. ",
                    ImageUrl = "/images/Speaker-187.jpg",
                    Email = "Davx.Nielsen@codecamp.net"
                },
                new() 
                {
                    Id = 1124,
                    FirstName = "Douglas",
                    LastName = "Crockford",
                    Sat = true,
                    Sun = false,
                    Favorite = true,
                    Bio = "Douglas Crockford discovered the JSON Data Interchange Format. He is also the author of _JavaScript: The Good Parts_. He has been called a guru, but he is actually more of a mahatma.",
                    Company = "PayPal",
                    TwitterHandle = "",
                    UserBioShort = "Douglas Crockford discovered the JSON Data Interchange Format. He is also the author of _JavaScript: The Good Parts_. He has been called a guru, but he is actually more of a mahatma.",
                    ImageUrl = "/images/Speaker-1124.jpg",
                    Email = "Douglas.Crockford@codecamp.net"
                },
                new() 
                {
                    Id = 10803,
                    FirstName = "Eugene",
                    LastName = "Chuvyrov",
                    Sat = true,
                    Sun = false,
                    Favorite = false,
                    Bio = "Eugene Chuvyrov is  a Senior Cloud Architect at Microsoft. He works directly with both startups and enterprises to enable their solutions in Microsoft cloud, and to make Azure better as a result of this work with partners.",
                    Company = "Microsoft",
                    TwitterHandle = "EugeneChuvyrov",
                    UserBioShort = "Cloud Architect at Microsoft focused on accelerating modern DevOps, Machine Learning and Blockchain.",
                    ImageUrl = "/images/Speaker-10803.jpg",
                    Email = "Eugenx.Chuvyrov@codecamp.net"
                },
                new() 
                {
                    Id = 8367,
                    FirstName = "Gayle Laakmann",
                    LastName = "McDowell",
                    Sat = true,
                    Sun = false,
                    Favorite = false,
                    Bio = "Gayle Laakmann McDowell is the founder and CEO of CareerCup.com and the author of three best selling books.",
                    Company = "CareerCup",
                    TwitterHandle = "gayle",
                    UserBioShort = "Gayle Laakmann McDowell is the founder and CEO of CareerCup.com and the author of three books.",
                    ImageUrl = "/images/Speaker-8367.jpg",
                    Email = "Gayle Laakmann.McDowell@codecamp.net"
                },
                new() 
                {
                    Id = 18805,
                    FirstName = "Mickey W.",
                    LastName = "Mantle",
                    Sat = true,
                    Sun = true,
                    Favorite = false,
                    Bio = "Mickey has been developing software systems and products for over 40 years, as a systems programmer, Tech Lead, Manager, VP Engineering, CTO, COO, and now CEO/CTO of his own company.",
                    Company = "Wanderful, Inc.",
                    TwitterHandle = "mwmantleCA",
                    UserBioShort = "Mickey has been developing software products for over 40 years – in a variety of leadership roles.",
                    ImageUrl = "/images/Speaker-18805.jpg",
                    Email = "Mickey W..Mantle@codecamp.net"
                },
                new() 
                {
                    Id = 41808,
                    FirstName = "Paul",
                    LastName = "Everitt",
                    Sat = true,
                    Sun = true,
                    Favorite = false,
                    Bio = "Paul is the PyCharm and WebStorm Developer Advocate at JetBrains. Before that, Paul was a partner at Agendaless Consulting and co-founder of Zope Corporation, taking the first open source application server through $14M of funding.",
                    Company = "JetBrains, Inc.",
                    TwitterHandle = "paulweveritt",
                    UserBioShort = "Paul is the PyCharm and WebStorm Developer Advocate at JetBrains.",
                    ImageUrl = "/images/Speaker-41808.jpg",
                    Email = "Paul.Everitt@codecamp.net"
                },
                new() 
                {
                    Id = 1530,
                    FirstName = "Tamara",
                    LastName = "Baker",
                    Sat = false,
                    Sun = true,
                    Favorite = true,
                    Company = "Code Camp",
                    TwitterHandle = "tammybaker123",
                    UserBioShort = "Tammy is a software development leader for over 20 years.",
                    Bio = "Tammy has held a number of executive and management roles over the past 15 years, including VP engineering Roles at USA Tech, Cantaloupe Systems, E-Color, and Untangle Inc.",
                    ImageUrl = "/images/Speaker-1530.jpg",
                    Email = "Tamara.Baker@codecamp.net"
                }
            };
            await dbContext.Speakers.AddRangeAsync(speakerList).ConfigureAwait(false);
            var productList = new List<ProductEntity>()
            {
                new()
                {
                    ProductId = 10,
                    ProductName = "Leaf Rake",
                    ProductCode = "GDN-0011",
                    TagList = ["Rake"],
                    ReleaseDate = DateTime.Parse("2016-03-19"),
                    Price = 19.95,
                    Description = "Leaf rake with 48-inch wooden handlx.",
                    StarRating = 3.3,
                    ImageUrl = "http://openclipart.org/image/300px/svg_to_png/26215/Anonymous_Leaf_Rakx.png",
                    DateCreated = DateTime.Parse("2020-08-08")
                },
                new()
                {
                    ProductId = 11,
                    ProductName = "Garden Cart",
                    ProductCode = "GDN-0023",
                    TagList = ["Cart"],
                    ReleaseDate = DateTime.Parse("2015-02-05"),
                    Price = 32.99,
                    Description = "15 gallon capacity rolling garden cart",
                    StarRating = 4.2,
                    ImageUrl = "http://openclipart.org/image/300px/svg_to_png/58471/garden_cart.png",
                    DateCreated = DateTime.Parse("2020-08-08")
                },
                new()
                {
                    ProductId = 12,
                    ProductName = "Video Game Controller",
                    ProductCode = "GMG-0042",
                    TagList = ["VGC"],
                    ReleaseDate = DateTime.Parse("2015-03-19"),
                    Price = 35.95,
                    Description = "Standard two-button video game controller",
                    StarRating = 4.6,
                    ImageUrl = "http://openclipart.org/image/300px/svg_to_png/120337/xbox-controller_01.png",
                    DateCreated = DateTime.Parse("2020-08-08")
                },
                new()
                {
                    ProductId = 13,
                    ProductName = "Saw",
                    ProductCode = "TBX-0022",
                    TagList = ["Saw"],
                    ReleaseDate = DateTime.Parse("2017-08-10"),
                    Price = 11.55,
                    Description = "15-inch steel blade hand saw",
                    StarRating = 3.7,
                    ImageUrl = "http://openclipart.org/image/300px/svg_to_png/27070/egore911_saw.png",
                    DateCreated = DateTime.Parse("2020-08-08")
                },
                new()
                {
                    ProductId = 14,
                    ProductName = "Hammer",
                    ProductCode = "TBX-0048",
                    TagList = ["Ham"],
                    ReleaseDate = DateTime.Parse("2016-07-14"),
                    Price = 8.9,
                    Description = "Curved claw steel hammer",
                    StarRating = 4.8,
                    ImageUrl = "http://openclipart.org/image/300px/svg_to_png/73/rejon_Hammer.png",
                    DateCreated = DateTime.Parse("2020-08-08")
                }
            };
            await dbContext.Products.AddRangeAsync(productList).ConfigureAwait(false);
            // Save Changes
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ex.Data.Add("Function", "DataSeedAsync");
            throw;
        }      
    }
}