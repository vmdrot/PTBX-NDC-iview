using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Albelli.OrderManagement.DAL.Contracts.Repositories;
using Albelli.OrderManagement.Models;


namespace Albelli.OrderManagement.DAL.Repositories
{
    public class ProductInfoRepository : IProductInfoRepository
    {
        private static readonly IDictionary<ProductType, ProductInfo> _productInfo = new Dictionary<ProductType, ProductInfo>()
        {
            { ProductType.PhotoBook, new ProductInfo { WidthMm = 19 } },
            { ProductType.Calendar, new ProductInfo { WidthMm = 10 } },
            { ProductType.Canvas, new ProductInfo { WidthMm = 16 } },
            { ProductType.Cards, new ProductInfo { WidthMm = 4.7 } },
            { ProductType.Mug, new ProductInfo { WidthMm = 94 } }
        };

#pragma warning disable 1998
        public async Task<ProductInfo> Get(ProductType productType)
#pragma warning restore 1998
        {
            if (!_productInfo.ContainsKey(productType))
            {
                throw new Exception($"No information available for product type {productType}.", null);
            }

            var info = _productInfo[productType];

            return new ProductInfo
            {
                ProductType = productType,
                WidthMm = info.WidthMm
            };
        }
    }
}
