using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendra.Core.Common
{
    public static class Errors
    {
        public const string PasswordErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, and one numeric digit.";
        public const string ConfirmPass = "The password and confirmation password do not match.";

    }
}
