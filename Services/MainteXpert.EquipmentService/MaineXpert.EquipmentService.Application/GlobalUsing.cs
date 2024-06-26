global using AutoMapper;
global using FluentValidation;
global using MaineXpert.EquipmentService.Application.Mapper;
global using MaineXpert.EquipmentService.Application.Mediator.Queries;
global using MaineXpert.EquipmentService.Application.Models;
global using MainteXpert.Common.Models.Base;
global using MainteXpert.Middleware.Behaviors;
global using MainteXpert.Repository.Collections.Equipment;
global using MainteXpert.Repository.Interface;
global using MediatR;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Serilog;
global using System.Reflection;
global using MaineXpert.EquipmentService.Application.Mediator.Commands;
global using MongoDB.Driver;

