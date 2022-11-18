using System.Net;
using System.Threading.Tasks;
using CountryVATCalculator.Controllers;
using CountryVATCalculator.Models;
using CountryVATCalculator.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CountryVATRateCalculator.Tests.Unit.Controllers
{
    /// <summary>
    /// Unit tests for <see cref="CountryVATController"/>
    /// </summary>
    [TestFixture]
    public class ToDoControllerUnitTests
    {
        public CountryVATController Controller { get; set; }

        public Mock<IRepository<Country>> MockRepository { get; set; }

        public Mock<IValidator<CalculateVATRequest>> MockValidator { get; set; }

        [SetUp]
        public void Setup()
        {
            MockRepository = new Mock<IRepository<Country>>();
            Controller = new CountryVATController(MockRepository.Object, MockValidator.Object);
        }

        /// <summary>
        /// Given a <see cref="CountryVATController"/> is correctly constructed
        /// When calling <see cref="CountryVATController.GetById(int)"/> with an invalid id
        /// Then a return a <see cref="HttpStatusCode.BadRequest"/>
        /// </summary>
        /// <param name="id">Invalid todo list item id</param>
        [Test]
        [TestCase(null)]
        [TestCase(-1)]
        [TestCase(0)]
        public async Task GetById_InvalidId_ReturnsBadRequest(int id)
        {
            var response = await Controller.GetById(id) as BadRequestResult;
            Assert.IsNotNull(response, "Incorrect response object type");
        }

        /// <summary>
        /// Given no result will be found in the store
        /// When calling <see cref="CountryVATController.GetById(int)"/> for a non-existent <see cref="Country"/>
        /// Then return <see cref="HttpStatusCode.NotFound"/>
        /// </summary>
        [Test]
        public async Task GetById_NoResultFoundInRepository_ReturnsNotFound()
        {
            MockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(Country));

            var response = await Controller.GetById(1) as NotFoundObjectResult;
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// Given an existing <see cref="Country"/> is in the <see cref="CountryRepository"/>
        /// When calling <see cref="CountryVATController.GetById(int)"/> with that item's Id
        /// Then return <see cref="HttpStatusCode.OK"/> with the body of the <see cref="Country"/>
        /// </summary>
        [Test]
        public async Task GetById_WithExsitingToDoItemId_ReturnsToDoItem()
        {
            var expectedItem = new Country();
            MockRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedItem);

            var response = await Controller.GetById(1) as OkObjectResult;
            Assert.IsNotNull(response);
            var item = response.Value as Country;
            Assert.AreEqual(expectedItem, item);
        }
    }
}
