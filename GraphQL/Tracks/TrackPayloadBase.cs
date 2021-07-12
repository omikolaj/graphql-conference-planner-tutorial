using GraphQL.Common;
using GraphQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Tracks
{
    public class TrackPayloadBase : Payload
    {
        protected TrackPayloadBase(Track track)
        {
            Track = track;
        }

        protected TrackPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {
        }

        public Track? Track { get; }
    }
}
