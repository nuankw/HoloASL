using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace MyBot
{
    public class MyBot : IBot
    {
        public async Task OnTurn(ITurnContext context)
        {
            ConversationContext.userMsg = context.Activity.Text;

            if (context.Activity.Type is ActivityTypes.Message)
            {
                if (string.IsNullOrEmpty(ConversationContext.userMsg))
                {
                    await context.SendActivity($" {ConversationContext.userMsg}");
                }
            }
            else
            {
                ConversationContext.userMsg = string.Empty;
                await context.SendActivity($"Welcome!\n Please click to record");
            }

        }
    }
}
