using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Attendees;
using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Sessions;
using GraphQL.Speakers;
using GraphQL.Tracks;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQL
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));

            services.AddGraphQLServer()
                    .AddQueryType(d => d.Name("Query"))
                        .AddTypeExtension<SpeakerQueries>()
                        .AddTypeExtension<AttendeeQueries>()
                        .AddTypeExtension<SessionQueries>()
                        .AddTypeExtension<TrackQueries>()
                    .AddMutationType(d => d.Name("Mutation"))
                        .AddTypeExtension<SpeakerMutations>()
                        .AddTypeExtension<SessionMutations>()
                        .AddTypeExtension<TrackMutations>()
                        .AddTypeExtension<AttendeeMutations>()
                    .AddSubscriptionType(d => d.Name("Subscription"))
                        .AddTypeExtension<SessionSubscriptions>()
                        .AddTypeExtension<AttendeeSubscriptions>()
                    .AddType<SpeakerType>()
                    .AddType<AttendeeType>()
                    .AddType<SessionType>()
                    .AddType<TrackType>()
                    .EnableRelaySupport()
                    .AddFiltering()
                    .AddSorting()
                    .AddInMemorySubscriptions()
                    .AddDataLoader<SessionByIdDataLoader>()
                    .AddDataLoader<SpeakerByIdDataLoader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
