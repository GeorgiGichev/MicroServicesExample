﻿using System.Threading.Tasks;

namespace VendorService.Services.Data.Vendor
{
    public interface IVendorService
    {
        Task<T> GetById<T>(int id);

        Task<IEnumerable<T>> GetAll<T>();

        Task<K> Create<T, K>(T model);

    }
}
