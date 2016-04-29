using System;
using Twilio.Lookups;

namespace TwilioLookupBot.Services
{
    public class TwilioLookupService
    {
        public static Number GetNumberInfo(string number)
        {
            var lookupClient = new LookupsClient(Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"), Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN"));

            var response = lookupClient.GetPhoneNumber(number, true);

            return response;
        }
    }
}