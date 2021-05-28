using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using RestSharp;
using RestSharp_V._02.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;

namespace RestSharp_V._02.Hooks
{
    [Binding]
    class TestInitialize
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private static ExtentKlovReporter klov;

        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        private Settings _settings;
        

        public TestInitialize(Settings settings, FeatureContext featureContext, ScenarioContext scenarioContext)
        {

            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
           _settings = settings;
        }


        [BeforeScenario]
        public void TestSetup()
        {
           //_settings.BaseUrl = new Uri(ConfigurationManager.AppSettings["baseUrl"].ToString());
            _settings.BaseUrl =new Uri("https://qaclient01.qa.wgtcorp.net/api/");
            _settings.RestClient.BaseUrl = _settings.BaseUrl;

        }
        [BeforeTestRun]
        public static void InitializeReport()
        {
            string file = "ExtentReport.html";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            var htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.Theme= AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            extent = new ExtentReports();

            //historical Report
            // klov = new ExtentKlovReporter();
            //klov.InitMongoDbConnection("localhost", 27017);
            //klov.ProjectName = "RestSharp_V.01";
            //klov.KlovUrl = "http://localhost : 5689";
            //klov.ReportName = "QA Test" + DateTime.Now.ToString();

            extent.AttachReporter(htmlReporter);
        }
        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }
        
        [AfterStep]
        public  void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
           // PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("TestStatus", BindingFlags.Instance | BindingFlags.NonPublic);
            //MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
           // object TestResult = getter.Invoke(ScenarioContext.Current, null);

            if (_scenarioContext.TestError == null)
            {

                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
            }
            // Pending Status
            /*if(TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            }*/
        }
        [BeforeScenario]
        public void Initialize()
        {
            //Create dynamic feature name 
            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            // Create dynamic scenario name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }
    }
}
