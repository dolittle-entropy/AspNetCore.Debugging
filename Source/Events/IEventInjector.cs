/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Artifacts;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Events;
using Dolittle.Tenancy;

namespace Dolittle.AspNetCore.Debugging.Events
{
    /// <summary>
    /// Represents an injector capable of inserting events directly into the event store and trigger event processors
    /// </summary>
    public interface IEventInjector
    {

        /// <summary>
        /// Insert event into the event store and trigger event processors
        /// </summary>
        void InjectEvent(TenantId tenant, Artifact artifact, EventSourceId eventSourceId, PropertyBag @event);
    }
}