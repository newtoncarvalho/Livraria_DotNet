using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using WebApiLivraria;

namespace WebApiLivrariaTest
{
    [TestClass]
    public class UnitTest1
    {
        private TestServer _server;
        private HttpClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            _server = new TestServer(new WebHostBuilder().UseEnvironment("Test")
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }


        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
