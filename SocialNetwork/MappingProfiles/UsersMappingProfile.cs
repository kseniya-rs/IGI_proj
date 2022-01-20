using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace SocialNetwork.MappingProfiles
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<Database.Models.UserModel, ViewModels.UserProfileViewModel>()
                .ForMember(p => p.IsFriend, o =>
                   o.ResolveUsing(
                       (src, dest, destMember, resContext) =>
                           dest.IsFriend = resContext.Items.ContainsKey("IsFriend") ? (bool)resContext.Items["IsFriend"] : false
                       )
                 )
                 .ForMember(p => p.Username, o => o.ResolveUsing(
                     x => x.Credential.Username
                 ));
            CreateMap<ViewModels.UserProfileViewModel, Database.Models.UserModel>();
        }
    }
}
