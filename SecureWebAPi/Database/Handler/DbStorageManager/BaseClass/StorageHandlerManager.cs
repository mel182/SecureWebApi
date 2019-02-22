using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Handler.DbStorageManager
{
    public abstract class StorageHandlerManager
    {
        protected DatabaseRepository Context { get; set; }


        public void SetRepositoryContext(DatabaseRepository databaseContext)
        {
            this.Context = databaseContext;
        }
    }
}
