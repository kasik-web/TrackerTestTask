
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.RegularExpressions;
using TrackerTestTask.Data;
using TrackerTestTask.Func;
using TrackerTestTask.Models;
using TrackerTestTask.Viber;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TrackerTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackLocationController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly int interval = 30; // для разделения прогулок              

        public TrackLocationController(AppDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] object data)
        {
            string viberMsg = "";
            string receiver = "";
            string msg = "";
            string imei = "";
            object message = new Object();
            string url = _config.GetValue<string>("ViberBot:Url");
            string accessToken = _config.GetValue<string>("ViberBot:Token");

            JObject jsonData = JsonConvert.DeserializeObject<JObject>(data.ToString());
            string viberEvent = jsonData["event"].ToString();

            if (viberEvent == "conversation_started")
            {
                receiver = jsonData["user"]["id"].ToString();
                msg = "\t\t\t\tВітаю!\n\nВведіть IMEI номер та отримайте данні про ваші прогулянки!";
                message = ViberSender.SetMessage(receiver, msg);
            }

            if (viberEvent == "message")
            {
                viberMsg = jsonData["message"]["text"].ToString();
                receiver = jsonData["sender"]["id"].ToString();
                Regex pattern = new Regex(@"^[1-9]\d{14}$");

                if (viberMsg.Contains("top10"))
                {
                    imei = viberMsg.Substring(viberMsg.IndexOf("&") + 1);

                    List<TrackLocations> trackLocations = _dbContext.TrackLocations.Where(x => x.IMEI == imei).OrderBy(x => x.DateTrack)
                                                .Select(x => new TrackLocations
                                                { IMEI = x.IMEI, Latitude = x.Latitude, Longitude = x.Longitude, DateTrack = x.DateTrack }).ToList();

                    List<WalkInfo> top10Walks = WalkCalculate.GetWalks(trackLocations, interval).OrderByDescending(w => w.WalkDistance).Take(10).ToList();
                    
                    message = ViberSender.SetMessage(receiver, top10Walks);
                }
                else if (viberMsg == "back")
                {
                    receiver = jsonData["sender"]["id"].ToString();
                    msg = "Введіть IMEI номер та отримайте данні про ваші прогулянки!";
                    message = ViberSender.SetMessage(receiver, msg);
                }
                else if (pattern.IsMatch(viberMsg))
                {
                    imei = viberMsg;

                    List<TrackLocations> trackLocations = _dbContext.TrackLocations.Where(x => x.IMEI == imei).OrderBy(x => x.DateTrack)
                                                .Select(x => new TrackLocations
                                                { IMEI = x.IMEI, Latitude = x.Latitude, Longitude = x.Longitude, DateTrack = x.DateTrack }).ToList();

                    var allWalks = WalkCalculate.GetWalks(trackLocations, interval).ToList();
                    var totalDuration = allWalks.Aggregate(TimeSpan.Zero, (acc, w) => acc + TimeSpan.Parse(w.Duration));

                    msg = $"Всього прогулянок: {allWalks.Count}\nВсього кілометрів: {Math.Round(allWalks.Sum(w => w.WalkDistance), 2)}\nВсього часу: {totalDuration} хв.";
                    message = ViberSender.SetMessage(receiver, msg, imei);
                }
                else
                {
                    msg = "Ой, халепа, це не схоже на IMEI:-(\nСпробуйте ще раз, будь ласка";
                    message = ViberSender.SetMessage(receiver, msg);
                }
            };

                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("X-Viber-Auth-Token", accessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonSerializer.Serialize(message), ParameterType.RequestBody);
               

                RestResponse response = await client.ExecuteAsync(request);

            return Ok();
        }        
    }
}
