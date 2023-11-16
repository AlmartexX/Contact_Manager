using AutoMapper;
using Contact.BLL.DTO;
using Contact.DAL.Modell;

namespace Contact.BLL.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ContactInfo, ContactDTO>();
            CreateMap<ContactDTO, ContactInfo>();
            CreateMap<CreateContactDTO, ContactInfo>();

        }
    }
}
