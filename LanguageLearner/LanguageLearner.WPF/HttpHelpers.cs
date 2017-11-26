using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLearner.WPF.Helpers
{
    public static class HttpHelpers
    {
        public static string Username;
        public static string Password;

        public static HttpClient InitializeHttpClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:57696/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(@"{ ""UserName"": """ + Username + @""",  ""Password"": """ + Password + @""",  ""RememberMe"": true}", Encoding.UTF8, "application/json");
            var result = client.PostAsync("api/Account/Login", content).Result;

            HttpHelpers.IsSuccessfullRequest(result, "Váratlan hiba történt, a bejelentkezés nem sikerült. Kérjük, próbálja később!");

            return client;
        }

        public static bool IsSuccessfullRequest(HttpResponseMessage response, String ErrorMessage)
        {
            bool success = response.IsSuccessStatusCode;
            if (!success)
                System.Windows.Forms.MessageBox.Show(ErrorMessage, "Probléma a szerkesztésben");

            return success;
        }

    }
}
