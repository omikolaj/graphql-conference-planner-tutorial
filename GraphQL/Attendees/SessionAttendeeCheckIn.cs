using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Extensions;
using HotChocolate;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQL.Attendees
{
    public class SessionAttendeeCheckIn
    {
        public SessionAttendeeCheckIn(int attendeeId, int sessionId)
        {
            AttendeeId = attendeeId;
            SessionId = sessionId;
        }

        [ID(nameof(Attendee))]
        public int AttendeeId { get; }

        [ID(nameof(Session))]
        public int SessionId { get; }

        [UseApplicationDbContext]
        public async Task<int> CheckInCountAsync
            ([ScopedService] ApplicationDbContext context, CancellationToken cancellationToken) 
                => await context.Sessions
                    .Where(session => session.Id == SessionId)
                    .SelectMany(session => session.SessionAttendees)
                    .CountAsync(cancellationToken);

        public Task<Attendee> GetAttendeeAsync(AttendeeByIdDataLoader attendeeById, CancellationToken cancellationToken) => attendeeById.LoadAsync(AttendeeId, cancellationToken);

        public Task<Session> GetSessionAsync(SessionByIdDataLoader sessionById, CancellationToken cancellationToken) => sessionById.LoadAsync(AttendeeId, cancellationToken);
    }
}
