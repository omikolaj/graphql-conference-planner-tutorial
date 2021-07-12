using GraphQL.Data;
using GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQL.Attendees
{
    [ExtendObjectType(Name = "Subscription")]
    public class AttendeeSubscriptions
    {
        [Subscribe(With = nameof(SubscribeToOnAttendeeCheckedInAsync))]
        public SessionAttendeeCheckIn OnAttendeeCheckedIn([ID(nameof(Session))] int sessionId,[EventMessage] int attendeeId, SessionByIdDataLoader sessionById, CancellationToken cancellationToken) => new SessionAttendeeCheckIn(attendeeId, sessionId);

        public async ValueTask<ISourceStream<int>> SubscribeToOnAttendeeCheckedInAsync
            (int sessionId, [Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken) 
                => await eventReceiver.SubscribeAsync<string, int>("OnAttendeeCheckedIn_" + sessionId, cancellationToken);
    }
}
