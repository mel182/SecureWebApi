using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Handler.DbStorageManager
{
    public abstract class StorageHandlerManager
    {
        protected DatabaseContext Context { get; set; }


        public void SetContext(DatabaseContext databaseContext)
        {
            this.Context = databaseContext;
        }
    }
}
