using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorService.Services.DTOModels.VendorModels;

namespace VendorService.Client.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewVendor(VendorPublishedModel model);
    }
}
