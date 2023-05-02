using Albelli.OrderManagement.Business.Contracts;
using Albelli.OrderManagement.DAL.Contracts.Repositories;
using Albelli.OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Business
{
    public class PackageWidthCalculator : IPackageWidthCalculator
    {
        public async Task<double> Calculate(IEnumerable<OrderLine> items, IProductInfoRepository productInfoRepository)
        {
            double pw = 0;

            foreach (var item in items)
            {
                if (item.ProductType == ProductType.Mug)
                    continue;
                var q = item.Quantity;
                var info = await productInfoRepository.Get(item.ProductType);

                pw += info.WidthMm * q;
            }
            var mugsCnt = items.Where(i => i.ProductType == ProductType.Mug).Count();
            var quadriplets = mugsCnt / 4 + mugsCnt % 4 > 0 ? 1 : 0;
            pw += quadriplets * (await productInfoRepository.Get(ProductType.Mug)).WidthMm;
            return pw;
        }
    }
}
