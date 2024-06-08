using Authentication.Application.Helper;
using Authentication.Application.Mediator.Commands;
using Authentication.Application.Models;
using AutoMapper;
using Common.Models.Base;
using Common.Models.User;
using MediatR;
using Mongo.Collections.User;
using Mongo.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Mediator.Handlers
{
    public class VerifyUserHandler : IRequestHandler<VerifyUserCommand, ResponseModel<AuthUserModel>>
    {
        private readonly IMongoRepository<UserCollection> _collection;
        private readonly IMongoRepository<UserRoleCollection> _roleCollection;
        private readonly ITokenGenerator _tokenGenerator;

        private readonly IMapper _mapper;

        public VerifyUserHandler(IMongoRepository<UserCollection> collection, 
            IMapper mapper,
            IMongoRepository<UserRoleCollection> roleCollection,
            ITokenGenerator tokenGenerator)
        {
            _collection = collection;
            _mapper = mapper;
            _roleCollection = roleCollection;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResponseModel<AuthUserModel>> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                /*
                FilterDefinition<UserCollection> filter = new ExpressionFilterDefinition<UserCollection>
                         (x => x.CitizenNumber == request.CitizenNumber && x.RegisterNumber == request.RegisterNumber);
                */
                var user = await _collection.FindOneAsync(x => x.RegisterNumber == request.RegisterNumber);

                AuthUserModel authUser = _mapper.Map<AuthUserModel>(user);
                authUser.Token = _tokenGenerator.CreateAccessToken(user.Id, false);

                var userRoleDocument = await _roleCollection.FindByIdAsync(user.RoleId);
                var userRoleModel = _mapper.Map<UserRoleModel>(userRoleDocument);

                authUser.UserRole = userRoleModel;

                return ResponseModel<AuthUserModel>.Success(data: authUser);





            }
            catch (Exception ex)
            {
                return ResponseModel<AuthUserModel>.Fail(data:null,message:ex.Message);
            }

        }
    }
}
