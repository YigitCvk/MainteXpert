﻿global using AutoMapper;
global using FluentValidation;
global using MainteXpert.Middleware.Behaviors;
global using MediatR;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Serilog;
global using System.Reflection;
global using MainteXpert.Common.Models.TPMMetricsService;
global using MainteXpert.Repository.Collections.TPM;
global using MainteXpert.Common.Models.Base;
global using MainteXpert.TPMMetricsService.Application.Mediator.Commands;
global using MainteXpert.Repository.Interface;
global using MongoDB.Driver;
global using MainteXpert.TPMMetricsService.Application.Mediator.Queries;
