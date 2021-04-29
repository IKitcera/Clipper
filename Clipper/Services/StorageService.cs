using System;
using System.Collections.Generic;
using System.Text;

namespace Clipper.Services
{
    class StorageService
    {
        private static MockDataStoreService storage;
        private StorageService() { }
        public static MockDataStoreService GetStorage()
        {
            if (storage == null)
                storage = new MockDataStoreService();
            return storage;
        }
    }
}
