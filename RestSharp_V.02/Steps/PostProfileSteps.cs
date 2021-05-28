
using NUnit.Framework;
using RestSharp_V._02.Utilities;
using RestSharp;
using RestSharp_V._02.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestSharp_V._02.Steps
{
    [Binding]
    class PostProfileSteps
    {
        private Settings _settings;
        public PostProfileSteps(Settings settings) => _settings = settings;

        [Given(@"I perform operation for ""(.*)"" with body")]
       
         public void GivenIPerformOperationForWithBody(string url, Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _settings.Request.AddJsonBody(new { name = data.name.ToString() });
            _settings.Request = new RestRequest(url, Method.POST);            
            _settings.Request.AddUrlSegment("profileNo", ((int)data.profile));
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
            
        }
    }
}
