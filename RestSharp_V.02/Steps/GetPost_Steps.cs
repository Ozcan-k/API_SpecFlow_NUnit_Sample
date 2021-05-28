using NUnit.Framework;
using RestSharp_V._02.Utilities;
using RestSharp;
using RestSharp_V._02.Base;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using RestSharp_V._02.Model;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestSharp_V._02.Steps
{
    [Binding]
    public class GetPost_Steps
    {
        private Settings _settings;


        public GetPost_Steps(Settings settings) => _settings = settings;
        

        [Given(@"I perform GET operation for ""(.*)""")]
        public void GivenIPerformGETOperationFor(string url)
        {

            _settings.Request = new RestRequest(url, Method.GET);           
           _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Data>(_settings.Request).GetAwaiter().GetResult();
           
        }

        [Given(@"Verify statuse code should be OK")]
        public void GivenVerifyStatuseCodeShouldBeOK()
        {
            Assert.That(_settings.Response.StatusCode.ToString, Is.EqualTo("OK"));
        }

               

        [Then(@"I should see the (.*) name as (.*)")]
        public void ThenIShouldSeeTheNameAs (string key ,string value)
        {

            var alldata = Libraries.DeserializeResponse(_settings.Response);


            foreach(var dt in alldata)
            {
                if (dt.Key == "data")
                {
                    var name = JsonConvert.DeserializeObject<Data>(dt.Value);
                    if(name != null)
                    {
                        Assert.That(name.firstName, Is.EqualTo(value));
                        Assert.That(name.lastName, Is.EqualTo("wgtcorp"));
                        
                            
                    }
                }
            }                    

        }


    }
    }