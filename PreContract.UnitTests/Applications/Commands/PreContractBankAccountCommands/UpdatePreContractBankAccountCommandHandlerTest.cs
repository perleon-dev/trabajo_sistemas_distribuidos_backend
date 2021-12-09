﻿using PreContracts.Api.Application.Commands.PreContractBankAccountCommands;
using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Commands.PreContractBankAccountCommands
{
    public class UpdatePreContractBankAccountCommandHandlerTest
    {
        private readonly UpdatePreContractBankAccountCommandHandler _sut;
        private readonly Mock<IPreContractBankAccountRepository> _IPreContractBankAccountRepository;
        private readonly Mock<IValuesSettings> _IValuesSettings;

        public UpdatePreContractBankAccountCommandHandlerTest()
        {
            this._IPreContractBankAccountRepository = new Mock<IPreContractBankAccountRepository>();
            this._IValuesSettings = new Mock<IValuesSettings>();
            this._sut = new UpdatePreContractBankAccountCommandHandler(this._IPreContractBankAccountRepository.Object, this._IValuesSettings.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettings.Setup(x => x.GetTimeZone()).Returns(timeZone);

            UpdatePreContractBankAccountCommand command = new UpdatePreContractBankAccountCommand();
            int response = 0;
            _IPreContractBankAccountRepository.Setup(x => x.Register(It.IsAny<PreContractBankAccount>())).Returns(Task.FromResult(response));
            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());

            current.Should().Be(response);
            _IPreContractBankAccountRepository.VerifyAll();
        }

    }
}
