using ReAgent.Models.BO;
using ReAgent.Models.DAO;
using ReAgent.Models.Enums;

namespace ReAgent.Mapping.BO
{
    public class AccountMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "BO.Account";
            }
        }

        public AccountMappingProfile()
        {
            this.CreateMap<tblUser, UserBO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.address))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.phone))
                .ForMember(dest => dest.VerificationToken, opt => opt.MapFrom(src => src.verification_token))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.registration_date))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (UserRole)src.fk_role))
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}