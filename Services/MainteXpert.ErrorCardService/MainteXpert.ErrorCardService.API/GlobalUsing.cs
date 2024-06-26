﻿global using MainteXpert.Common.Models;
global using MainteXpert.Common.Models.Base;
global using MainteXpert.Common.Models.ErrorCard;
global using MainteXpert.Common.Models.Pagination;
global using MainteXpert.ErrorCardService.Application.Mediator.Commands.ErrorCard;
global using MainteXpert.ErrorCardService.Application.Mediator.Commands.ErrorCardCall;
global using MainteXpert.ErrorCardService.Application.Mediator.Queries.ErrorCard;
global using MainteXpert.ErrorCardService.Application.Mediator.Queries.ErrorCardCall;
global using MainteXpert.ErrorCardService.Application.Models.Response;
global using MediatR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using System.Net;
global using MainteXpert.Repository.Configration;
global using MainteXpert.Repository.Interface;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.Extensions.Options;
global using MainteXpert.ErrorCardService.Application.DI;
global using MainteXpert.Middleware.Exceptions;
global using MainteXpert.Repository.Repository;
global using Microsoft.OpenApi.Models;