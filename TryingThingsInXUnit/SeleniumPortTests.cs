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
using System.Diagnostics;

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

                //var processes = Process.GetProcessesByName("ChromeDriver.exe");
                //foreach (var process in processes)
                //{
                //    process.Kill(true);
                //}
            }    
        }

        [Theory(Skip ="For live unit testing")]
        [InlineData(4444, true)]
        [InlineData(4444, false)]
        [InlineData(4445, false)]
        [InlineData(4446, false)]
        public void ChromeDriver_WithPortSet_ShouldUseThatPort(int port, bool again)
        {
            // sanity check
            GetCurrentInUsePorts().Should().NotContain(port); // This fails when it is already in use
            
            // Arrange
            var service = ChromeDriverService.CreateDefaultService("C:\\SeleniumDrivers");
            service.Port = port;
            _webDriver = new ChromeDriver(service);

            // Act
            _webDriver.Navigate().GoToUrl("https://www.google.com");
            
            // Assert
            GetCurrentInUsePorts().Should().Contain(port);
        }

        [Fact(Skip = "lut")]
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
