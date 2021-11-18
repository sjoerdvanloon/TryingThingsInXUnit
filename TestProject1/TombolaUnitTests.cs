using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class TombolaUnitTests
    {

        [Fact()]
        public void Constructor_ShouldThrowException_WhenItemsAreNull()
        {
            // Arrange

            // Act
            Action act = () => new Tombola<string>(null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact()]
        public void Draw_ShouldReturnRandomItem()
        {
            // Arrange
            var instance = new Tombola<int>(new int[] { 1,2});

            // Act
            var drawn =  instance.Draw();

            // Assert
            drawn.Should().BeOneOf(new int[] { 1,2 });
        }

        [Fact()]
        public void Draw_ShouldReturnNewNumberSecondTime()
        {
            // Arrange
            var instance = new Tombola<int>(new int[] { 1, 2 });
            var drawnA = instance.Draw();

            // Act
            var drawnB = instance.Draw();


            // Assert
            if (drawnA == 1)
            {
                drawnB.Should().Be(2);
            }
            else
            {
                drawnB.Should().Be(1);
            }
        }

        [Fact()]
        public void Draw_ShouldReturnFirstNumberThirdTime_BecauseItLooped()
        {
            // Arrange
            var instance = new Tombola<int>(new int[] { 1, 2 });
            var drawnA = instance.Draw();
            var drawnB = instance.Draw();

            // Act
            var drawnC = instance.Draw();

            // Assert
            drawnC.Should().Be(drawnA);
        }

    }
}
