﻿using GraphQL.Common;
using GraphQL.Data;
using GraphQL.DataLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQL.Attendees
{
    public class CheckInAttendeePayload : AttendeePayloadBase
    {
        private int? _sessionId;

        public CheckInAttendeePayload(Attendee attendee, int sessionId)
            : base(attendee)
        {
            _sessionId = sessionId;
        }

        public CheckInAttendeePayload(UserError error)
            : base(new[] { error })
        {
        }

        public async Task<Session?> GetSessionAsync(SessionByIdDataLoader sessionById, CancellationToken cancellationToken)
        {
            if (_sessionId.HasValue)
            {
                return await sessionById.LoadAsync(_sessionId.Value, cancellationToken);
            }

            return null;
        }
    }
}
