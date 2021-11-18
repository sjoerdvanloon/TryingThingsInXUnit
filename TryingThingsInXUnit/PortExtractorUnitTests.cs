using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TryingThingsInXUnit
{
    public class PortExtractorUnitTests
    {

        [Theory()]
        [InlineData("1", new int[] { 1 })]
        [InlineData("1,2", new int[] { 1, 2 })]
        [InlineData("1-3", new int[] { 1, 2, 3 })]
        [InlineData("1-3,6", new int[] { 1, 2, 3, 6 })]
        [InlineData("1-3,6-8", new int[] { 1, 2, 3, 6, 7, 8 })]
        public void GetPorts_ShouldReturnCorrectPossiblePorts(string input, int[] expected)
        {
            // Arrange
            var instance = new PortExtractor();

            // Act
            var ports = instance.GetPorts(input);

            // Assert
            ports.Should().HaveCount(expected.Count()).And.Contain(expected);
        }

        [Fact()]
        public void GetPorts_ShouldThrowException_WhenTooManyNumbersInRange()
        {
            // Arrange
            var instance = new PortExtractor();

            // Act
            Action act = () => instance.GetPorts("1-2-3");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Range part 1-2-3 contained more then 2 (3) unparsed numbers parts");
        }

        [Fact()]
        public void GetPorts_ShouldThrowException_WhenFirstNumberIsNotANumber()
        {
            // Arrange
            var instance = new PortExtractor();

            // Act
            Action act = () => instance.GetPorts("a");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("a is not an valid int");
        }

        [Fact()]
        public void GetPorts_ShouldThrowException_WhenSecondNumberIsNotANumber()
        {
            // Arrange
            var instance = new PortExtractor();

            // Act
            Action act = () => instance.GetPorts("1-b");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("b is not an valid int");
        }

    }
}
