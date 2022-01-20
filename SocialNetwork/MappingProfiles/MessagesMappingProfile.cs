using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.ViewModels;
using Database.Models;

namespace SocialNetwork.MappingProfiles
{
    public class MessagesMappingProfile : Profile
    {
        public MessagesMappingProfile()
        {
            CreateMap<MessageViewModel, MessageModel>();
            CreateMap<MessageModel, MessageViewModel>();
        }
    }
}
