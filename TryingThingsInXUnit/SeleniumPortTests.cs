using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using System.Net.NetworkInformation;
using FluentAssertions;

namespace TryingThingsInXUnit
{
    public class SeleniumPortTests : IDisposable
    {

        private IWebDriver? _webDriver;
        public void Dispose()
        {
            if (this._webDriver != null)
            {
                this._webDriver.Quit();
                this._webDriver.Dispose();
            }    
        }

        [Theory()]
        [InlineData(4444)]
        [InlineData(4445)]
        [InlineData(4446)]
        public void ChromeDriver_WithPortSet_ShouldUseThatPort(int port)
        {
            // sanity check
            GetCurrentInUsePorts().Should().NotContain(port);
            
            // Arrange
            var service = ChromeDriverService.CreateDefaultService();
            service.Port = port;
            _webDriver = new ChromeDriver(service);

            // Act
            _webDriver.Navigate().GoToUrl("https://www.google.com");
            
            // Assert
            GetCurrentInUsePorts().Should().Contain(port);
        }

        [Fact()]
        public void ChromeDriver_WithNoPortSet_ShouldWork()
        {
            // sanity check

            // Arrange
            var service = ChromeDriverService.CreateDefaultService();
            _webDriver = new ChromeDriver(service);

            // Act
            _webDriver.Navigate().GoToUrl("https://www.google.com");

            // Assert
        }

        private int[] GetCurrentInUsePorts()
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            var portsInUse = tcpConnInfoArray.Select(x => x.LocalEndPoint.Port).ToArray();

            return portsInUse;
        }


    }
}
