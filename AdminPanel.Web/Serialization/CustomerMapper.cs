using AdminPanel.Data.Models;
using AdminPanel.Web.ViewModels;

namespace AdminPanel.Web.Serialization
{
    public class CustomerMapper
    {
        /// <summary>
        /// Serializes a Customer data model to a CustomerModel view model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static CustomerModel SerializeCustomer(Customer customer)
        {
            var address = new CustomerAddressModel
            {
                Id = customer.Id,
                AddressLine1 = customer.PrimaryAddress.AddressLine1,
                AddressLine2 = customer.PrimaryAddress.AddressLine2,
                City = customer.PrimaryAddress.City,
                State = customer.PrimaryAddress.State,
                Country = customer.PrimaryAddress.Country,
                PostalCode = customer.PrimaryAddress.PostalCode,
                CreatedOn = customer.PrimaryAddress.CreatedOn,
                UpdatedOn = customer.PrimaryAddress.UpdatedOn
            };

            return new CustomerModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = address
            };
        }
        /// <summary>
        /// Serializes a CustomerModel view model to Customer data model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Customer SerializeCustomer(CustomerModel customer)
        {
            var address = new CustomerAddress
            {
                Id = customer.Id,
                AddressLine1 = customer.PrimaryAddress.AddressLine1,
                AddressLine2 = customer.PrimaryAddress.AddressLine2,
                City = customer.PrimaryAddress.City,
                State = customer.PrimaryAddress.State,
                Country = customer.PrimaryAddress.Country,
                PostalCode = customer.PrimaryAddress.PostalCode,
                CreatedOn = customer.PrimaryAddress.CreatedOn,
                UpdatedOn = customer.PrimaryAddress.UpdatedOn
            };

            return new Customer
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = address
            };
        }
    }
}
