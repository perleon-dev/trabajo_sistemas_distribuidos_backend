﻿using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Domain.Aggregates.PreContractAggregate;
using Contracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using Contracts.Api.Domain.Util;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Contracts.Api.Application.Commands.PreContractCommands
{
	public class UpdateMassiveStatePreContractCommandHandler : IRequestHandler<UpdateMassiveStatePreContractCommand, MessageResponse>
	{
		readonly IPreContractTradenameQuery _iPreContractTradenameQuery;
		readonly IPreContractRepository _iPreContractRepository;

		public UpdateMassiveStatePreContractCommandHandler(IPreContractRepository iPreContractRepository, IPreContractTradenameQuery iPreContractTradenameQuery)
		{
			_iPreContractRepository = iPreContractRepository;
			_iPreContractTradenameQuery = iPreContractTradenameQuery;
		}

		public async Task<MessageResponse> Handle(UpdateMassiveStatePreContractCommand request, CancellationToken cancellationToken)
		{
			try
			{

				var preContractsToUpdate = new List<Contracts.Api.Domain.Aggregates.PreContractAggregate.PreContract>();
				var preContractTradenamesToUpdate = new List<PreContractTradename>();

				foreach (var preContract in request.preContractList)
					preContractsToUpdate.Add(new Contracts.Api.Domain.Aggregates.PreContractAggregate.PreContract(preContract.contractId, preContract.contractVersion, preContract.contractModification, request.state, request.updateUserId, request.updateUserFullname));

				await _iPreContractRepository.UpdateStateJson(preContractsToUpdate, preContractTradenamesToUpdate);

				return new MessageResponse()
				{
					codeStatus = CodeStatus.Create,
					message = "Actualización satisfactoria"
				};
			}
			catch
			{
				return new MessageResponse()
				{
					codeStatus = CodeStatus.InternalError,
					message = "Ocurrió un error"
				};

			}
		}
	}
}
