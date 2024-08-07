using APITestProject.POCO;
using RestSharp;

namespace APITestProject
{
    public class Tests
    {
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://localhost:7059/");
        }

        [Test]
        public void ResourceIsCreatedSuccessfully()
        {
            request = new RestRequest("api/Employees", Method.Post);
            request.AddJsonBody(new
            {
                Id = 1,
                Name = "John Doe",
                Designation = "Software Engineer",
                Email = "john.doe@example.com"
            });
            
            var response =  client.Execute(request);
            var locationHeader = response.Headers.FirstOrDefault(h => h.Name == "Location")?.Value.ToString();
            
            Assert.AreEqual((int)response.StatusCode, 201);
            Assert.NotNull(response);
            Assert.IsTrue(locationHeader != null);
            
        }

        [Test]
        public void FetchRecords()
        {
            request = new RestRequest("api/Employees",Method.Get);
            var response = client.Execute<List<Employee>>(request);
            List<Employee> records = response.Data.ToList();
            Assert.IsNotNull(records);
            Assert.AreEqual((int)response.StatusCode, 200);
        }

    }
}