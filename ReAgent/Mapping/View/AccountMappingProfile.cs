using ReAgent.Models.BO;
using ReAgent.Models.Views.Account;

namespace ReAgent.Mapping.View
{
    public class AccountMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "View.Account";
            }
        }

        public AccountMappingProfile()
        {
            this.CreateMap<RegisterVM, UserBO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.VerificationToken, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            this.CreateMap<UserBO, UserVM>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<UserVM, UserBO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.VerificationToken, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());

        }
    }
}