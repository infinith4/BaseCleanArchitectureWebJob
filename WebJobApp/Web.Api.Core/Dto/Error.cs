using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto
{
    public sealed class Error
    {
        public string Code { get; }
        public string Description { get; }

        public Error(string code, string description)
        {
            this.Code = code;
            this.Description = description;
        }
    }
}
