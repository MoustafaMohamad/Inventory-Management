﻿using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Enums;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Queries;
using MediatR;

namespace Inventory_Management.Features.Products.UpdateProduct.Commands
{
    public record UpdateProductCommand(int ID,
        string Name, Category Category,
        int Quantity, decimal Price, int Threshold,
        DateTime ExpiryDate, string Unit,
        IFormFile Image) : IRequest<ResultDto<bool>>;

    public class UpdateProductCommandHandler  : BaseRequestHandler<Product, UpdateProductCommand, ResultDto<bool>> 
    {
        private readonly ICloudinaryService _cloudinaryService;
        public UpdateProductCommandHandler(ICloudinaryService cloudinaryService,RequestParameters<Product> requestParameters) : base(requestParameters)
        {
            _cloudinaryService = cloudinaryService;
        }
        public  async override Task<ResultDto<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProductResult = await _mediator.Send(new GetProductByIdQuery(request.ID));
            if (!existingProductResult.IsSuccess)
            {
                return ResultDto<bool>.Faliure(existingProductResult.ErrorCode, existingProductResult.Message);
            }
            var product = existingProductResult.Data.MapOne<Product>();
            if (product is null)
            {
                return ResultDto<bool>.Faliure(existingProductResult.ErrorCode, existingProductResult.Message);
            }
           
                var imageUploadResult = await _cloudinaryService.UploadImageAsync(request.Image);
                var imageUrl = imageUploadResult.Url.ToString();
                product.ImageUrl = imageUrl;
            
            
            product.Name = request.Name;
            product.Category = request.Category;
            product.Quantity = request.Quantity;
            product.Price = request.Price;
            product.Threshold = request.Threshold;
            product.ExpiryDate = request.ExpiryDate;
            product.Unit = request.Unit;
            product.CreatedAt = DateTime.UtcNow;
            _repository.Update(product);
            await _repository.SaveChanges();
            return ResultDto<bool>.Sucess(true);
        }
    }
}
