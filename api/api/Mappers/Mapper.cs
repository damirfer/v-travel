using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Model.Guide, Model.ViewModels.GuideVM.Single>().ForMember(x => x.GuideLanguage, opt => opt.Ignore());
            CreateMap<Model.Guide, Model.ViewModels.GuideVM.Single>().ReverseMap().ForMember(x => x.GuideLanguage, opt => opt.Ignore());

        }
    }
}
