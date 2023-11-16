using Contact.DAL.Context;
using Contact.DAL.Modell;
using Contact.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Contact.DAL.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationContext _context;
        public ContactRepository(ApplicationContext context)
        {
            _context = context
                ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<ContactInfo>> GetContactsAsync() =>
             await _context.Contacts
            .AsNoTracking()
            .ToListAsync();

        public async Task<ContactInfo> GetContactByIdAsync(int id) =>
             await _context.Contacts
             .AsNoTracking()
             .FirstOrDefaultAsync(e => e.Id == id);


        public async Task CreateContactAsync(ContactInfo contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(ContactInfo contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteContactAsync(int id)
        {
            var contactToDelete = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contactToDelete);
            await _context.SaveChangesAsync();

        }
    }
}
