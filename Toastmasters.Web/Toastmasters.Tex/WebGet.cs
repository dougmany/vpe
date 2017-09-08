﻿using Newtonsoft.Json;
using System;
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

        public Meeting GetMeeting(string path, out String error)
        {
            Meeting meeting = new Meeting();
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
                    meeting = JsonConvert.DeserializeObject<Meeting>(jsonTask.Result);             
                }
                else
                {
                    error = $"{response.StatusCode.ToString()}: {response.RequestMessage.RequestUri}";
                }
                return meeting;
            }
            catch (WebException e)
            {
                error = e.Message;
                return meeting;
            }
        }
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
        public String MeetingDateString { get { return MeetingDate.ToString("f"); } }
    }
}
