using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoAPI.Controllers;
using ToDoAPI.Models;
using ToDoAPI.Services;
using ToDoAPI.Tests.MockData;

namespace ToDoAPI.Tests
{
    public class ToDoControllerTests
    {

        private readonly Mock<IToDoService> mockToDoService;
        public ToDoControllerTests()
        {
            mockToDoService = new Mock<IToDoService>();
        }

        [Fact]
        public async Task Get_WithoutId_ReturnsAllToDoItems()
        {
            var todoList = ToDoMockData.GetTodos();

            // Arrange
            mockToDoService.Setup(service => service.GetAsync()).ReturnsAsync(todoList);
            var controller = new ToDoController(mockToDoService.Object);

            /// Act
            var result = await controller.Get();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(todoList.Count);
        }

        [Fact]
        public async Task GetById_WithId_ReturnsToDoItem()
        {
            const string Id = "663bdfd8d92503a0e90e19db";
            var expectedToDoItem = ToDoMockData.GetTodos().First(x => x.Id == Id);

            // Arrange
            mockToDoService.Setup<Task<ToDoItem>>(service => service.GetAsync(Id)).ReturnsAsync(expectedToDoItem);
            var controller = new ToDoController(mockToDoService.Object);

            /// Act
            var result = await controller.GetById(Id);

            //Assert
            result?.Result.Should().NotBeNull();

            result.Result.Should().BeOfType<OkObjectResult>();

            var actualTodoItem = ((ObjectResult?)result.Result).Value;

            actualTodoItem.Should().NotBeNull();
            actualTodoItem.Should().BeOfType<ToDoItem>();

            ((ToDoItem?)actualTodoItem)?.Id.Should().Be(expectedToDoItem.Id);
        }

        [Fact]
        public async Task GetById_ItemDoesNotExist_ReturnsNotFound()
        {
            // Arrange            
            const string Id = "663bdfd8d92503a0e90e19df";
            mockToDoService.Setup(service => service.GetAsync(Id)).ReturnsAsync((ToDoItem?)null);

            var controller = new ToDoController(mockToDoService.Object);

            // Act
            var result = await controller.GetById(Id);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            const string Id = "663bdfd8d92503a0e90e19dg";

            var todoItem = new ToDoItem
            {
                Name = "Drop Kids at school",
                IsCompleted = false
            };
            mockToDoService.Setup(service => service.CreateAsync(todoItem)).Callback((ToDoItem t) =>
            {
                t.Id = Id;
            });
            var controller = new ToDoController(mockToDoService.Object);

            // Act
            var result = await controller.Post(todoItem);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();
            var routeValue = (((CreatedAtActionResult)result).RouteValues).Values;
            var id = ((object[])routeValue)[0];
            id.Should().Be(Id);

            mockToDoService.Verify(x => x.CreateAsync(todoItem), Times.Once());
        }

        [Fact]
        public async Task Update_ItemDoesNotExist_ReturnsNotFound()
        {
            // Arrange            
            const string Id = "663bdfd8d92503a0e90e19df";

            var todoItem = new ToDoItem
            {
                Name = "Drop Kids at school",
                IsCompleted = true
            };
            mockToDoService.Setup(service => service.GetAsync(Id)).ReturnsAsync((ToDoItem?)null);

            var controller = new ToDoController(mockToDoService.Object);

            // Act
            var result = await controller.Update(Id, todoItem);

            // Assert
            mockToDoService.Verify(x => x.UpdateAsync(Id, todoItem), Times.Never);
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Update_ItemExists_UpdatesItemAndReturnsNoContent()
        {
            // Arrange            
            const string Id = "663bdfd8d92503a0e90e19df";

            var todoItem = new ToDoItem
            {
                Id = Id,
                Name = "Drop Kids at school",
                IsCompleted = true
            };
            mockToDoService.Setup(service => service.GetAsync(Id)).ReturnsAsync((todoItem));
            mockToDoService.Setup(service => service.UpdateAsync(Id, todoItem));

            var controller = new ToDoController(mockToDoService.Object);

            // Act
            var result = await controller.Update(Id, todoItem);

            // Assert
            mockToDoService.Verify(x => x.UpdateAsync(Id, todoItem), Times.Once);
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ItemDoesNotExist_ReturnsNotFound()
        {
            // Arrange            
            const string Id = "663bdfd8d92503a0e90e19df";
            mockToDoService.Setup(service => service.GetAsync(Id)).ReturnsAsync((ToDoItem?)null);

            var controller = new ToDoController(mockToDoService.Object);

            // Act
            var result = await controller.Delete(Id);

            // Assert
            mockToDoService.Verify(x => x.DeleteAsync(Id), Times.Never);
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Delete_ItemExists_DeletesItemAndReturnsNoContent()
        {
            // Arrange
            const string Id = "663bdfd8d92503a0e90e19df";
            mockToDoService.Setup(service => service.GetAsync(Id)).ReturnsAsync(new ToDoItem());
            mockToDoService.Setup(service => service.DeleteAsync(Id));

            var controller = new ToDoController(mockToDoService.Object);

            // Act
            var result = await controller.Delete(Id);

            // Assert
            mockToDoService.Verify(x => x.DeleteAsync(Id), Times.Once);
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
