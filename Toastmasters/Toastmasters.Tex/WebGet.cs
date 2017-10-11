using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Toastmasters.Tex
{
    class WebGet
    {
        static HttpClient client = new HttpClient();
        public WebGet(String url)
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public T GetMeeting<T>(string path, out String error)
        {
            var data = (T)Activator.CreateInstance(typeof(T));
            
            error = "";
            try
            {
                var responseTask = client.GetAsync(path);
                responseTask.Wait();
                var response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonTask = response.Content.ReadAsStringAsync();
                    jsonTask.Wait();
                    data = JsonConvert.DeserializeObject<T>(jsonTask.Result);             
                }
                else
                {
                    error = $"{response.StatusCode.ToString()}: {response.RequestMessage.RequestUri}";
                }
                return data;
            }
            catch (WebException e)
            {
                error = e.Message;
                return data;
            }
        }

        public IEnumerable<T> GetMeetingList<T>(string path, out String error)
        {
            var data = new List<T>();

            error = "";
            try
            {
                var responseTask = client.GetAsync(path);
                responseTask.Wait();
                var response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonTask = response.Content.ReadAsStringAsync();
                    jsonTask.Wait();
                    return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonTask.Result);
                }
                else
                {
                    error = $"{response.StatusCode.ToString()}: {response.RequestMessage.RequestUri}";
                }
                return data;
            }
            catch (WebException e)
            {
                error = e.Message;
                return data;
            }
        }
    }

    public class MeetingAgenda
    {
        public MeetingAgenda()
        {
            MeetingDate = DateTime.Now;
            Toastmaster = "";
            Inspirational = "";
            Joke = "";
            GeneralEvaluator = "";
            EvaluatorI = "";
            EvaluatorII = "";
            Timer = "";
            BallotCounter = "";
            Grammarian = "";
            TableTopics = "";
            SpeakerI = "";
            SpeakerII = "";
            President = "";
            Sargent = "";
            AbsentI = "";
            AbsentII = "";
        }  

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

        public DateTime NextMeetingDate { get; set; }
        public String NextToastmaster { get; set; }
        public String NextInspirational { get; set; }
        public String NextJoke { get; set; }
        public String NextGeneralEvaluator { get; set; }
        public String NextEvaluatorI { get; set; }
        public String NextEvaluatorII { get; set; }
        public String NextTimer { get; set; }
        public String NextBallotCounter { get; set; }
        public String NextGrammarian { get; set; }
        public String NextTableTopics { get; set; }
        public String NextSpeakerI { get; set; }
        public String NextSpeakerII { get; set; }
        public String NextPresident { get; set; }
        public String NextSargent { get; set; }

        public String NextMeetingDateString { get { return NextMeetingDate.ToString("d"); } }
    }

    public class Meeting
    {
        public Meeting()
        {
            MeetingDate = DateTime.Now;
            Toastmaster = "";
            Inspirational = "";
            Joke = "";
            GeneralEvaluator = "";
            EvaluatorI = "";
            EvaluatorII = "";
            Timer = "";
            BallotCounter = "";
            Grammarian = "";
            TableTopics = "";
            SpeakerI = "";
            SpeakerII = "";
            President = "";
            Sargent = "";
            AbsentI = "";
            AbsentII = "";
        }

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
        public String MeetingDateString { get { return MeetingDate.ToString("M"); } }
    }
}
