using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.PersonalInformation;

namespace ContactStoreTipsDemo
{
    class RemoteIdHelper
    {
        private const string ContactStoreLocalInstanceIdKey = "LocalInstanceId";

        public async Task SetRemoteIdGuid(ContactStore store)
        {
            IDictionary<string, object> properties;
            properties = await store.LoadExtendedPropertiesAsync().AsTask<IDictionary<string, object>>();
            if (!properties.ContainsKey(ContactStoreLocalInstanceIdKey))
            {
                // the given store does not have a local instance id so set one against store extended properties
                Guid guid = Guid.NewGuid();
                properties.Add(ContactStoreLocalInstanceIdKey, guid.ToString());
                System.Collections.ObjectModel.ReadOnlyDictionary<string, object> readonlyProperties = new System.Collections.ObjectModel.ReadOnlyDictionary<string, object>(properties);
                await store.SaveExtendedPropertiesAsync(readonlyProperties).AsTask();
            }
        }

        public async Task<string> GetTaggedRemoteId(ContactStore store, string remoteId)
        {
            string taggedRemoteId = string.Empty;

            System.Collections.Generic.IDictionary<string, object> properties;
            properties = await store.LoadExtendedPropertiesAsync().AsTask<System.Collections.Generic.IDictionary<string, object>>();
            if (properties.ContainsKey(ContactStoreLocalInstanceIdKey))
            {
                taggedRemoteId = string.Format("{0}_{1}", properties[ContactStoreLocalInstanceIdKey], remoteId);
            }
            else
            {
                // handle error condition
            }

            return taggedRemoteId;
        }

        public async Task<string> GetUntaggedRemoteId(ContactStore store, string taggedRemoteId)
        {
            string remoteId = string.Empty;

            System.Collections.Generic.IDictionary<string, object> properties;
            properties = await store.LoadExtendedPropertiesAsync().AsTask<System.Collections.Generic.IDictionary<string, object>>();
            if (properties.ContainsKey(ContactStoreLocalInstanceIdKey))
            {
                string localInstanceId = properties[ContactStoreLocalInstanceIdKey] as string;
                if (taggedRemoteId.Length > localInstanceId.Length + 1)
                {
                    remoteId = taggedRemoteId.Substring(localInstanceId.Length + 1);
                }
            }
            else
            {
                // handle error condition
            }

            return remoteId;
        }

    }
 

}
