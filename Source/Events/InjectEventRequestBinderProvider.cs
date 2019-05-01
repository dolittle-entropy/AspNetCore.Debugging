/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Dolittle.AspNetCore.Debugging.Events
{
    /// <summary>
    /// Represents a <see cref="IModelBinderProvider"/> for providing <see cref="InjectEventRequestBinder"/>
    /// </summary>
    public class InjectEventRequestBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc/>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(InjectEventRequest)) return new BinderTypeModelBinder(typeof(InjectEventRequestBinder));

            return null;
        }
    }
}
