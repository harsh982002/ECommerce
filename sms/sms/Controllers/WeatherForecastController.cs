using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace sms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        public async Task<IHttpActionResult> SendSms([FromBody] SmsData smsData)
        {
            // Twilio API credentials
            string accountSid = "dc0ae087-3e7d-4a02-bc8c-6f8fb180febb";
            string authToken = "LpRJpBW/nW4mjHnX2PgmrI8+j/wvfCbF";

            // Create an HTTP client
            HttpClient client = new HttpClient();

            // Set the basic authentication header using your Twilio credentials
            string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{accountSid}:{authToken}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

            // Create the request parameters
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("To", smsData.RecipientNumber),
                new KeyValuePair<string, string>("From", "+27724901815"),
                new KeyValuePair<string, string>("Body", smsData.Message)
            });

            try
            {
                // Send the HTTP POST request to the Twilio API
                var response = await client.PostAsync("https://api.twilio.com/2010-04-01/Accounts/YOUR_ACCOUNT_SID/Messages.json", requestContent);

                // Handle the response
                if (response.IsSuccessStatusCode)
                {
                    return (IHttpActionResult)Ok("SMS sent successfully!");
                }
                else
                {
                    return (IHttpActionResult)BadRequest($"Failed to send SMS. Response status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                // Dispose of the HTTP client
                client.Dispose();
            }
        }
        public class SmsData
        {
            public string RecipientNumber { get; set; }
            public string Message { get; set; }
        }
        private IHttpActionResult InternalServerError(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
