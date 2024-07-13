using AutoMapper;
using eShop.Catalog.Application.Contract.Data;
using eShop.Catalog.Domain.Product;
using CSharpFunctionalExtensions;
using FluentValidation;
using System.Text.Json;
using eShop.Catalog.Application.Products.Contracts;
using eShop.Catalog.Application.Products.Contracts.DTOs;


namespace eShop.Catalog.Application.Products
{
    public class ProductManager : IProductManager
    {
        //injection
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;
        private IValidator<ProductForCreateDto> _productForCreateValidator;
        private IValidator<ProductForUpdateDto> _productForUpdateValidator;
        public ProductManager(
            IProductRepository productRepository,
            IMapper mapper,
            IValidator<ProductForCreateDto> productForCreateValidator,
            IValidator<ProductForUpdateDto> productForUpdateValidator
            )
        {
            _ProductRepository = productRepository;
            _mapper = mapper;
            _productForCreateValidator = productForCreateValidator;
            _productForUpdateValidator = productForUpdateValidator;
        }

        public async Task<Result> CreateProductAsync(ProductForCreateDto productDto)
        {
            //conversion (DTO __> Domain object)

            try
            {
                //dry (dont repeat urself (create validator folder in products folder) )

                //if (string.IsNullOrEmpty(productDto.Name))
                //{
                //    return Result.Failure("product name is required.");
                //}
                var ValidationResult = _productForCreateValidator.Validate(productDto);
                if (!ValidationResult.IsValid)
                {
                    // ValidationResult.Errors 
                    return Result.Failure(JsonSerializer.Serialize(ValidationResult));
                }


                Product product = new(productDto.Name, productDto.Price, productDto.Description);
                await _ProductRepository.AddAsync(product);

                return Result.Success();
            }
            catch (Exception exp)
            {

                return Result.Failure(exp.Message);

            }

        }

        public async Task<Result> DeleteProductAsync(Guid productId)
        {
            try
            {
                var product = await _ProductRepository.GetByIdAsync(productId);
                if (product is null)
                {
                    return Result.Failure<ProductDto>($"product Id ({productId}) not found");
                }
                //execute/canExecute
                //order
                await _ProductRepository.DeleteAsync(product);
                return Result.Success();
            }
            catch (Exception exp)
            {
                return Result.Failure<ProductDto>(exp.Message);
            }
        }


        public async Task<Result<PageList<ProductDto>>> FilterProductsAsync(string? criteria, QueryData data)
        {
            try
            {
                var (product, totalRecordCount) = await _ProductRepository.FilterAsync(criteria, data.sort, data.pageSize, data.pageIndex);
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(product);
                var pageList = new PageList<ProductDto>(productDtos, totalRecordCount);

                return Result.Success(pageList);
            }
            catch (Exception exp)
            {
                return Result.Failure<PageList<ProductDto>>(exp.Message);
            }
        }
        public async Task<Result<PageList<ProductDto>>> SearchProductAsync(string? text, QueryData data)
        {
            try
            {
                var (product, totalRecordCount) = await _ProductRepository.SearchAsync(text, data.sort, data.pageSize, data.pageIndex);
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(product);
                var pageList = new PageList<ProductDto>(productDtos, totalRecordCount);

                return Result.Success(pageList);
            }
            catch (Exception exp)
            {
                return Result.Failure<PageList<ProductDto>>(exp.Message);
            }
        }
        public async Task<Result<ProductDto>> GetProductByIdAsync(Guid productId)
        {
            //conversion 
            //product object => converting to productDto
            try
            {
                var product = await _ProductRepository.GetByIdAsync(productId);
                if (product is null)
                {
                    return Result.Failure<ProductDto>($"product Id ({productId}) not found");
                }
                //ProductDto productDto = new(product.Id , product.Name, product.Price, product.Description);
                var productDto = _mapper.Map<ProductDto>(product);

                return productDto;
            }
            catch (Exception exp)
            {
                return Result.Failure<ProductDto>(exp.Message);
            }



        }

        public async Task<Result<IEnumerable<ProductDto>>> GetProductsAsync()
        {
            try
            {
                IEnumerable<Product> products = await _ProductRepository.FindEntitiesAysnc(p =>
                p.Price > 5 && p.Price < 25
                );

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Result.Success(productDtos);
            }
            catch (Exception exp)
            {

                return Result.Failure<IEnumerable<ProductDto>>(exp.Message);
            }
        }

        public async Task<Result> UpdateProductAsync(Guid productId, ProductForUpdateDto ProductDto)
        {
            //conversion (DTO __> Domain object)

            try
            {
                var ValidationResult = _productForUpdateValidator.Validate(ProductDto);
                if (!ValidationResult.IsValid)
                {
                    return Result.Failure(JsonSerializer.Serialize(ValidationResult));
                }
                var product = await _ProductRepository.GetByIdAsync(productId);

                if (product is null)
                {
                    return Result.Failure($"product id ({productId}) not found.");
                }
                //update
                product.ChangePrice(ProductDto.Price);
                product.ChangeName(ProductDto.Name);
                product.Description = ProductDto.Description;

                await _ProductRepository.UpdateAsync(product);

                return Result.Success();
            }
            catch (Exception exp)
            {

                return Result.Failure(exp.Message);

            }

        }



    }
}
