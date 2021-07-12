using GraphQL.Data;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Tracks
{
    public record RenameTrackInput([ID(nameof(Track))] int Id, string Name);
}
