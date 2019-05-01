/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Artifacts;
using Dolittle.Commands;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Commands;
using Dolittle.Tenancy;

namespace Dolittle.AspNetCore.Debugging.Commands
{
    /// <summary>
    /// Represents a coordinator capable of handling commands
    /// </summary>
    public interface ICommandCoordinator
    {
        /// <summary>
        /// Handle a command
        /// </summary>
        CommandResult Handle(TenantId tenant, ICommand command);
    }
}