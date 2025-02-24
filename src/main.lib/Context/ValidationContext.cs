﻿using ACMESharp.Authorizations;
using ACMESharp.Protocol.Resources;
using Autofac;
using PKISharp.WACS.Clients.Acme;
using PKISharp.WACS.DomainObjects;
using PKISharp.WACS.Plugins.Base.Options;
using PKISharp.WACS.Plugins.Interfaces;
using System;

namespace PKISharp.WACS.Context
{
    public class ValidationContextParameters
    {
        public ValidationContextParameters(
            AuthorizationContext authorization,
            TargetPart targetPart,
            ValidationPluginOptions options)
        {
            TargetPart = targetPart;
            OrderContext = authorization.Order;
            Authorization = authorization.Authorization;
            Label = authorization.Label;
            Options = options;
        }
        public OrderContext OrderContext { get; }
        public ValidationPluginOptions Options { get; }
        public TargetPart TargetPart { get; }
        public Authorization Authorization { get; }
        public string Label { get; }
    }

    public class ValidationContext
    {
        public ValidationContext(
            ILifetimeScope scope,
            ValidationContextParameters parameters)
        {
            Identifier = parameters.Authorization.Identifier.Value;
            Label = parameters.Label;
            TargetPart = parameters.TargetPart;
            Authorization = parameters.Authorization;
            OrderResult = parameters.OrderContext.OrderResult;
            Scope = scope;
            ChallengeType = parameters.Options.ChallengeType;
            PluginName = parameters.Options.Name;
            ValidationPlugin = scope.Resolve<IValidationPlugin>();
            Valid = parameters.Authorization.Status == AcmeClient.AuthorizationValid;
        }
        public bool Valid { get; }
        public ILifetimeScope Scope { get; }
        public string Identifier { get; }
        public string Label { get; }
        public string ChallengeType { get; }
        public string PluginName { get; }
        public OrderResult OrderResult { get; }
        public TargetPart? TargetPart { get; }
        public Authorization Authorization { get; }
        public Challenge? Challenge { get; set; }
        public IChallengeValidationDetails? ChallengeDetails { get; set; }
        public IValidationPlugin ValidationPlugin { get; set; }
    }

}
