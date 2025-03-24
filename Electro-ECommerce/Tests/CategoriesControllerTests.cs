using Electro_ECommerce.Controllers;
using Electro_ECommerce.Models;
using Electro_ECommerce.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Electro_ECommerce.Tests
{
    public class CategoriesControllerTests
    {
        private readonly Mock<IRepository<Category>> _mockRepo;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly Mock<ILogger<CategoriesController>> _mockLogger; 
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _mockRepo = new Mock<IRepository<Category>>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockLogger = new Mock<ILogger<CategoriesController>>(); 
            _controller = new CategoriesController(_mockRepo.Object, _mockWebHostEnvironment.Object); 
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfCategories()
        {
          
            _mockRepo.Setup(repo => repo.GetAll())
                .Returns(new List<Category> {
                    new Category { CategoryId = 1, Name = "Category 1" },
                    new Category { CategoryId = 2, Name = "Category 2" }
                });

            
            var result = _controller.Index();

            
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.Model);
            Assert.Equal(2, ((List<Category>)model).Count);
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
        public void Details_ReturnsNotFoundResult_WhenCategoryNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Category?)null);

            // Act
            var result = _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_ReturnsViewResult_WithCategory()
        {
            // Arrange
            var category = new Category { CategoryId = 1, Name = "Test Category" };
            _mockRepo.Setup(repo => repo.GetById(1)).Returns(category);

            // Act
            var result = _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model as Category; // Cast to Category

            Assert.True(AreCategoriesEqual(category, model)); // Use the comparison method
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_ReturnsRedirectToIndex_WhenModelStateIsValid()
        {
            // Arrange
            _mockWebHostEnvironment.Setup(env => env.WebRootPath).Returns("some_path");
            var newCategory = new Category { Name = "New Category" };
            var mockFile = new Mock<IFormFile>();

            // Act
            var result = await _controller.Create(newCategory, mockFile.Object);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepo.Verify(repo => repo.Add(newCategory), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task Create_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Name is required");
            var newCategory = new Category { Name = "Invalid Category" };
            var mockFile = new Mock<IFormFile>();

            // Act
            var result = await _controller.Create(newCategory, mockFile.Object);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newCategory, viewResult.Model);
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
        public void Edit_Get_ReturnsNotFoundResult_WhenCategoryNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Category?)null);

            // Act
            var result = _controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithCategory()
        {
            // Arrange
            var category = new Category { CategoryId = 1, Name = "Test Category" };
            _mockRepo.Setup(repo => repo.GetById(1)).Returns(category);

            // Act
            var result = _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model as Category;
            Assert.True(AreCategoriesEqual(category, model));
        }

        [Fact]
        public async Task Edit_Post_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var category = new Category { CategoryId = 2, Name = "Test Category" };

            // Act
            var result = await _controller.Edit(1, category, null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Edit_Post_ReturnsRedirectToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var category = new Category { CategoryId = 1, Name = "Test Category" };
            _mockRepo.Setup(repo => repo.GetById(1)).Returns(category); // Ensure existing category is returned
            _mockRepo.Setup(repo => repo.Find(It.IsAny<System.Linq.Expressions.Expression<Func<Category, bool>>>())).Returns(new List<Category>());

            // Act
            var result = await _controller.Edit(1, category, null);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepo.Verify(repo => repo.Update(It.IsAny<Category>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Name is required");
            var category = new Category { CategoryId = 1, Name = "Invalid Category" };

            // Act
            var result = await _controller.Edit(1, category, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(category, viewResult.Model);
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
        public void Delete_Get_ReturnsNotFoundResult_WhenCategoryNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Category?)null);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Get_ReturnsViewResult_WithCategory()
        {
            // Arrange
            var category = new Category { CategoryId = 1, Name = "Test Category" };
            _mockRepo.Setup(repo => repo.GetById(1)).Returns(category);

            // Act
            var result = _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model as Category;
            Assert.True(AreCategoriesEqual(category, model));
        }

        [Fact]
        public void DeleteConfirmed_Post_ReturnsRedirectToIndex_WhenCategoryIsDeleted()
        {
            // Arrange
            var category = new Category { CategoryId = 1, Name = "Test Category" };
            _mockRepo.Setup(repo => repo.GetById(1)).Returns(category);

            // Act
            var result = _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepo.Verify(repo => repo.Delete(category), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteConfirmed_Post_ReturnsNotFound_WhenCategoryNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Category?)null);

            // Act
            var result = _controller.DeleteConfirmed(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private bool AreCategoriesEqual(Category? expected, Category? actual)
        {
            if (expected == null && actual == null) return true;
            if (expected == null || actual == null) return false;

            return expected.CategoryId == actual.CategoryId &&
                   expected.Name == actual.Name &&
                   expected.Description == actual.Description &&
                   expected.ImageUrl == actual.ImageUrl;
        }
    }
}
