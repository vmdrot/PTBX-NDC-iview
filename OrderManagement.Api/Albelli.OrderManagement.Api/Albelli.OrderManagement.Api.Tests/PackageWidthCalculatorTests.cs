using Albelli.OrderManagement.Api.Controllers;
using Albelli.OrderManagement.Business;
using Albelli.OrderManagement.Business.Contracts;
using Albelli.OrderManagement.DAL.Contracts.Repositories;
using Albelli.OrderManagement.DAL.Repositories;
using Albelli.OrderManagement.Models;
using System.Collections.Generic;
using Xunit;

namespace Albelli.OrderManagement.Api.Tests
{

    public class PackageWidthCalculatorTests
    {
        [Fact]
        public void PackageWidthTest_Success()
        {
            IProductInfoRepository productInfoRepository = new ProductInfoRepository();
            IPackageWidthCalculator calc = new PackageWidthCalculator();
            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Calendar, Quantity = 2 });

            var res = calc.Calculate(lines, productInfoRepository).ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.Equal(20, res);
        }

        [Fact]
        public void PackageWidthTest_Failure()
        {
            IProductInfoRepository productInfoRepository = new ProductInfoRepository();
            IPackageWidthCalculator calc = new PackageWidthCalculator();
            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Calendar, Quantity = 2 });

            var res = calc.Calculate(lines, productInfoRepository).ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.Equal(20, res);
        }

        [Fact]
        public void MugPackageWidthTest_Success()
        {
            IProductInfoRepository productInfoRepository = new ProductInfoRepository();
            IPackageWidthCalculator calc = new PackageWidthCalculator();

            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Mug, Quantity = 5 });

            var res = calc.Calculate(lines, productInfoRepository).ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.Equal(94, res); // 188 was an old expected value - before a more sophisticated, mug-specifics accounting algorythm has been implemented
        }
    }
}
