using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestNinja4.Mocking;

namespace TestNinja4.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private HousekeeperService _service;
        private DateTime _statementDate = new DateTime(2017, 1, 1);
        private Housekeeper _housekeeper;
        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            var unitOfWork = new Mock<IUnitOfWork>();            
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementFileName = "filename";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }
        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_HouseKeeperEmailIsInvalid_ShouldNotGenerateStatements(string emailContent)
        {
            _housekeeper.Email = emailContent;

            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                Times.Never);
        }
        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);
            VerifyEmailSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_StatementFileNameIsInvalid_ShouldNotEmailTheStatement(string statementeFileName)
        {
            _statementFileName = statementeFileName;

            _service.SendStatementEmails(_statementDate);
            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                    ))
                .Throws<Exception>();

            _service.SendStatementEmails(_statementDate);
            VerifyMessageBoxDisplay();
        }

        private void VerifyMessageBoxDisplay()
        {
            _messageBox.Verify(mb => mb.Show(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                                It.IsAny<string>(),
                                It.IsAny<string>(),
                                It.IsAny<string>(),
                                It.IsAny<string>()),
                            Times.Never);
        }
        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                            _housekeeper.Email,
                            _housekeeper.StatementEmailBody,
                            _statementFileName,
                            It.IsAny<string>()));
        }
    }
}
