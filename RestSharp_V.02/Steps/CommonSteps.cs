using RestSharp_V._02.Utilities;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp_V._02.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestSharp_V._02.Steps
{
    [Binding]
    public class CommonSteps
    {
        private Settings _settings;

       
        // private IAuthenticator authenticator;

        public CommonSteps(Settings settings)
        {
            _settings = settings;
        }

        //public CommonSteps(Settings settings)=> _settings =settings;

        [Given(@"I get JWT authentication of User with following details")]
        
        public void GivenIGetJWTAuthenticationOfUserWithFollowingDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();

            _settings.Request = new RestRequest("Auth/SignIn", Method.POST);
            _settings.Request.AddJsonBody(new { user = (string)data.Email, password = (string)data.Password });           
         

            //Get access token
            _settings.Response = _settings.RestClient.ExecutePostAsync(_settings.Request).GetAwaiter().GetResult();
            var access_token = _settings.Response.GetResponseObject("token");
         
            // Authentication
            var authenticator = new JwtAuthenticator(access_token);
            _settings.RestClient.Authenticator = authenticator;
        }

    }
}
