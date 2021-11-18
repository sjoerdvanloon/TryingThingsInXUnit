using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        async public Task Test1()
        {
            // act
            Func<Task> act = () => TestMe();

            await act.Should().ThrowAsync<Exception>().WithMessage("Sergej");
            // await act.Should().ThrowAsync<Exception>().WithMessage("Sjoerd");

        }

        [Fact]
        async public Task Test2()
        {
            await Task.Delay(0);
            // act
            object x = new Sergej();

            x.Should().BeOfType<Sergej>().And.BeEquivalentTo(new Sergej());
            //x.Should().BeOfType<Sergej>().And.BeSameAs(new Sergej());


        }

        [Theory()]
        [InlineData(456)]
        public void FindIpPort(int port)
        {
            bool isAvailable = true;

            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    isAvailable = false;
                    break;
                }
            }



            // At this point, if isAvailable is true, we can proceed accordingly.
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