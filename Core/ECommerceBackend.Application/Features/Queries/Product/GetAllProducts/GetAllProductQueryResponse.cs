namespace ECommerceBackend.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductQueryResponse
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<Domain.Entities.Product>? Data { get; set; }
    }
}
