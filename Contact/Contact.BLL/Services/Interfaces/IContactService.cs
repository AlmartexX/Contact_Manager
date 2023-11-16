using Contact.BLL.DTO;

namespace Contact.BLL.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDTO>> GetContactsAsync();

        Task<ContactDTO> GetContactByIdAsync(int id);

        Task<CreateContactDTO> CreateContactAsync(CreateContactDTO contact);

        Task <ContactDTO>UpdateContactAsync(ContactDTO contact, int id);

        Task DeleteContactAsync(int id);
    }
}
