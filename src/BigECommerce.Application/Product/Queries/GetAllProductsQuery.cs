using BigECommerce.Application.Product.Dtos;
using MediatR;

namespace BigECommerce.Application.Product.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
        public GetAllProductsQuery(string search)
        {
            Search = search;
        }

        public string Search { get; internal set; }
    }
}
