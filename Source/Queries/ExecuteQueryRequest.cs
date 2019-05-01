/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Artifacts;
using Dolittle.PropertyBags;
using Dolittle.Tenancy;

namespace Dolittle.AspNetCore.Debugging.Queries
{
    /// <summary>
    /// Captures the information necessary to call the query coordinator
    /// </summary>
    public class ExecuteQueryRequest
    {
        /// <summary>
        /// The tenant to handle the query in
        /// </summary>
        /// <value></value>
        public TenantId Tenant { get; set; }

        /// <summary>
        /// The artifact identifying the query
        /// </summary>
        /// <value></value>
        public Artifact Artifact { get; set; }

        /// <summary>
        /// The actual query data
        /// </summary>
        /// <value></value>
        public PropertyBag Query { get; set; }
    }
}