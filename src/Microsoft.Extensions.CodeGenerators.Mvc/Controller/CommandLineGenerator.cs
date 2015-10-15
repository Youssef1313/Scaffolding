﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.CodeGeneration;
using Microsoft.Extensions.CodeGeneration.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.CodeGenerators.Mvc.Controller
{
    [Alias("controller")]
    public class CommandLineGenerator : ICodeGenerator
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandLineGenerator([NotNull]IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task GenerateCode([NotNull]CommandLineGeneratorModel model)
        {
            ControllerGeneratorBase generator = null;

            if (model.IsRestController)
            {
                if (string.IsNullOrEmpty(model.ModelClass))
                {
                    if (model.GenerateReadWriteActions)
                    {
                        //WebAPI with Actions
                    }
                    else
                    {
                        //Empty Web API
                    }
                }
                else
                {
                    //WebAPI With Context
                }
            }
            else
            {
                if (string.IsNullOrEmpty(model.ModelClass))
                {
                    if (model.GenerateReadWriteActions)
                    {
                        //Mvc with Actions
                    }
                    else
                    {
                        generator = GetGenerator<MvcControllerEmpty>();
                    }
                }
                else
                {
                    generator = GetGenerator<MvcControllerWithContext>();
                }
            }

            if (generator != null)
            {
                await generator.Generate(model);
            }
            else
            {
                // Just throwing as I enable this functionality, should remove it once I fill all the above...
                throw new Exception("Functionality not yet enabled...");
            }
        }

        private ControllerGeneratorBase GetGenerator<TChild>() where TChild : ControllerGeneratorBase
        {
            return (ControllerGeneratorBase)ActivatorUtilities.CreateInstance<TChild>(_serviceProvider);
        }
    }
}
