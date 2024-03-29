﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface ICartsRepository
    {
        Task<Cart> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Cart>> GetAllWithDetailsAsync();
        Task CreateAsync(Cart cart);
        Task DeleteByIdAsync(int id);
        void Update(Cart cart);
    }
}
