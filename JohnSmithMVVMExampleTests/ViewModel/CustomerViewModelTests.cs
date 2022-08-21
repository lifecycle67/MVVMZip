using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Model;
using System.ComponentModel;
using DemoApp.Properties;

namespace DemoApp.ViewModel.Tests
{
    [TestClass()]
    public class CustomerViewModelTests
    {
        /// <summary>
        /// 신규 고객 입력 정보에 대한 유효성 검사 수행 결과를 검증
        /// </summary>
        [TestMethod()]
        public void InvalidUserCustomerTest()
        {
            var invalidUserCustomer = Customer.CreateCustomer(100, "", "", false, "");
            CustomerRepositoryStub customerRepository = new CustomerRepositoryStub();
            CustomerViewModel sut = new CustomerViewModel(invalidUserCustomer, customerRepository);
            Assert.AreEqual((sut as IDataErrorInfo)[nameof(CustomerViewModel.FirstName)], Strings.Customer_Error_MissingFirstName);
            Assert.AreEqual((sut as IDataErrorInfo)[nameof(CustomerViewModel.LastName)], Strings.Customer_Error_MissingLastName);
            Assert.AreEqual((sut as IDataErrorInfo)[nameof(CustomerViewModel.Email)], Strings.Customer_Error_MissingEmail);
            Assert.AreEqual((sut as IDataErrorInfo)[nameof(CustomerViewModel.CustomerType)], Strings.CustomerViewModel_Error_MissingCustomerType);
            Assert.AreEqual(invalidUserCustomer.IsValid, false);
        }

        /// <summary>
        /// 신규 고객 정보 추가 동작에 대한 검증
        /// </summary>
        [TestMethod()]
        public void SaveTest()
        {
            var addCustomer = Customer.CreateCustomer(100, "anakin", "skywalker", false, "starwars@iamyourfather.com");
            CustomerRepositoryStub customerRepository = new CustomerRepositoryStub();
            CustomerViewModel sut = new CustomerViewModel(addCustomer, customerRepository);
            sut.Save();

            Assert.IsTrue(customerRepository.ContainsCustomer(addCustomer));
        }
    }
}