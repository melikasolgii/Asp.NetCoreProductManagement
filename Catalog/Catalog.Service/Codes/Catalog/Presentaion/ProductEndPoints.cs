using Carter;
using eShop.Catalog.Application.Contract.Data;
using eShop.Catalog.Presentaion.Requests;
using eShop.Catalog.Domain.Product;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using eShop.Catalog.Application.Products.Contracts;
using eShop.Catalog.Application.Products.Contracts.DTOs;


namespace eShop.Catalog.Catalog.Presentaion
{
    public class ProductEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var Products = app.MapGroup("Products").WithTags("Products");

            Products.MapPut("/{productId}", UpdateProductAsync);
            Products.MapDelete("/{productId}", DeleteProductAsync);
            Products.MapPost("/", CreateProductAsync);
            Products.MapGet("/", GetProductsAsync);
            Products.MapGet("/{productId}", GetProductByIdAsync);
            Products.MapGet("/filter", FilterProductAsync);
            Products.MapGet("/search", SearchProductAsync);
            //Products.MapGet("/byMinPrice", GetProductByMinPriceAsync);
            //Products.MapGet("/byMaxPrice", GetProductByMaxPriceAsync);
            //Products.MapGet("/byPrice", GetProductByPriceAsync);
        }


        private async Task<IResult> UpdateProductAsync([FromRoute] Guid productId, [FromBody] ProductForUpdateDto ProductDto, [FromServices] IProductManager productManager)
        {
            var result = await productManager.UpdateProductAsync(productId, ProductDto);
            if (result.IsSuccess)
            {
                return Results.Ok();
            }
            var ValidationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);


            if (ValidationResult is not null)
            {
                return Results.ValidationProblem(ValidationResult!.ToDictionary());
            }
            return Results.BadRequest(result.Error);
        }

        private async Task<IResult> CreateProductAsync([FromBody] ProductForCreateDto ProductDto, [FromServices] IProductManager productManager)
        {
            var result = await productManager.CreateProductAsync(ProductDto);
            if (result.IsSuccess)
            {
                return Results.Ok();
            }
            var ValidationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);


            if (ValidationResult is not null)
            {
                return Results.ValidationProblem(ValidationResult!.ToDictionary());
            }
            return Results.BadRequest(result.Error);
        }
        private async Task<IResult> GetProductsAsync([FromServices] IProductManager productManager)
        {
            var ProductResult = await productManager.GetProductsAsync();
            if (ProductResult.IsSuccess)
            {
                return Results.Ok(ProductResult.Value);
            }
            return Results.BadRequest(ProductResult.Error);

        }
        private async Task<IResult> GetProductByIdAsync([FromRoute] Guid productId, [FromServices] IProductManager productManager)
        {
            var productResult = await productManager.GetProductByIdAsync(productId);

            if (productResult.IsSuccess)
            {
                return Results.Ok(productResult.Value);
            }

            return Results.BadRequest(productResult.Error);
        }
        private async Task<IResult> DeleteProductAsync(
            [FromRoute] Guid productId,
            [FromServices] IProductManager productManager)
        {
            var productResult = await productManager.DeleteProductAsync(productId);

            if (productResult.IsSuccess)
            {
                return Results.Ok();
            }

            return Results.BadRequest(productResult.Error);
        }



        //private async Task<IResult> FilterProductAsync(
        //    [AsParameters] FilterRequest filter ,
        //    [FromServices] IProductManager productManager
        //    )
        //{
        //    var productResult = await productManager.FilterProductsAsync(
        //    filter.criteria, 
        //    new QueryData(filter.sort, filter.pageSize , filter.pageIndex)
        //    );

        //        if (productResult.IsSuccess)
        //        {
        //            return Results.Ok(productResult.Value);
        //        }
        //        return Results.BadRequest(productResult.Error);
        //}




        private async Task<IResult> FilterProductAsync(
            [AsParameters] FilterRequest filter,
            [FromServices] IProductManager productManager,
            HttpResponse response
            )
        {
            var productsPageListResult = await productManager.FilterProductsAsync(
            filter.criteria,
            new QueryData(
            filter.sort,
            filter.pageSize,
            filter.pageIndex)
            );

            if (productsPageListResult.IsSuccess)
            {
                response.Headers.Append("X-TotalRecordCount", productsPageListResult.Value.TotalRecordCount.ToString());
                //payload
                return TypedResults.Ok(productsPageListResult.Value.Items);

            }
            return Results.BadRequest(productsPageListResult.Error);
        }


        private async Task<IResult> SearchProductAsync(
            [AsParameters] SearchRequest search,
            [FromServices] IProductManager productManager,
            HttpResponse response
            )
        {
            //remove for production environment (page loader)
            await Task.Delay(1500);

            var productsPageListResult = await productManager.SearchProductAsync(
            search.Text,
            new QueryData(
            search.sort,
            search.pageSize,
            search.pageIndex)
            );

            if (productsPageListResult.IsSuccess)
            {
                response.Headers.Append("X-TotalRecordCount", productsPageListResult.Value.TotalRecordCount.ToString());
                //payload
                return TypedResults.Ok(productsPageListResult.Value.Items);

            }
            return Results.BadRequest(productsPageListResult.Error);
        }





    }
}
