/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Artifacts;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Queries;
using Dolittle.Tenancy;
using IRuntimeQueryCoordinator = Dolittle.Queries.Coordination.IQueryCoordinator;

namespace Dolittle.AspNetCore.Debugging.Queries
{
    /// <summary>
    /// An implementation of <see cref="IQueryCoordinator"/>
    /// </summary>
    public class QueryCoordinator : IQueryCoordinator
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IRuntimeQueryCoordinator _runtimeQueryCoordinator;
        readonly IContainer _container;

        /// <summary>
        /// Instanciates a new <see cref="QueryCoordinator"/>
        /// </summary>
        /// <param name="executionContextManager"></param>
        /// <param name="runtimeQueryCoordinator"></param>
        /// <param name="container"></param>
        public QueryCoordinator(
            IExecutionContextManager executionContextManager,
            IRuntimeQueryCoordinator runtimeQueryCoordinator,
            IContainer container
        )
        {
            _executionContextManager = executionContextManager;
            _runtimeQueryCoordinator = runtimeQueryCoordinator;
            _container = container;
        }

        /// <inheritdoc/>
        public QueryResult Handle(TenantId tenant, IQuery query)
        {
            _executionContextManager.CurrentFor(tenant);
            var instance = _container.Get(query.GetType()) as IQuery;

            foreach (var property in query.GetType().GetProperties())
            {
                if (!property.Name.Equals("Query"))
                {
                    property.SetValue(instance, property.GetValue(query));
                }
            }

            return _runtimeQueryCoordinator.Execute(instance, new PagingInfo { Number = 0, Size = int.MaxValue }).Result;
        }
    }
}