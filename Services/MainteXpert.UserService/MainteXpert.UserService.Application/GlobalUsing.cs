global using FluentValidation;
global using MongoDB.Bson;
global using MongoDB.Driver;
global using MainteXpert.Repository.Collections.User;
global using MainteXpert.Repository.Interface;
global using MainteXpert.UserService.Application.Mediator.Commands;
global using MongoDB.Bson.Serialization.Attributes;
global using MainteXpert.Common.Models.Base;
global using MainteXpert.Common.Models.User;
global using MediatR;
global using AutoMapper;
global using MainteXpert.UserService.Application.Mediator.Queries;
global using MainteXpert.UserService.Application.Models.Request;
global using MainteXpert.Common.Models;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Serilog;
global using System.Reflection;
global using MainteXpert.Middleware.Behaviors;
global using MainteXpert.UserService.Application.Mapper;
global using SystemTextJson = System.Text.Json.JsonSerializer;








