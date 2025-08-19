using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;

namespace ProductManagement.Application.Profiles;

public class SupplierProfile : Profile {
    public SupplierProfile()
    {
        CreateMap<Supplier, SupplierRequestDto>().ReverseMap();
        CreateMap<Supplier, SupplierResponseDto>().ReverseMap();
    }

}

