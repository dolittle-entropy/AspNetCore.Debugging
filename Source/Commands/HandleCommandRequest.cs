/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Artifacts;
using Dolittle.PropertyBags;
using Dolittle.Tenancy;

namespace Dolittle.AspNetCore.Debugging.Commands
{
    /// <summary>
    /// Captures the information necessary to call the command coordinator
    /// </summary>
    public class HandleCommandRequest
    {
        /// <summary>
        /// The tenant to handle the command in
        /// </summary>
        /// <value></value>
        public TenantId Tenant { get; set; }

        /// <summary>
        /// The artifact identifying the command
        /// </summary>
        /// <value></value>
        public Artifact Artifact { get; set; }

        /// <summary>
        /// The actual command data
        /// </summary>
        /// <value></value>
        public PropertyBag Command { get; set; }
    }
}