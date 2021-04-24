using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCommon.HelperModels
{
    public class JwtTokenPacket
    {
        public string UserName { get; set; }
        public string Expiration { get; set; }
        public string Token { get; set; }
    }
}
