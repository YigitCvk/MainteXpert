﻿global using FluentValidation;
global using MainteXpert.Taiga.Application.Mapper;
global using MediatR;
global using Microsoft.Extensions.DependencyInjection;
global using System.Reflection;
global using Microsoft.Extensions.Logging;
global using Newtonsoft.Json;
global using System.Diagnostics;
global using AutoMapper;
global using MainteXpert.MaintenanceSchedule.Application.Validations;
global using Microsoft.Extensions.Configuration;
global using Serilog;
global using MainteXpert.Common.Models.Base;
global using MainteXpert.Common.Models.Taiga;
global using MainteXpert.Repository.Collections.TaigaProject;
global using MainteXpert.Repository.Interface;
global using MainteXpert.Taiga.Application.Mediator.Commands;
global using MainteXpert.Taiga.Application.Mediator.Queries;
