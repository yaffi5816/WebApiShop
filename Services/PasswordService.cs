using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zxcvbn;
namespace Services
{
    public class PasswordService
    {
        public Password CheckPassword(string password)
        {
            Password newPassword = new Password();
            newPassword.ThePassword = password;
            var result = Zxcvbn.Core.EvaluatePassword(newPassword.ThePassword);
            newPassword.Level=result.Score;
            return newPassword;
        }
    }
}
