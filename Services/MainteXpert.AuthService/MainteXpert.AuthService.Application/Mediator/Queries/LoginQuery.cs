using Authentication.Application.Models;
using Common.Models.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Mediator.Queries
{
    public class LoginQuery:IRequest<ResponseModel<AuthUserModel>>
    {
        public string RegisterNumber { get; set; }
        public string Password { get; set; }
    }
}
