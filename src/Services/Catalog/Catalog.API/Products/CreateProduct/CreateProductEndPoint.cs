using BuildingBlocks.CQRS;
using Catalog.API.Products.CreateProduct;
using static System.Net.WebRequestMethods;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, decimal Price, string ImageFile);
    public record CreateProductResponse(Guid Id);

    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", 
                async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Creates a new product.");

        }   
    }
}

//1️ Client → POST /products
//   |
//   V
//2️ ASP.NET Minimal API (Carter endpoint)
//   - отримує CreateProductRequest (DTO для API)
//   - тип: record CreateProductRequest
//   |
//   V
//3️ Mapster.Adapt()
//   - перетворює CreateProductRequest → CreateProductCommand
//   - тип: record CreateProductCommand : ICommand<CreateProductResult>
//   |
//   V
//4️ sender.Send(command)   // ISender з MediatR
//   - MediatR бачить, що це IRequest<CreateProductResult>
//   - шукає handler, який реалізує IRequestHandler<CreateProductCommand, CreateProductResult>
//   |
//   V
//5️ CreateProductCommandHandler.Handle()
//   - клас: CreateProductCommandHandler
//   - інтерфейс: ICommandHandler < CreateProductCommand, CreateProductResult >
//   -він успадковує IRequestHandler<CreateProductCommand, CreateProductResult>
//   - тут виконується бізнес-логіка (збереження продукту в БД)
//   -повертає CreateProductResult(Guid Id)
//   |
//   V
//6️ Mapster.Adapt()
//   - перетворює CreateProductResult → CreateProductResponse
//   - тип: record CreateProductResponse
//   |
//   V
//7️ Results.Created(...)
//   - повертає HTTP 201 Created + JSON (CreateProductResponse)

