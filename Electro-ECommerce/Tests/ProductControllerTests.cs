using Electro_ECommerce.Models;
using Electro_ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Electro_ECommerce.Controllers;

namespace Electro_ECommerce.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IRepository<Product>> _mockProductRepo;
        private readonly Mock<IRepository<Category>> _mockCategoryRepo;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockProductRepo = new Mock<IRepository<Product>>();
            _mockCategoryRepo = new Mock<IRepository<Category>>();
            _controller = new ProductController(_mockProductRepo.Object, _mockCategoryRepo.Object);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            _mockProductRepo.Setup(repo => repo.GetAll())
                .Returns(new List<Product> {
                    new Product { ProductId = 1, Name = "Product 1" },
                    new Product { ProductId = 2, Name = "Product 2" }
                });

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(2, ((List<Product>)model).Count);
        }

        [Fact]
        public void Details_ReturnsNotFoundResult_WhenIdIsZero()
        {
            // Act
            var result = _controller.Details(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_ReturnsNotFoundResult_WhenProductNotFound()
        {
            // Arrange
            _mockProductRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);

            // Act
            var result = _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_ReturnsViewResult_WithProduct()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product" };
            _mockProductRepo.Setup(repo => repo.GetById(1)).Returns(product);

            // Act
            var result = _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(product, viewResult.Model);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult_WithCategoriesInViewBag()
        {
            // Arrange
            _mockCategoryRepo.Setup(repo => repo.GetAll())
                .Returns(new List<Category> {
                new Category { CategoryId = 1, Name = "Category 1" },
                new Category { CategoryId = 2, Name = "Category 2" }
                });

            // Act
            var result = _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var categories = Assert.IsType<List<Category>>(viewResult.ViewData["Categories"]);
            Assert.Equal(2, categories.Count);
        }

        [Fact]
        public void Create_Post_ReturnsRedirectToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var newProduct = new Product { Name = "New Product", CategoryId = 1 };

            // Act
            var result = _controller.Create(newProduct);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockProductRepo.Verify(repo => repo.Add(newProduct), Times.Once);
            _mockProductRepo.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Create_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Name is required");
            var newProduct = new Product { Name = null };

            _mockCategoryRepo.Setup(repo => repo.GetAll())
             .Returns(new List<Category> {
                new Category { CategoryId = 1, Name = "Category 1" },
                new Category { CategoryId = 2, Name = "Category 2" }
             });

            // Act
            var result = _controller.Create(newProduct);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newProduct, viewResult.Model);
            Assert.IsType<SelectList>(viewResult.ViewData["Categories"]);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFoundResult_WhenIdIsZero()
        {
            // Act
            var result = _controller.Edit(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFoundResult_WhenProductNotFound()
        {
            // Arrange
            _mockProductRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);

            // Act
            var result = _controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithProductAndCategoriesInViewBag()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product", CategoryId = 1 };
            _mockProductRepo.Setup(repo => repo.GetById(1)).Returns(product);

            _mockCategoryRepo.Setup(repo => repo.GetAll())
                 .Returns(new List<Category> {
                    new Category { CategoryId = 1, Name = "Category 1" },
                    new Category { CategoryId = 2, Name = "Category 2" }
                 });

            // Act
            var result = _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(product, viewResult.Model);
            Assert.IsType<SelectList>(viewResult.ViewData["Categories"]);
        }

        [Fact]
        public void Edit_Post_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var product = new Product { ProductId = 2, Name = "Test Product" };

            // Act
            var result = _controller.Edit(1, product);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Edit_Post_ReturnsRedirectToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product", CategoryId = 1 };
            _mockProductRepo.Setup(repo => repo.Find(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>())).Returns(new List<Product>());
            // Act
            var result = _controller.Edit(1, product);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockProductRepo.Verify(repo => repo.Update(product), Times.Once);
            _mockProductRepo.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Name is required");

            _mockCategoryRepo.Setup(repo => repo.GetAll())
                 .Returns(new List<Category> {
                    new Category { CategoryId = 1, Name = "Category 1" },
                    new Category { CategoryId = 2, Name = "Category 2" }
                 });

            var product = new Product { ProductId = 1, Name = null };
            // Act
            var result = _controller.Edit(1, product);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(product, viewResult.Model);
            Assert.IsType<SelectList>(viewResult.ViewData["Categories"]);
        }

        [Fact]
        public void Delete_Get_ReturnsNotFoundResult_WhenIdIsZero()
        {
            // Act
            var result = _controller.Delete(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Get_ReturnsNotFoundResult_WhenProductNotFound()
        {
            // Arrange
            _mockProductRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Get_ReturnsViewResult_WithProduct()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product" };
            _mockProductRepo.Setup(repo => repo.GetById(1)).Returns(product);

            // Act
            var result = _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(product, viewResult.Model);
        }

        [Fact]
        public void DeleteConfirmed_Post_ReturnsRedirectToIndex_WhenProductIsDeleted()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product" };
            _mockProductRepo.Setup(repo => repo.GetById(1)).Returns(product);

            // Act
            var result = _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockProductRepo.Verify(repo => repo.Delete(product), Times.Once);
            _mockProductRepo.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteConfirmed_Post_ReturnsNotFound_WhenProductNotFound()
        {
            // Arrange
            _mockProductRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);

            // Act
            var result = _controller.DeleteConfirmed(1) as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}