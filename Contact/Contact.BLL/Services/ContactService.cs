using AutoMapper;
using Contact.BLL.DTO;
using Contact.BLL.Services.Interfaces;
using Contact.DAL.Modell;
using Contact.DAL.Repositories.Interfaces;

namespace Contact.BLL.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public ContactService(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper
               ?? throw new ArgumentNullException();

            _contactRepository = contactRepository
                ?? throw new ArgumentNullException();
        }

        public async Task<CreateContactDTO> CreateContactAsync(CreateContactDTO newContact)
        {
            try
            {
                var contact = _mapper.Map<ContactInfo>(newContact);
                await _contactRepository.CreateContactAsync(contact);
                return newContact;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task DeleteContactAsync(int id)
        {
            try
            {
                var contactId = await _contactRepository.GetContactByIdAsync(id);
                if (contactId == null)
                {
                    throw new Exception();
                }
                await _contactRepository.DeleteContactAsync(id);
                
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<ContactDTO> GetContactByIdAsync(int id)
        {
            try
            {
                var contact = await _contactRepository.GetContactByIdAsync(id);
                return _mapper.Map<ContactDTO>(contact);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsAsync()
        {
            try
            {
                var contacts = await _contactRepository.GetContactsAsync();
                return _mapper.Map<IEnumerable<ContactDTO>>(contacts);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ContactDTO> UpdateContactAsync(ContactDTO contact,int id)
        {
            try
            {
                var existingContact = await _contactRepository.GetContactByIdAsync(id);
                var updatedContact = _mapper.Map<ContactInfo>(contact);

                updatedContact.Id = existingContact.Id;

                await _contactRepository.UpdateContactAsync(updatedContact);
                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
