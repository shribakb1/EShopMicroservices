
namespace Catalog.API.Products.CreateProduct
{
    // Ця record-клас представляє "команду" – намір створити продукт
    // Вона інкапсулює всі дані, потрібні для операції
    public record CreateProductCommand(string Name, List<string> Category, string Description, decimal Price, string ImageFile)
        : ICommand<CreateProductResult>;

    // Це "результат" команди – те, що повертає обробник після створення продукту
    public record CreateProductResult(Guid Id);

    // Handler – це клас, який виконує логіку створення продукту

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("At least one category is required.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.ImageFile)
                .NotEmpty().WithMessage("Image file is required.")
                .Must(BeAValidUrl).WithMessage("Image file must be a valid URL.");
        }
        private bool BeAValidUrl(string imageFile)
        {
            return Uri.TryCreate(imageFile, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session) 
       : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Тут буде бізнес-логіка створення продукту (збереження у БД, валідації тощо)

            //Створити продукт
            //Зберегти у БД
            //Повернути результат з Id нового продукту
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                Price = command.Price,
                ImageFile = command.ImageFile
            };

            session.Store(product);

            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
