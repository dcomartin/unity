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
    /// A <see cref="BuilderStrategy"/> that will look for a build plan
    /// in the current context. If it exists, it invokes it, otherwise
    /// it creates one and stores it for later, and invokes it.
    /// </summary>
    public class BuildPlanStrategy : BuilderStrategy, IRegisterTypes
    {
        /// <summary>
        /// Called during the chain of responsibility for a build operation.
        /// </summary>
        /// <param name="context">The context for the operation.</param>
        public override void PreBuildUp(IBuilderContext context)
        {
            Guard.ArgumentNotNull(context, "context");

            IPolicyList buildPlanLocation;

            var plan = context.Policies.Get<IBuildPlanPolicy>(context.BuildKey, out buildPlanLocation);
            if (plan == null || plan is OverriddenBuildPlanMarkerPolicy)
            {
                IPolicyList creatorLocation;

                var planCreator = context.Policies.Get<IBuildPlanCreatorPolicy>(context.BuildKey, out creatorLocation);
                if (planCreator != null)
                {
                    plan = planCreator.CreatePlan(context, context.BuildKey);
                    (buildPlanLocation ?? creatorLocation).Set(plan, context.BuildKey);
                }
            }
            if (plan != null)
            {
                plan.BuildUp(context);
            }
        }


        #region Registerations

        public IEnumerable<IBuilderPolicy> OnRegisterType(Type from, Type to, string name, LifetimeManager lifetimeManager, InjectionMember[] injectionMembers)
        {
            if (null == injectionMembers || 0 == injectionMembers.Length)
                return Enumerable.Empty<IBuilderPolicy>();

            // TODO: Replace with real implementation
            return Enumerable.Empty<IBuilderPolicy>();
        }

        #endregion

    }
}
