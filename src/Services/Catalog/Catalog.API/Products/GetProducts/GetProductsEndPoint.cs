
namespace Catalog.API.Products.GetProducts
{
    //public record GetProductsRequest();
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndPoint : ICarterModule
    { 
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", 
                async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductQuery());
                
                var response = result.Adapt<GetProductResult>();
                
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Gets all products.");
        }
    }
}
