﻿using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
    }
}
