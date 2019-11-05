using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;



namespace DAL
{
    public interface IDB
    {
        IConfiguration Configuration { get; }
    }
}
