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

namespace GraphQL.Tracks
{
    [ExtendObjectType(Name = "Query")]
    public class TrackQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<Track> GetTracks([ScopedService] ApplicationDbContext context) => context.Tracks.OrderBy(t => t.Name);

        [UseApplicationDbContext]
        public Task<Track> GetTrackByNameAsync(
            string name,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            context.Tracks.FirstAsync(t => t.Name == name);

        [UseApplicationDbContext]
        public async Task<IEnumerable<Track>> GetTrackByNamesAsync(
            string[] names,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            await context.Tracks.Where(t => names.Contains(t.Name)).ToListAsync();

        public Task<Track> GetTrackByIdAsync(
            [ID(nameof(Track))] int id,
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
            trackById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Track>> GetTracksByIdAsync(
            [ID(nameof(Track))] int[] ids,
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
            await trackById.LoadAsync(ids, cancellationToken);
    }
}
