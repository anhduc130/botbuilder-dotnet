using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Schema;

namespace Microsoft.Bot.Builder.Integration.AspNet.Core
{
    internal class MultiplexingAdapter : BotAdapter, IBotFrameworkHttpAdapter
    {
        private readonly IEnumerable<IBotFrameworkHttpAdapter> _adapters;

        public MultiplexingAdapter(IEnumerable<IBotFrameworkHttpAdapter> adapters)
        {
            _adapters = adapters;
        }

        public override Task DeleteActivityAsync(ITurnContext turnContext, ConversationReference reference, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ProcessAsync(HttpRequest httpRequest, HttpResponse httpResponse, IBot bot, CancellationToken cancellationToken = default)
        {
            // get route from request
            // find right adapter
            await _adapters.First().ProcessAsync(httpRequest, httpResponse, bot, cancellationToken).ConfigureAwait(false);
        }

        public override Task ContinueConversationAsync(string botId, Activity continuationActivity, BotCallbackHandler callback, CancellationToken cancellationToken)
        {
            // map params (serviceurl) -> route
            // map route -> adapter
            return adapter.ContinueConversationAsync(botId, continuationActivity, callback, cancellationToken);
        }
    }
}
