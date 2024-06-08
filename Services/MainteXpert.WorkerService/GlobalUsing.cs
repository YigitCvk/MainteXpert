﻿global using MainteXpert.Common.Enums;
global using MainteXpert.Common.Models.Activity;
global using MainteXpert.Repository.Collections.Activity;
global using MainteXpert.Repository.Interface;
global using MongoDB.Driver;
global using MongoDB.Driver.Linq;
global using Quartz;
global using ILogger = Microsoft.Extensions.Logging.ILogger;
global using MainteXpert.MessagingService.Producer;
global using MainteXpert.MessagingService.Events;
global using MainteXpert.MessagingService.Queues;
global using MainteXpert.MessagingService.Interfaces;
global using MainteXpert.MessagingService;
global using MainteXpert.Middleware.Behaviors;
global using MainteXpert.Repository.Configration;
global using MainteXpert.Repository.Repository;
global using MainteXpert.WorkerService;
global using MediatR;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Options;
global using RabbitMQ.Client;
global using Serilog;
global using MainteXpert.Repository.Collections.Lookup;
global using MainteXpert.WorkerService.Jobs;
global using Quartz.Impl;