using GraphQL.Common;
using GraphQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Tracks
{
    public class RenameTrackPayload : TrackPayloadBase
    {
        public RenameTrackPayload(Track track)
            : base(track)
        {
        }

        public RenameTrackPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}
