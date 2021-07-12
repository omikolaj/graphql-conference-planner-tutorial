using GraphQL.Common;
using GraphQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Tracks
{
    public class AddTrackPayload : TrackPayloadBase
    {
        public AddTrackPayload(Track track) : base(track) { }

        public AddTrackPayload(IReadOnlyList<UserError> errors) : base(errors) { }
    }
}
