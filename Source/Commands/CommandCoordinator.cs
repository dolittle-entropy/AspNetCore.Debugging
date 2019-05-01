/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Artifacts;
using Dolittle.Commands;
using Dolittle.Execution;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Commands;
using Dolittle.Tenancy;
using IRuntimeCommandCoordinator = Dolittle.Commands.Coordination.ICommandCoordinator;

namespace Dolittle.AspNetCore.Debugging.Commands
{
    /// <summary>
    /// An implementation of <see cref="ICommandCoordinator"/>
    /// </summary>
    public class CommandCoordinator : ICommandCoordinator
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IRuntimeCommandCoordinator _runtimeCommandCoordinator;

        /// <summary>
        /// Instanciates a new <see cref="CommandCoordinator"/>
        /// </summary>
        public CommandCoordinator(
            IExecutionContextManager executionContextManager,
            IRuntimeCommandCoordinator runtimeCommandCoordinator
        )
        {
            _executionContextManager = executionContextManager;
            _runtimeCommandCoordinator = runtimeCommandCoordinator;
        }

        /// <inheritdoc/>
        public CommandResult Handle(TenantId tenant, ICommand command)
        {
            _executionContextManager.CurrentFor(tenant);
            return _runtimeCommandCoordinator.Handle(command);
        }
    }
}