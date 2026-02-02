using StudentRegistration.DBHelper;
using StudentRegistration.Repositories.Interface;
using Dapper;
using StudentRegistration.Models.Entities;
using StudentRegistration.Models.DTOs;

namespace StudentRegistration.Repositories.Implementation
{
    public class ManageContact : IManageContact
    {
        private readonly IDBConnection _dBConnection;
        public ManageContact(IDBConnection dBConnection)
        {
            _dBConnection = dBConnection;
        }

        public List<ContactDTO> GetAllContacts()
        {
            using (var connection = _dBConnection.GetConnection())
            {
                var query = @"SELECT [ContactID]
           ,[Name]
           , case when [Gender] = '0' then 'Male'
		   when [Gender] = '1' then 'Female'
		   End as [Gender]
           ,[Email]
           ,[Address]
           ,[State]
           ,[Zip]
           ,[Country]
           ,[Phone] FROM Contacts";
                var contacts = connection.Query<ContactDTO>(query).ToList();
                return contacts;
            }
        }

        public void SaveContact(Contact contact)
        {
            using (var connection = _dBConnection.GetConnection())
            {
                var query = @"INSERT INTO [dbo].[Contacts]
           ([Name]
           ,[Gender]
           ,[Email]
           ,[Address]
           ,[State]
           ,[Zip]
           ,[Country]
           ,[Phone])
     VALUES
           (@Name
            ,@Gender
           ,@Email
           ,@Address
           ,@State
           ,@Zip
           ,@Country
           ,@Phone)";

                var parameters = new { Name = contact.Name, Gender = contact.Gender, Email = contact.Email, Address = contact.Address, State = contact.State, Zip = contact.Zip, Country = contact.Country, Phone = contact.Phone };

                connection.Execute(query, parameters);
            }
        }

        public bool CheckForDuplicateEmails(string Email)
        {
            using (var connection = _dBConnection.GetConnection())
            {
                var query = "SELECT * FROM Contacts where Email = @Email";
                return connection.Query<Contact>(query, new {Email = Email}).Any();
            }
        }

        public int DeleteContact(int ContactId)
        {
            using (var connection = _dBConnection.GetConnection())
            {
                var query = "Delete Contacts where ContactId = @ContactId";
                return connection.Execute(query, new { ContactId = ContactId });
            }
        }

        public ContactDTO GetContactById(int ContactId)
        {
            using (var connection = _dBConnection.GetConnection())
            {
                var query = @"SELECT [ContactID]
           ,[Name]
           , case when [Gender] = '0' then 'Male'
		   when [Gender] = '1' then 'Female'
		   End as [Gender]
           ,[Email]
           ,[Address]
           ,[State]
           ,[Zip]
           ,[Country]
           ,[Phone] FROM Contacts where ContactID = @ContactID";
                return connection.Query<ContactDTO>(query, new { ContactID = ContactId }).First();
            }
        }

        public int UpdateContactBy(Contact contact)
        {
            using (var connection = _dBConnection.GetConnection())
            {
                var query = @"update Contacts set Name = @Name, Gender = @Gender, Email = @Email, Address = @Address, State = @State, Zip = @Zip, Country = @Country, Phone = @Phone where ContactID = @ContactID";
                var parameters = new { Name = contact.Name, Gender = contact.Gender, Email = contact.Email, Address = contact.Address, State = contact.State, Zip = contact.Zip, Country = contact.Country, Phone = contact.Phone };
                return connection.Execute(query, parameters);
            }
        }
    }
}
