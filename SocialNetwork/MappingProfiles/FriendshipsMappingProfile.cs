using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.ViewModels;
using Database.Models;
using AutoMapper;

namespace SocialNetwork.MappingProfiles
{
    public class FriendshipsMappingProfile : Profile
    {
        public FriendshipsMappingProfile()
        {
            CreateMap<FriendshipModel, FriendshipViewModel>();
            CreateMap<FriendshipViewModel, FriendshipModel>();
        }
    }
}
