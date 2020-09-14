using ReAgent.Models.BO;
using ReAgent.Models.DAO;

namespace ReAgent.Mapping.BO
{
    public class OrderMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "BO.Order";
            }
        }

        public OrderMappingProfile()
        {
            this.CreateMap<tblOrder, OrderBO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_id))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.details))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.date))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.fk_status))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.fk_user))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.tblUser.first_name} {src.tblUser.last_name}"));

            this.CreateMap<tblOrderMessage, OrderMessageBO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.user_name))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.message))
                .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => src.send_date));

        }
    }
}