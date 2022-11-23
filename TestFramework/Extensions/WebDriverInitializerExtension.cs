using Microsoft.Extensions.DependencyInjection;
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestFramework.Settings;

namespace TestFramework.Extensions
{
    public static  class WebDriverInitializerExtension
    {

        public static IServiceCollection UseWebDriverInitializer(
              this IServiceCollection services)
        {
            services.AddSingleton(ReadConfig());

            return services;
        }

        private static TestSettings ReadConfig()
        {
            var configFile = File
                            .ReadAllText(Path.GetDirectoryName(
                                Assembly.GetExecutingAssembly().Location)
                            + "/appsettings.json");

            var jsonSerializeOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jsonSerializeOptions.Converters.Add(new JsonStringEnumConverter());

            var testSettings = JsonSerializer.Deserialize<TestSettings>(configFile, jsonSerializeOptions);

            return testSettings;
        }
    }
}
