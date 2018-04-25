using AutoMapper;
using Instagraph.Data.Dto;
using Instagraph.Models;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(x => x.ProfilePicture, z => z.UseValue<Picture>(null));
        }
    }
}
