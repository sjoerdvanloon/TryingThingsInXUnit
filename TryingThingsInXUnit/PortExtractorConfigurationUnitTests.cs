using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TryingThingsInXUnit
{
    public class PortExtractorConfigurationUnitTests
    {

        [Fact()]
        public void Constructor_ShouldCreateInstance()
        {
            // Arrange

            // Act
            var instance = new PortExtractorConfiguration();

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact()]
        public void Validate_ShouldReturnValid_WhenEverythingIsOkay()
        {
            // Arrange
            var instance = new PortExtractorConfiguration();

            // Act
            var result = instance.Validate();

            // Assert
            result.Should().NotBeNull();
            result.Valid.Should().BeTrue();
            result.Reason.Should().BeNullOrWhiteSpace();
        }


        [Fact()]
        public void Validate_ShouldReturnInValid_WhenRangeAndPartIndicatorsAreTheSame()
        {
            // Arrange
            var instance = new PortExtractorConfiguration();


            // Act
            instance.RangeIndicator = '-';
            instance.PartSeparator = '-';

            var result = instance.Validate();

            // Assert
            result.Should().NotBeNull();
            result.Valid.Should().BeFalse();
            result.Reason.Should().Be("PartSeparator and RangeIndicator are both -");
        }

        [Fact()]
        public void Validate_ShouldReturnInValid_WhenMaskAndPartIndicatorsAreTheSame()
        {
            // Arrange
            var instance = new PortExtractorConfiguration();

            // Act
            instance.MaskIndicator = '*';
            instance.PartSeparator = '*';

            var result = instance.Validate();

            // Assert
            result.Should().NotBeNull();
            result.Valid.Should().BeFalse();
            result.Reason.Should().Be("PartSeparator and MaskIndicator are both *");
        }

        [Fact()]
        public void Validate_ShouldReturnInValid_WhenMaskAndRangeIndicatorsAreTheSame()
        {
            // Arrange
            var instance = new PortExtractorConfiguration();


            // Act
            instance.MaskIndicator = '-';
            instance.RangeIndicator = '-';

            var result = instance.Validate();

            // Assert
            result.Should().NotBeNull();
            result.Valid.Should().BeFalse();
            result.Reason.Should().Be("MaskIndicator and RangeIndicator are both -");
        }

    }
}
