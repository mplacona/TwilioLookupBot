using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using TwilioLookupBot.Services;

namespace TwilioLookupBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
if (message.Type == "Message")
{
    // Get number information
    var numberLookup = TwilioLookupService.GetNumberInfo(message.Text);

    // Check if the number entered is valid
    if (numberLookup.RestException != null)
    {
        return message.CreateReplyMessage("You entered an invalid phone number");
    }

    // Build a response using Markdown
    var response =
        $"The number **{numberLookup.NationalFormat}** has the following details:" +
        $"{Environment.NewLine}{Environment.NewLine}" +
        $"**Carrier:** {numberLookup.Carrier.Name}" +
        $"{Environment.NewLine}{Environment.NewLine}" +
        $"**Country Code:** {numberLookup.CountryCode}" +
        $"{Environment.NewLine}{Environment.NewLine}" +
        $"**Type:** {numberLookup.Carrier.Type}";
                
    return message.CreateReplyMessage(response);
}
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}