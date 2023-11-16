using Contact.DAL.Modell;

namespace Contact.DAL.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactInfo>> GetContactsAsync();

        Task<ContactInfo> GetContactByIdAsync(int id);

        Task CreateContactAsync(ContactInfo contact);

        Task UpdateContactAsync(ContactInfo contact);

        Task DeleteContactAsync(int id);
    }
}
