using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Common.MassTransit.Messages
{
    public record UserByIdResponse(int idUser, string username);
}
