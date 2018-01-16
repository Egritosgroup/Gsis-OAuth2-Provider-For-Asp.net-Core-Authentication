using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace EgritosGroup.Auth.Provider.OAuthGsis
{
    public class GsisAuthenticationHandler : OAuthHandler<GsisAuthenticationOptions>
    {
        public GsisAuthenticationHandler(
            IOptionsMonitor<GsisAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        public static async Task LogOutAsync(string gsisAccessToken, string gsisExpiresAt, string gsisTokenType, bool useTestEnv = false)
        {
            var address = useTestEnv ? GsisAuthenticationDefaults.LogOutEndpointTest : GsisAuthenticationDefaults.LogOutEndpoint;
            //var request = new HttpRequestMessage(HttpMethod.Get, address);

            var request = new HttpRequestMessage(HttpMethod.Post, address);
            var parameters = new Dictionary<string, string> {
                //{ "token", gsisAccessToken },
                { "cleintid", gsisAccessToken },
                //{ "param2", "2" }
            };
            request.Content = new FormUrlEncodedContent(parameters);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", gsisAccessToken);

            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                //Logger.LogError("An error occurred while retrieving the user profile in GSIS: the remote server " +
                //                "returned a {Status} response with the following payload: {Headers} {Body}.",
                //                /* Status: */ response.StatusCode,
                //                /* Headers: */ response.Headers.ToString(),
                //                /* Body: */ await response.Content.ReadAsStringAsync());
            }
            //throw new NotImplementedException();
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity,
            AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var address = Options.UserInformationEndpoint;

            //// If at least one field is specified,
            //// append the fields to the endpoint URL.
            //if (Options.Fields.Count != 0)
            //{
            //    address = address.Insert(address.LastIndexOf("~") + 1, $":({ string.Join(",", Options.Fields)})");
            //}

            var request = new HttpRequestMessage(HttpMethod.Get, address);
            request.Headers.Add("x-li-format", "json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

            var response = await Backchannel.SendAsync(request, Context.RequestAborted);
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError("An error occurred while retrieving the user profile in GSIS: the remote server " +
                                "returned a {Status} response with the following payload: {Headers} {Body}.",
                                /* Status: */ response.StatusCode,
                                /* Headers: */ response.Headers.ToString(),
                                /* Body: */ await response.Content.ReadAsStringAsync());

                throw new HttpRequestException("An error occurred while retrieving the user profile.");
            }

            var stringContent = await response.Content.ReadAsStringAsync();
            var xml = new System.Xml.XmlDocument();
            xml.LoadXml(stringContent);
            var userJson = JsonConvert.SerializeXmlNode(xml.FirstChild.FirstChild);
            var user = JObject.Parse(userJson);
            var userInfo = user.GetValue("userinfo");

            var claims = new Dictionary<string, string>();
            foreach (var item in userInfo)
            {
                var name = (((JProperty)item).Name).Replace("@", "");
                var value = ((JProperty)item).Value.ToString().Trim();
                claims.Add(name, value);
                //item.Type
                //context.Identity.AddClaim(new Claim(name, value, ClaimValueTypes.String, context.Options.ClaimsIssuer));
            }
            string dicUserjson = JsonConvert.SerializeObject(claims, Formatting.None);
            var payload = JObject.Parse(dicUserjson);
            
            var principal = new ClaimsPrincipal(identity);
            var context = new OAuthCreatingTicketContext(principal, properties, Context, Scheme, Options, Backchannel, tokens, payload);
            context.RunClaimActions(payload);

            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }
    }
}
