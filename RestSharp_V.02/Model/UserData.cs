using Consul;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharp_V._02.Model
{
    public class Role
    {
        public string roleName { get; set; }
        public object permissions { get; set; }
        public List<string> rolePermissions { get; set; }
        public string roleId { get; set; }
        public object permissionIds { get; set; }
        public List<string> rolePermissionIds { get; set; }
    }

    public class Site
    {
        public string siteName { get; set; }
        public string siteId { get; set; }
        public object roleName { get; set; }
        public object permissions { get; set; }
        public object rolePermissions { get; set; }
        public string roleId { get; set; }
        public object permissionIds { get; set; }
        public object rolePermissionIds { get; set; }
    }
     
    public class Data
    {
        public string userId { get; set; }
        public object title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public object phoneNumber { get; set; }
        public string status { get; set; }
        public object organization { get; set; }
        public bool isUserLevel { get; set; }
        public object defaultSiteId { get; set; }
        public List<Role> roles { get; set; }
        public List<Site> sites { get; set; }
    }

    public class Values
    {
        public string apiUrlWithToken { get; set; }
        public List<List<double>> defaultBounds { get; set; }
        public int zoomLevel { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
        public Values values { get; set; }
        public bool success { get; set; }
        public List<object> messages { get; set; }
    }

}
