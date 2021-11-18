using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xunit;

namespace TryingThingsInXUnit
{
    public class FluentAssertionsUnitTests
    {
        [Fact]
        async public Task CheckIfAsyncThrow_GivesCorrectException()
        {
            // act
            Func<Task> act = () => TestMe();

            await act.Should().ThrowAsync<Exception>().WithMessage("Sergej");
            // await act.Should().ThrowAsync<Exception>().WithMessage("Sjoerd");

        }

        [Fact]
        async public Task CheckIfType_IsCorrect()
        {
            await Task.Delay(0);
            // act
            object x = new Sergej();

            x.Should().BeOfType<Sergej>().And.BeEquivalentTo(new Sergej());
            //x.Should().BeOfType<Sergej>().And.BeSameAs(new Sergej());


        }

        public class Sergej
        {
            public int Age { get; set; } = 45;
        }

        public async Task TestMe()
        {
            await Task.Delay(0);

            throw new Exception("Sergej");
        }
    }
}