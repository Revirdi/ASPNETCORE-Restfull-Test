using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testeraja.Data;
using testeraja.Models;

namespace testeraja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest AddContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = AddContactRequest.Address,
                FullName = AddContactRequest.FullName,
                Phone = AddContactRequest.Phone,
                Email = AddContactRequest.Email,
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPatch]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.FullName = updateContactRequest.FullName;
            contact.Phone = updateContactRequest.Phone;
            contact.Email = updateContactRequest.Email;
            contact.Address = updateContactRequest.Address;

            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            dbContext.Remove(contact);
            await dbContext.SaveChangesAsync();

            return Ok("Success Delete Contact");
        }
    }
}
