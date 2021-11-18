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

        [Theory()]
        [InlineData("1-2", true, false)]
        [InlineData("1*2", false, true)]
        [InlineData("1z2", true, true)]
        public void GetPorts_ShouldThrowException_WhenUnsupportedCharacter(string input, bool enableMasking, bool enableRanges)
        {
            // Arrange
            var instance = new PortExtractor();
            instance.PortExtractorConfiguration.EnableMasking = enableMasking;
            instance.PortExtractorConfiguration.EnableRanges = enableRanges;

            // Act
            Action act = () => instance.GetPorts(input);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("input contained unsupported characters");
        }

        [Fact()]
        public void GetPorts_ShouldThrowException_WhenFirstNumberIsSmallerThenSecond()
        {
            // Arrange
            var instance = new PortExtractor();

            // Act
            Action act = () => instance.GetPorts("3-1");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Port two 1 should be larger then port one 3");
        }

        [Fact()]
        public void GetPorts_ShouldThrowException_WhenNoNumberPartsCouldBeFound()
        {
            // Arrange
            var instance = new PortExtractor();

            // Act
            Action act = () => instance.GetPorts("-");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Range part '-' contained no number parts");
        }

        [Theory()]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void GetPorts_ShouldThrowException_WhenNoInputWasGiven(string input)
        {
            // Arrange
            var instance = new PortExtractor();

            // Act
            Action act = () => instance.GetPorts(input);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithParameterName("input");
        }

        [Fact()]
        public void GetPorts_ShouldThrowException_WhenMaskingWasUsed()
        {
            // Arrange
            var instance = new PortExtractor();

            // Act
            Action act = () => instance.GetPorts("1*");

            // Assert
            act.Should().Throw<NotSupportedException>();
        }

        [Fact()]
        public void GetPorts_ShouldThrowException_ConfigurationWasInvalid()
        {
            // Arrange
            var instance = new PortExtractor();
            instance.PortExtractorConfiguration.MaskIndicator = '-';
            instance.PortExtractorConfiguration.PartSeparator = '-';

            // Act
            Action act = () => instance.GetPorts("1");

            // Assert
            act.Should().Throw<Exception>().WithMessage("PortExtractorConfiguration is invalid*");
        }

    }
}
