using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Models.DTOs;
using StudentRegistration.Models.Entities;
using StudentRegistration.Repositories.Interface;

namespace StudentRegistration.Controllers
{
    public class ContactController : Controller
    {
        private readonly IManageContact _manageContact;
        public ContactController(IManageContact manageContact)
        {
            _manageContact = manageContact;
        }
        public IActionResult Index()
        {
            var contacts = _manageContact.GetAllContacts();
            return View("NewContact", contacts);
        }

        [HttpPost]
        public IActionResult SaveNewContact([FromBody] ContactDTO contactDTO)
        {
            Contact contact = new Contact
            {
                Name = contactDTO.Name,
                Email = contactDTO.Email,
                Gender = contactDTO.Gender,
                Address = contactDTO.Address,
                State = contactDTO.State,
                Zip = contactDTO.Zip,
                Country = contactDTO.Country,
                Phone = contactDTO.Phone,
            };
            _manageContact.SaveContact(contact);
            return Ok(new { message = "Contact saved successfully" });
        }

        [HttpPost]
        public IActionResult CheckForDuplicateEmails([FromBody] string EmailID)
        {
            if (_manageContact.CheckForDuplicateEmails(EmailID))
                return BadRequest(new { message = "This Email already exists" });
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteContact([FromBody] int ContactId)
        {
            if (_manageContact.DeleteContact(ContactId) > 0)
                return Ok(new { message = "Contact Deleted Successfully" });
            return BadRequest();
        }

        [HttpPost]
        public IActionResult GetContactById([FromBody] int ContactId)
        {
            var contact = _manageContact.GetContactById(ContactId);
            if (contact != null)
                return Ok(contact);
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateContact([FromBody] ContactDTO contactDTO)
        {          
            return BadRequest();
        }

    }
}
