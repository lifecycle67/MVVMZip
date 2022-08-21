using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.DataAccess;
using DemoApp.Model;
using System.Globalization;

namespace DemoApp.ViewModel.Tests
{
    [TestClass()]
    public class AllCustomersViewModelTests
    {
        private AllCustomersViewModel _sut;
        private CustomerRepositoryStub _customerRepository;

        public AllCustomersViewModelTests()
        {
            _customerRepository = new CustomerRepositoryStub();
            _sut = new AllCustomersViewModel(_customerRepository);
        }

        /// <summary>
        /// CustomerRepository의 데이터가 전체 고객 목록으로 구성되었는지 검증
        /// </summary>
        [TestMethod()]
        public void AllCustomerListTest()
        {
            int count = 0;
            bool isValid = true;
            foreach (CustomerViewModel customerVm in _sut.AllCustomersView.SourceCollection)
            {
                count++;
                var cust = Customer.CreateCustomer(customerVm.TotalSales,
                                                   customerVm.FirstName,
                                                   customerVm.LastName,
                                                   customerVm.IsCompany,
                                                   customerVm.Email);
                if (_customerRepository.ContainsCustomer(cust) == false)
                    isValid = false;
            }

            Assert.AreEqual(count, 5);
            Assert.AreEqual(isValid, true);
        }

        /// <summary>
        /// 전체 고객 목록을 회사와 개인으로 분류하는지 검증
        /// </summary>
        [TestMethod()]
        public void AllCustomersViewTestGroupedByIsCompany()
        {
            Assert.AreEqual(_sut.AllCustomersView.CanGroup, true);
            Assert.AreEqual(_sut.AllCustomersView.GroupDescriptions.Count, 1);
            foreach (CustomerViewModel customer in _sut.AllCustomersView.SourceCollection)
            {
                Assert.AreEqual(
                    _sut.AllCustomersView.GroupDescriptions[0].GroupNameFromItem(customer, 0, CultureInfo.CurrentCulture),
                    customer.IsCompany);
            }
        }
    }

    public class CustomerRepositoryStub : ICustomerRepository
    {
        public event EventHandler<CustomerAddedEventArgs> CustomerAdded;
        List<Customer> _customerList = new List<Customer>();

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (!_customerList.Contains(customer))
            {
                _customerList.Add(customer);

                this.CustomerAdded?.Invoke(this, new CustomerAddedEventArgs(customer));
            }
        }

        public bool ContainsCustomer(Customer customer)
        {
            return _customerList.Contains(customer, new ValueEqualityComparer());
        }

        public List<Customer> GetCustomers()
        {
            _customerList.Add(Customer.CreateCustomer(1000, "company1", null, true, "company1@email.com"));
            _customerList.Add(Customer.CreateCustomer(2000, "company1", null, true, "company2@email.com"));
            _customerList.Add(Customer.CreateCustomer(3000, "company3", null, true, "company3@email.com"));
            _customerList.Add(Customer.CreateCustomer(100, "per1", "son1", false, "person1@email.com"));
            _customerList.Add(Customer.CreateCustomer(200, "per2", "son2", false, "person2@email.com"));

            return _customerList;
        }
    }

    public class ValueEqualityComparer : EqualityComparer<Customer>
    {
        public override bool Equals(Customer x, Customer y)
        {
            return x.TotalSales == y.TotalSales &&
                x.FirstName == y.FirstName &&
                x.LastName == y.LastName &&
                x.IsCompany == y.IsCompany &&
                x.Email == y.Email;
        }

        public override int GetHashCode(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}