using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Toastmasters.Tex
{
    class WebGet
    {
        static HttpClient client = new HttpClient();
        public WebGet()
        {
            client.BaseAddress = new Uri("http://localhost:8656/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Meeting> GetMeetingAsync(string path)
        {
            Meeting meeting = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                meeting = JsonConvert.DeserializeObject<Meeting>(json);             
            }
            return meeting;
        }
    }

    public class Meeting
    {
        public DateTime MeetingDate { get; set; }
        public String Toastmaster { get; set; }
        public String Inspirational { get; set; }
        public String Joke { get; set; }
        public String GeneralEvaluator { get; set; }
        public String EvaluatorI { get; set; }
        public String EvaluatorII { get; set; }
        public String Timer { get; set; }
        public String BallotCounter { get; set; }
        public String Grammarian { get; set; }
        public String TableTopics { get; set; }
        public String SpeakerI { get; set; }
        public String SpeakerII { get; set; }
        public String President { get; set; }
        public String Sargent { get; set; }
        public String AbsentI { get; set; }
        public String AbsentII { get; set; }
        public String MeetingDateString { get { return MeetingDate.ToString("f"); } }
    }
}
