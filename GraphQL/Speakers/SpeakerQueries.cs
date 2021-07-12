﻿using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Extensions;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQL
{
    [ExtendObjectType(Name = "Query")]
    public class SpeakerQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<Speaker> GetSpeakers([ScopedService] ApplicationDbContext context) => context.Speakers.OrderBy(t => t.Name);

        public Task<Speaker> GetSpeakerByIdAsync([ID(nameof(Speaker))] int id, SpeakerByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync([ID(nameof(Speaker))] int[] ids, SpeakerByIdDataLoader dataLoader, CancellationToken cancellationToken) => await dataLoader.LoadAsync(ids, cancellationToken);
    }
}
