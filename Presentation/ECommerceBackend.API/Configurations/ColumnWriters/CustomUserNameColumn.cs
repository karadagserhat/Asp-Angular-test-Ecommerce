using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;

namespace ECommerceBackend.API.Configurations.ColumnWriters
{
    public class CustomUserNameColumn : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (!logEvent.Properties.ContainsKey("UserName"))
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserName", "Anonymous"));
            }
        }
    }
}