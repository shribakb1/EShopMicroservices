using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPrecondguredProducts());
            await session.SaveChangesAsync();
        }

        public static IEnumerable<Product> GetPrecondguredProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Id = new Guid("63d490f4-3f5e-4b6d-8c9e-8f1e3f0c8e4a"),
                    Name = "IPhone X",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec lacinia mi quis euismod ultrices. ",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = new List<string>() { "Smart Phone", "Electronics" }
                },
                new Product
                {
                    Id = new Guid("73d490f4-3f5e-4b6d-8c9e-8f1e3f0c8e4b"),
                    Name = "Samsung 10",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec lacinia mi quis euismod ultrices. ",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = new List<string>() { "Smart Phone", "Electronics" }
                },
                new Product
                {
                    Id = new Guid("83d490f4-3f5e-4b6d-8c9e-8f1e3f0c8e4c"),
                    Name = "Huawei Plus",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec lacinia mi quis euismod ultrices. ",
                    ImageFile = "product-3.png",
                    Price = 650.00M,
                    Category = new List<string>() { "White Appliances", "Electronics" }
                },
                new Product
                {
                    Id = new Guid("93d490f4-3f5e-4b6d-8c9e-8f1e3f0c8e4d"),
                    Name = "Xiaomi Mi 9",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec lacinia mi quis euismod ultrices. ",
                    ImageFile = "product-4.png",
                    Price = 470.00M,
                    Category = new List<string>() { "White Appliances", "Electronics" }
                },
                new Product
                {
                    Id = new Guid("a3d490f4-3f5e-4b6d-8c9e-8f1e3f0c8e4e"),  
                    Name = "HTC U11+ Plus",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec lacinia mi quis euismod ultrices. ",
                    ImageFile = "product-5.png",
                    Price = 380.00M,
                    Category = new List<string>() { "Smart Phone", "Electronics" }
                },
                new Product
                {
                    Id = new Guid("b3d490f4-3f5e-4b6d-8c9e-8f1e3f0c8e4f"),
                    Name = "LG G7 ThinQ",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec lacinia mi quis euismod ultrices. ",
                    ImageFile = "product-6.png",
                    Price = 240.00M,
                    Category = new List<string>() { "Home Kitchen", "Electronics" }
                }
            };
        }
    }
}
