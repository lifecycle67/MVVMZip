using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Properties;

namespace DemoApp.ViewModel.Tests
{
    [TestClass()]
    public class MainWindowViewModelTests
    {
        /// <summary>
        /// 전체 고객 목록 보기 명령에 의해 전체 고객 목록 작업 영역이 생성되는지 검증
        /// </summary>
        [TestMethod()]
        public void AddViewAllCustomerWorkspaceTest()
        {
            CustomerRepositoryStub customerRepository = new CustomerRepositoryStub();
            MainWindowViewModel sut = new MainWindowViewModel(customerRepository);

            var cmd = sut.Commands.Where(c => c.DisplayName == Strings.MainWindowViewModel_Command_ViewAllCustomers).FirstOrDefault();
            Assert.IsNotNull(cmd);
            cmd.Command.Execute(null);
            Assert.IsTrue(sut.Workspaces.Any(w => w.GetType().Equals(typeof(AllCustomersViewModel))));
            Assert.AreEqual(sut.Workspaces.Count, 1);
        }

        /// <summary>
        /// 고객 추가 명령에 의해 고객 추가 작업 영역이 생성되는지 검증
        /// </summary>
        [TestMethod()]
        public void AddNewCustomerWorkspaceTest()
        {
            CustomerRepositoryStub customerRepository = new CustomerRepositoryStub();
            MainWindowViewModel sut = new MainWindowViewModel(customerRepository);
            var cmd = sut.Commands.Where(c => c.DisplayName == Strings.MainWindowViewModel_Command_CreateNewCustomer).FirstOrDefault();
            Assert.IsNotNull(cmd);
            cmd.Command.Execute(null);
            Assert.IsTrue(sut.Workspaces.Any(w => w.GetType().Equals(typeof(CustomerViewModel))));
            Assert.AreEqual(sut.Workspaces.Count, 1);
        }

        /// <summary>
        /// 여러 개의 작업 영역을 추가했을 때 검증
        /// </summary>
        [TestMethod()]
        public void AddMultipleWorkspacesTest()
        {
            CustomerRepositoryStub customerRepository = new CustomerRepositoryStub();
            MainWindowViewModel sut = new MainWindowViewModel(customerRepository);

            var viewAllCustomerCmd = sut.Commands.Where(c => c.DisplayName == Strings.MainWindowViewModel_Command_ViewAllCustomers).FirstOrDefault();
            var newCustomerCmd = sut.Commands.Where(c => c.DisplayName == Strings.MainWindowViewModel_Command_CreateNewCustomer).FirstOrDefault();
            Assert.IsNotNull(viewAllCustomerCmd);
            Assert.IsNotNull(newCustomerCmd);

            viewAllCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);
            viewAllCustomerCmd.Command.Execute(null);

            Assert.AreEqual(sut.Workspaces.Count, 5);
            Assert.AreEqual(sut.Workspaces.Count(w => w.GetType().Equals(typeof(AllCustomersViewModel))), 1);
            Assert.AreEqual(sut.Workspaces.Count(w => w.GetType().Equals(typeof(CustomerViewModel))), 4);
        }

        /// <summary>
        /// 전체 고객 목록 작업 영역 닫았을 때 작업 영역에서 제거되는지 검증
        /// </summary>
        [TestMethod()]
        public void CloseViewAllCustomerWorkspaceTest()
        {
            CustomerRepositoryStub customerRepository = new CustomerRepositoryStub();
            MainWindowViewModel sut = new MainWindowViewModel(customerRepository);

            var viewAllCustomerCmd = sut.Commands.Where(c => c.DisplayName == Strings.MainWindowViewModel_Command_ViewAllCustomers).FirstOrDefault();
            var newCustomerCmd = sut.Commands.Where(c => c.DisplayName == Strings.MainWindowViewModel_Command_CreateNewCustomer).FirstOrDefault();
            Assert.IsNotNull(viewAllCustomerCmd);
            Assert.IsNotNull(newCustomerCmd);

            viewAllCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);

            var workspace = sut.Workspaces.FirstOrDefault(w => w.GetType().Equals(typeof(AllCustomersViewModel)));
            Assert.IsNotNull(workspace);

            workspace.CloseCommand.Execute(null);
            Assert.AreEqual(sut.Workspaces.Count(w => w.GetType().Equals(typeof(AllCustomersViewModel))), 0);
            Assert.AreEqual(sut.Workspaces.Count, 3);
        }

        /// <summary>
        /// 새 고객 작업 영역을 닫았을 때 작업 영역에서 제거되는지 검증
        /// </summary>
        [TestMethod()]
        public void CloseNewCustomerWorkspaceTest()
        {
            CustomerRepositoryStub customerRepository = new CustomerRepositoryStub();
            MainWindowViewModel sut = new MainWindowViewModel(customerRepository);

            var viewAllCustomerCmd = sut.Commands.Where(c => c.DisplayName == Strings.MainWindowViewModel_Command_ViewAllCustomers).FirstOrDefault();
            var newCustomerCmd = sut.Commands.Where(c => c.DisplayName == Strings.MainWindowViewModel_Command_CreateNewCustomer).FirstOrDefault();
            Assert.IsNotNull(viewAllCustomerCmd);
            Assert.IsNotNull(newCustomerCmd);

            viewAllCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);
            newCustomerCmd.Command.Execute(null);

            var workspace = sut.Workspaces.FirstOrDefault(w => w.GetType().Equals(typeof(CustomerViewModel)));
            Assert.IsNotNull(workspace);
            workspace.CloseCommand.Execute(null);

            Assert.AreEqual(sut.Workspaces.Count(w => w.GetType().Equals(typeof(CustomerViewModel))), 2);
            Assert.AreEqual(sut.Workspaces.Count, 3);

            workspace = sut.Workspaces.FirstOrDefault(w => w.GetType().Equals(typeof(CustomerViewModel)));
            Assert.IsNotNull(workspace);
            workspace.CloseCommand.Execute(null);

            Assert.AreEqual(sut.Workspaces.Count(w => w.GetType().Equals(typeof(CustomerViewModel))), 1);
            Assert.AreEqual(sut.Workspaces.Count, 2);
        }
    }
}