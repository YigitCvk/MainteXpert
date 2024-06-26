﻿global using AutoMapper;
global using FluentValidation;
global using MainteXpert.InventoryService.Application.Mapper;
global using MainteXpert.Middleware.Behaviors;
global using MediatR;
global using MongoDB.Driver;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Serilog;
global using System.Reflection;
global using MainteXpert.Common.Models.Base;
global using MainteXpert.Common.Models.Inventory;
global using MainteXpert.InventoryService.Application.Mediator.Commands;
global using MainteXpert.Repository.Collections;
global using MainteXpert.Repository.Interface;
global using MainteXpert.InventoryService.Application.Mediator.Queries;
