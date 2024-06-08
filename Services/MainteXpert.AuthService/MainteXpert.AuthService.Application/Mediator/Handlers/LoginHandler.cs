using Authentication.Application.Helper;
using Authentication.Application.Mediator.Queries;
using Authentication.Application.Models;
using AutoMapper;
using Common.Models.Base;
using Common.Models.User;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mongo.Collections.User;
using Mongo.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Mediator.Handlers
{
    public class LoginHandler : IRequestHandler<LoginQuery, ResponseModel<AuthUserModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<UserCollection> _userCollection;
        private readonly IMongoRepository<UserRoleCollection> _roleCollection;
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;
        public LoginHandler(IMapper mapper,
            IMongoRepository<UserCollection> userCollection,
            IMongoRepository<UserRoleCollection> roleCollection,
            IConfiguration configuration,
            ITokenGenerator tokenGenerator)
        {
            _mapper = mapper;
            _userCollection = userCollection;
            _roleCollection = roleCollection;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResponseModel<AuthUserModel>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userCollection.FindOneAsync(x => x.RegisterNumber.Equals(request.RegisterNumber) && x.Password.Equals(request.Password));
                if (user is not null)
                {
                    if (!user.IsActive)
                    {
                        return ResponseModel<AuthUserModel>
                                .Fail(data: null,message:"giriş yapılamıyor yönetici onayı gerekli");
                    }
                    else
                    {
                        AuthUserModel authUser = _mapper.Map<AuthUserModel>(user);
                        authUser.Token = _tokenGenerator.CreateAccessToken(user.Id);
                        var userRoleDocument = await _roleCollection.FindByIdAsync(user.RoleId);
                        var userRoleModel = _mapper.Map<UserRoleModel>(userRoleDocument);
                        authUser.UserRole = userRoleModel;

                        return ResponseModel<AuthUserModel>.Success(data: authUser);
                    }
                }
                else
                {
                    return ResponseModel<AuthUserModel>.Fail(data: null, message: "Kullanıcı bulunamadı", httpCode: HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return ResponseModel<AuthUserModel>.Fail(data: null, message: ex.Message);
            }
        }
    }
}
