/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Artifacts;
using Dolittle.Events;
using Dolittle.Execution;
using Dolittle.PropertyBags;
using Dolittle.Reflection;
using Dolittle.Serialization.Json;
using Dolittle.Strings;
using Dolittle.Tenancy;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dolittle.AspNetCore.Debugging.Events
{
    /// <summary>
    /// Represents a <see cref="IModelBinder"/> for binding <see cref="InjectEventRequest"/>
    /// </summary>
    public class InjectEventRequestBinder : IModelBinder
    {
        readonly ISerializer _serializer;
        readonly IArtifactTypeMap _artifactTypeMap;

        /// <summary>
        /// Initializes a new instance of <see cref="InjectEventRequestBinder"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use</param>
        /// <param name="artifactTypeMap"></param>
        public InjectEventRequestBinder(ISerializer serializer, IArtifactTypeMap artifactTypeMap)
        {
            _serializer = serializer;
            _artifactTypeMap = artifactTypeMap;
        }

        /// <inheritdoc/>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var stream = bindingContext.HttpContext.Request.Body;

            using(var buffer = new MemoryStream())
            {
                await stream.CopyToAsync(buffer);

                buffer.Position = 0L;

                using(var reader = new StreamReader(buffer))
                {
                    var json = await reader.ReadToEndAsync();
                    var requestKeyValues = _serializer.GetKeyValuesFromJson(json);
                    var request = new InjectEventRequest
                    {
                        Tenant = Guid.Parse(requestKeyValues["tenant"].ToString()),
                        Artifact = _serializer.FromJson<Artifact>(requestKeyValues["artifact"].ToString()),
                        EventSource = Guid.Parse(requestKeyValues["eventSource"].ToString()),
                    };

                    var eventType = _artifactTypeMap.GetTypeFor(request.Artifact);
                    var eventData = _serializer.FromJson(eventType, requestKeyValues["event"].ToString());
                    request.Event = eventData.ToPropertyBag();
                    
                    bindingContext.Result = ModelBindingResult.Success(request);
                }

                bindingContext.HttpContext.Request.Body = buffer;
            }
        }
    }
}