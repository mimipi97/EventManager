using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmanagement.Repositories
{
    public class RepositoryFactory
    {
        public static EventRepository GetEventRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EventManagerConnectionString"].ConnectionString;
            return new EventRepository(connectionString);
        }
    }
}

