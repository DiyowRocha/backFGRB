using AutoMapper;
using backFGRB.Domain.Models;

namespace backFGRB.Application.Services.Logs;

public class LogProfile : Profile
{
    public LogProfile()
    {
        CreateMap<CreateLogDto, Log>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Timestamp, opt => opt.Ignore())
            .ForMember(dest => dest.PerformedBy, opt => opt.Ignore());

        CreateMap<Log, LogViewModel>();
    }
}