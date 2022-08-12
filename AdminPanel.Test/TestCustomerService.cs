using AdminPanel.Data;
using AdminPanel.Data.Models;
using AdminPanel.Services.Customer;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
namespace AdminPanel.Test
{
    public class TestCustomerService
    {
        [Fact]
        public void CustomerService_GetsAllCustomers_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<AdminDbContext>()
                .UseInMemoryDatabase("gets_all").Options;

            using var context = new AdminDbContext(options);

            var sut = new CustomerService(context);

            sut.CreateCustomer(new Customer {
                Id = 123123,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                FirstName = "qwer",
                LastName = "asdf",
                PrimaryAddress = new CustomerAddress
                {
                    Id = 123,
                    CreatedOn = DateTime.Now,
                    AddressLine1 = "q",
                    AddressLine2 = "w",
                    City = "asdf",
                    Country = "zxc",
                    PostalCode = "1234",
                    State = "asdfg",
                    UpdatedOn = DateTime.Now
                }
 
            });
            sut.CreateCustomer(new Customer
            {
                Id = -1231,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                FirstName = "qer",
                LastName = "as",
                PrimaryAddress = new CustomerAddress
                {
                    Id = 13,
                    CreatedOn = DateTime.Now,
                    AddressLine1 = "q",
                    AddressLine2 = "w",
                    City = "asdf",
                    Country = "zxc",
                    PostalCode = "1234",
                    State = "asdfg",
                    UpdatedOn = DateTime.Now
                }

            });
            var allCustomers = sut.GetAllCustomers();

            allCustomers.Count.Should().Be(2);
        }


        [Fact]
        public void CustomerService_CreatesCustomer_GivenNewCustomerObject()
        {
            var options = new DbContextOptionsBuilder<AdminDbContext>()
                .UseInMemoryDatabase("Add_writes_to_database").Options;

            using var context = new AdminDbContext(options);
            var service = new CustomerService(context);

            service.CreateCustomer(new Customer
            {
                Id = 123123,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                FirstName = "qwer",
                LastName = "asdf",
                PrimaryAddress = new CustomerAddress
                {
                    Id = 123,
                    CreatedOn = DateTime.Now,
                    AddressLine1 = "q",
                    AddressLine2 = "w",
                    City = "asdf",
                    Country = "zxc",
                    PostalCode = "1234",
                    State = "asdfg",
                    UpdatedOn = DateTime.Now
                }
            });
            context.Customers.Single().Id.Should().Be(123123); 
        }

        [Fact]
        public void CustomerService_DeletesCustomer_GivenId()
        {
            var options = new DbContextOptionsBuilder<AdminDbContext>()
                .UseInMemoryDatabase("deletes_one")
                .Options;

            using var context = new AdminDbContext(options);
            var service = new CustomerService(context);

            service.CreateCustomer(new Customer
            {
                Id = 123123,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                FirstName = "qwer",
                LastName = "asdf",
                PrimaryAddress = new CustomerAddress
                {
                    Id = 123,
                    CreatedOn = DateTime.Now,
                    AddressLine1 = "q",
                    AddressLine2 = "w",
                    City = "asdf",
                    Country = "zxc",
                    PostalCode = "1234",
                    State = "asdfg",
                    UpdatedOn = DateTime.Now
                }
            });

            service.DeleteCustomer(123123);
            var allCustomers = service.GetAllCustomers();
            allCustomers.Count.Should().Be(0);
        }


        [Fact]
        public void CustomerService_OrdersByLastName_WhenGetAllCustomersInvoked()
        {
            var data = new List<Customer>
            {
                new Customer
            {
                Id = 1,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                FirstName = "qwer",
                LastName = "Zulu",
                PrimaryAddress = new CustomerAddress
                {
                    Id = 123,
                    CreatedOn = DateTime.Now,
                    AddressLine1 = "q",
                    AddressLine2 = "w",
                    City = "asdf",
                    Country = "zxc",
                    PostalCode = "1234",
                    State = "asdfg",
                    UpdatedOn = DateTime.Now
                }
            },
                new Customer
            {
                Id = 2,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                FirstName = "qwer",
                LastName = "Alpha",
                PrimaryAddress = new CustomerAddress
                {
                    Id = 12,
                    CreatedOn = DateTime.Now,
                    AddressLine1 = "q",
                    AddressLine2 = "w",
                    City = "asdf",
                    Country = "zxc",
                    PostalCode = "1234",
                    State = "asdfg",
                    UpdatedOn = DateTime.Now
                }
            },
                new Customer
            {
                Id = 3,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                FirstName = "qwer",
                LastName = "Tango",
                PrimaryAddress = new CustomerAddress
                {
                    Id = 13,
                    CreatedOn = DateTime.Now,
                    AddressLine1 = "q",
                    AddressLine2 = "w",
                    City = "asdf",
                    Country = "zxc",
                    PostalCode = "1234",
                    State = "asdfg",
                    UpdatedOn = DateTime.Now
                }
            }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            var mockContext = new Mock<AdminDbContext>();

            mockContext.Setup(c => c.Customers)
                .Returns(mockSet.Object);

            // Act
            var sut = new CustomerService(mockContext.Object);
            var customers = sut.GetAllCustomers();

            // Assert
            customers.Count.Should().Be(3);
            customers[0].Id.Should().Be(2);
            customers[1].Id.Should().Be(3);
            customers[2].Id.Should().Be(1);
        }
    }
}

