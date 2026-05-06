using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Users.Commands.BlockUser
{
    public class BlockUserCommand
    {
        public int UserId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime? BlockedUntil { get; set; } // null = permanent
    }
}
