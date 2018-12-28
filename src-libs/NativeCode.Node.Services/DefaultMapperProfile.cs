namespace NativeCode.Node.Services
{
    using System;
    using AutoMapper;
    using Clients.Radarr.Requests;
    using Clients.Sonarr.Requests;
    using Messages;
    using Protocol = Clients.Radarr.Requests.Protocol;

    public class DefaultMapperProfile : Profile
    {
        public DefaultMapperProfile()
        {
            this.CreateMap<MovieRelease, MovieReleaseInfo>()
                .ForMember(dest => dest.DownloadUrl, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Protocol, opt => opt.MapFrom(src => Protocol.Torrent))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTime.UtcNow.ToString("o")))
                .ReverseMap();

            this.CreateMap<SeriesRelease, SeriesReleaseInfo>()
                .ForMember(dest => dest.DownloadUrl, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Protocol, opt => opt.MapFrom(src => Protocol.Torrent))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTime.UtcNow.ToString("o")))
                .ReverseMap();
        }
    }
}
