using FlexHealthDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Services
{
    public interface ITokenService
    {
        Task<string> GetToken(UserUpdateDto userUpdateDto);
    }
}
