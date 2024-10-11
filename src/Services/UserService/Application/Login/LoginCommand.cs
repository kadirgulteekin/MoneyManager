using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Login;

public sealed class LoginCommand : ICommand<string>
{
    public string Email{ get; set; }
    public string Password{ get; set; }
}
