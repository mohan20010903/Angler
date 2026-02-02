using StudentRegistration.Models.DTOs;
using StudentRegistration.Models.Entities;

namespace StudentRegistration.Repositories.Interface
{
    public interface IManageContact
    {
        void SaveContact(Contact contact);
        List<ContactDTO> GetAllContacts();
        bool CheckForDuplicateEmails(string Email);
        int DeleteContact(int ContactId);
        ContactDTO GetContactById(int ContactId);
        int UpdateContactBy(Contact contact);
    }
}
