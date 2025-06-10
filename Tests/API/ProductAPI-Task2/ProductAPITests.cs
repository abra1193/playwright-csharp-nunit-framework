using System.Net;
using System.Threading.Tasks;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using FluentAssertions;
using NUnit.Framework;
using qa_automation_exercise__mejiabritoabraham.Framework.API.APIActions;
using qa_automation_exercise__mejiabritoabraham.Utils;

namespace qa_automation_exercise__mejiabritoabraham.Tests.API.ProductAPI_Task2
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Product API Tests")]
    public class ProductApiTests
    {
        private ProductActions _productActions;

        [SetUp]
        public void BaseSetup()
        {
            _productActions = new ProductActions();
        }

        [Test]
        public async Task ShouldCreateProduct()
        {
            Assert.Ignore("Not yet implemented");
            var response = await _productActions.CreateProductAsync(
                Constants.ValidProductId,
                "Chew Toy",
                "Durable dog chew", Constants.PartnerId);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task ShouldRetrieveProduct()
        {
            Assert.Ignore("Not yet implemented");
            var response = await _productActions.GetProductAsync(Constants.ValidProductId, Constants.PartnerId);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data!.Title.Should().Be("chew Toy");
        }

        [Test]
        public async Task ShouldNotAccessOtherPartnerProducts()
        {
            Assert.Ignore("Not yet implemented");
            var response = await _productActions.GetProductRawAsync(Constants.ValidProductId, Constants.OtherPartnerId);

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task ShouldReturnsBadRequestOnInvalidProductId()
        {
            Assert.Ignore("Not yet implemented");
            var response = await _productActions.CreateProductAsync("123", Constants.PartnerId, "Invalid ID Product");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task ShouldCreateProductWithoutDescription()
        {
            Assert.Ignore("Not yet implemented");
            var createResponse = await _productActions.CreateProductAsync(Constants.ValidProductId2, "Simple Product", Constants.PartnerId);

            Assert.AreEqual(HttpStatusCode.OK, createResponse.StatusCode);

            var getResponse = await _productActions.GetProductAsync(Constants.ValidProductId2, Constants.PartnerId);

            getResponse.Data!.Title.Should().Be("Simple Product");
            getResponse.Data!.Description.Should().BeNullOrEmpty();
        }

        [Test]
        public async Task ShouldReturnUnauthorizedWhenPartnerHeaderIsMissing()
        {
            Assert.Ignore("Not yet implemented");
            var response = await _productActions.CreateProductAsync("UNAUTHORIZED1", "No Partner Header");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
