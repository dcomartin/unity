// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Unity.Container.Registration;
using Unity.Utility;

namespace ObjectBuilder2
{
    /// <summary>
    /// Represents a strategy for mapping build keys in the build up operation.
    /// </summary>
    public class BuildKeyMappingStrategy : BuilderStrategy, IRegisterTypes
    {
        /// <summary>
        /// Called during the chain of responsibility for a build operation.  
        /// Looks for the <see cref="IBuildKeyMappingPolicy"/>
        /// and if found maps the build key for the current operation.
        /// </summary>
        /// <param name="context">The context for the operation.</param>
        public override void PreBuildUp(IBuilderContext context)
        {
            Guard.ArgumentNotNull(context, "context");

            var policy = context.Policies.Get<IBuildKeyMappingPolicy>(context.BuildKey);
            if (policy == null) return;

            var key = policy.Map(context.BuildKey, context);
            if (key == context.BuildKey) return;

            var existing = context.NewBuildUp(context.BuildKey, (child) => 
            {
                child.Existing = context.Existing;
                child.BuildKey = key;
            });

            if (null != existing)
            {
                context.Existing = existing;
                context.BuildComplete = true;
            }
        }


        #region Registerations

        public IEnumerable<IBuilderPolicy> OnRegisterType(Type from, Type to, string name, LifetimeManager lifetimeManager, InjectionMember[] injectionMembers)
        {
            // TODO: Replace with real implementation
            return Enumerable.Empty<IBuilderPolicy>();
        }

        #endregion

    }
}
