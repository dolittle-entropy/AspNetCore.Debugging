/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Artifacts;
using Dolittle.Queries;
using Dolittle.Tenancy;

namespace Dolittle.AspNetCore.Debugging.Queries
{
    /// <summary>
    /// Represents a coordinator capable of executing queries
    /// </summary>
    public interface IQueryCoordinator
    {
        /// <summary>
        /// Execute a query
        /// </summary>
        QueryResult Handle(TenantId tenant, IQuery query);
    }
}