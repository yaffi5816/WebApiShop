namespace DTO
{
    public record ProductDTO(
        int ProductId,
        int CategoryId,
        string ProductName,
        string ProductDescreption,
        string CategoryName,
        double Price,
        string ImgUrl
    );
}
