/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Dolittle.Artifacts;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Events;
using Dolittle.Tenancy;

namespace Dolittle.AspNetCore.Debugging.Events
{
    /// <summary>
    /// Captures the information necessary to inject an event into the event store
    /// </summary>
    public class InjectEventRequest
    {
        /// <summary>
        /// The tenant to inject the event into
        /// </summary>
        /// <value></value>
        public TenantId Tenant { get; set; }

        /// <summary>
        /// The artifact identifying the event
        /// </summary>
        /// <value></value>
        public Artifact Artifact { get; set; }

        /// <summary>
        /// The event source to apply the event to
        /// </summary>
        /// <value></value>
        public EventSourceId EventSource { get; set; }

        /// <summary>
        /// The actual event data
        /// </summary>
        /// <value></value>
        public PropertyBag Event { get; set; }
        //public Dictionary<string,object> Event { get; set; }
    }
}