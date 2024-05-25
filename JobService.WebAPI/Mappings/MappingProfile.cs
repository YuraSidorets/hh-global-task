using AutoMapper;
using JobService.Domain;
using JobService.WebAPI.Dto;

namespace JobService.WebAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<JobTotalRequest, Job>();
        CreateMap<ItemDto, Item>();
        CreateMap<ItemCostResult, ItemResponseDto>();
        CreateMap<JobCostResult, JobTotalResponse>();
    }
}