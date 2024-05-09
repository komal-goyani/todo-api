using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using ToDoAPI.Models;

namespace ToDoAPI.Tests
{
    public class ToDoItemTests
    {
        [Fact]
        public void Name_SetWithValueExceeding100Chars_ValidationShouldFail()
        {
            //Arrange
            var todoItem = new ToDoItem();
            todoItem.Name = new string('x', 101);

            var context = new ValidationContext(todoItem);

            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(todoItem, context, results, validateAllProperties: true);

            // Assert
            isValid.Should().BeFalse(because: "Name length is greater than max length");

        }

        [Fact]
        public void Name_SetWithValueLessthan100Chars_ValidationShouldFail()
        {
            //Arrange
            var todoItem = new ToDoItem();
            todoItem.Name = new string('x', 51);

            var context = new ValidationContext(todoItem);

            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(todoItem, context, results, validateAllProperties: true);

            // Assert
            isValid.Should().BeTrue();

        }

    }
}