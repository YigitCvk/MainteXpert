﻿global using AutoMapper;
global using FluentValidation;
global using MainteXpert.Common.Models.Base;
global using MainteXpert.Common.Models.Kaizen;
global using MainteXpert.KaizenService.Application.Mediator.Commands;
global using MainteXpert.KaizenService.Application.Mediator.Queries;
global using MainteXpert.Middleware.Behaviors;
global using MainteXpert.Repository.Collections.Kaizen;
global using MainteXpert.Repository.Interface;
global using MediatR;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using MongoDB.Bson;
global using MongoDB.Driver;
global using Serilog;
global using System.Reflection;
