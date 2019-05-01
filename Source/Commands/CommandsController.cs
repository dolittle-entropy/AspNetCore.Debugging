/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Commands;
using Dolittle.Events;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Events;
using Microsoft.AspNetCore.Mvc;

namespace Dolittle.AspNetCore.Debugging.Commands
{
    /// <summary>
    /// Represents a debugging API endpoint for working with <see cref="ICommand">commands</see>
    /// </summary>
    [Route("api/Dolittle/Debugging/Commands")]
    public class CommandsController : ControllerBase
    {
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly IObjectFactory _objectFactory;
        readonly ICommandCoordinator _coordinator;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandsController"/>
        /// </summary>
        /// <param name="artifactTypeMap"></param>
        /// <param name="objectFactory"></param>
        /// <param name="coordinator"></param>
        public CommandsController(
            IArtifactTypeMap artifactTypeMap,
            IObjectFactory objectFactory,
            ICommandCoordinator coordinator
        )
        {
            _artifactTypeMap = artifactTypeMap;
            _objectFactory = objectFactory;
            _coordinator = coordinator;
        }

        /// <summary>
        /// Handles a command
        /// </summary>
        /// <param name="request">The command and metadata to handle</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Handle([FromBody] HandleCommandRequest request)
        {
            var type = _artifactTypeMap.GetTypeFor(request.Artifact);
            var command = _objectFactory.Build(type, request.Command) as ICommand;
            var result = _coordinator.Handle(request.Tenant, command);
            return Ok(result);
        }
    }
}