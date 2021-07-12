using GraphQL.Common;
using GraphQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Sessions
{
    public class AddSessionPayload : SessionPayloadBase
    {
        public AddSessionPayload(Session session) : base(session)
        {
        }

        public AddSessionPayload(UserError error)
            : base(new[] { error })
        {
        }
    }

}
