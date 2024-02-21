using AutoMapper;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Domain.Models;

namespace UsersAPI.Application.Mappings;

public class ModelToDtoMap : Profile
{
    public ModelToDtoMap()
    {
        CreateMap<User, UserResponseDto>();
        /*.AfterMap((model, dto) =>
        {
        dto.Id = model.Id;
        dto.Name = model.Name;
        dto.Email = model.Email;
        dto.CreatedAt = model.CreatedAt;
        });*/
    }
}