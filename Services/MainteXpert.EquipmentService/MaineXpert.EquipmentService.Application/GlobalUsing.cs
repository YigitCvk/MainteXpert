global using AutoMapper;
global using FluentValidation;
global using MainteXpert.EquipmentService.Application.Mapper;
global using MainteXpert.EquipmentService.Application.Mediator.Queries;
global using MainteXpert.EquipmentService.Application.Models;
global using MainteXpert.Common.Models.Base;
global using MainteXpert.Middleware.Behaviors;
global using MainteXpert.Repository.Collections.Equipment;
global using MainteXpert.Repository.Interface;
global using MediatR;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Serilog;
global using System.Reflection;
global using MainteXpert.EquipmentService.Application.Mediator.Commands;
global using MongoDB.Driver;
global using Microsoft.Extensions.Logging;
global using Newtonsoft.Json;
global using System.Diagnostics;
global using MainteXpert.Common.Models.Equipment;
global using EquipmentModel = MainteXpert.Common.Models.Equipment.EquipmentModel;


