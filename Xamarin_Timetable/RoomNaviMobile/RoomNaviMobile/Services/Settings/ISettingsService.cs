using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomNaviMobile.Services.Settings
{
    public interface ISettingsService
    {
        string AuthAccessToken { get; set; }
        string AuthIdToken { get; set; }
        bool UseMocks { get; set; }
        string IdentityEndpointBase { get; set; }
        string GatewayShoppingEndpointBase { get; set; }
        string GatewayMarketingEndpointBase { get; set; }
        bool UseFakeLocation { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
        bool AllowGpsLocation { get; set; }

        bool GetValueOrDefault(string key, bool defaultValue);
        string GetValueOrDefault(string key, string defaultValue);
        Task AddOrUpdateValue(string key, bool value);
        Task AddOrUpdateValue(string key, string value);
    }
}
