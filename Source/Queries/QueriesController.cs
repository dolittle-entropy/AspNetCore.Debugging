/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Events;
using Dolittle.PropertyBags;
using Dolittle.Queries;
using Dolittle.Runtime.Events;
using Microsoft.AspNetCore.Mvc;

namespace Dolittle.AspNetCore.Debugging.Queries
{
    /// <summary>
    /// Represents a debugging API endpoint for working with <see cref="IQuery">queries</see>
    /// </summary>
    [Route("api/Dolittle/Debugging/Queries")]
    public class QueriesController : ControllerBase
    {
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly IObjectFactory _objectFactory;
        readonly IQueryCoordinator _coordinator;

        /// <summary>
        /// Initializes a new instance of <see cref="QueriesController"/>
        /// </summary>
        /// <param name="artifactTypeMap"></param>
        /// <param name="objectFactory"></param>
        /// <param name="coordinator"></param>
        public QueriesController(
            IArtifactTypeMap artifactTypeMap,
            IObjectFactory objectFactory,
            IQueryCoordinator coordinator
        )
        {
            _artifactTypeMap = artifactTypeMap;
            _objectFactory = objectFactory;
            _coordinator = coordinator;
        }

        /// <summary>
        /// Handles a query
        /// </summary>
        /// <param name="request">The query and metadata to execute</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Handle([FromBody] ExecuteQueryRequest request)
        {
            var type = _artifactTypeMap.GetTypeFor(request.Artifact);
            var command = _objectFactory.Build(type, request.Query) as IQuery;
            var result = _coordinator.Handle(request.Tenant, command);
            return Ok(result);
        }
    }
}