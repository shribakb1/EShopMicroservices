namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, decimal Price, string ImageFile);
    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", 
                async (UpdateProductRequest request, ISender sender) =>
            {
                var productCommand = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(productCommand);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Updates an existing product.");
        }
    }
}
