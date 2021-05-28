
using NUnit.Framework;
using RestSharp;
using RestSharp_V._02.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;


namespace RestSharp_V._02.Steps
{
    [Binding]
   public  class FileUpload_Steps
    {
        private Settings _settings;
            public FileUpload_Steps(Settings settings) => _settings = settings;


        [Given(@"I perform POST operation for ""(.*)""")]
        public void GivenIPerformPOSTOperationFor(string path)
        {
            _settings.Request = new RestRequest(path, Method.POST);
            _settings.Request.AddFile("file", "C://User", "image/jpeg");
            _settings.Response = _settings.RestClient.ExecuteAsPost(_settings.Request, "POST");
        }

        [Then(@"I see the file is being uploaded with response as (.*)")]
        public void ThenISeeTheFileIsBeingUploadedWithResponseAs(string status)
        {
            Assert.That(_settings.Response.StatusCode.ToString(), Is.EqualTo(status));
        }

    }
}
