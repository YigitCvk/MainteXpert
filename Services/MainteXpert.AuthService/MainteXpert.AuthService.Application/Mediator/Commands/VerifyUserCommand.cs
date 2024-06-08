using Authentication.Application.Models;
using Common.Models.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Mediator.Commands
{
    public class VerifyUserCommand : IRequest<ResponseModel<AuthUserModel>>
    {
        public string RegisterNumber { get; set; }
      
    }
}
