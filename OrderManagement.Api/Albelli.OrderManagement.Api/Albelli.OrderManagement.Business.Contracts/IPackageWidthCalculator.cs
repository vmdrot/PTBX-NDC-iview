using Albelli.OrderManagement.DAL.Contracts.Repositories;
using Albelli.OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Business.Contracts
{
    public interface IPackageWidthCalculator
    {
        Task<double> Calculate(IEnumerable<OrderLine> items, IProductInfoRepository productInfoRepository);
    }
}
