using ReAgent.Models.BO;
using ReAgent.Models.Views.Order;

namespace ReAgent.Mapping.View
{
    public class OrderMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "View.Order";
            }
        }

        public OrderMappingProfile()
        {
            this.CreateMap<OrderBO, OrderVM>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Details));

            this.CreateMap<OrderMessageBO, OrderMessageVM>();

        }
    }
}