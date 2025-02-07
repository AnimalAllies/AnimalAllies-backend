﻿using AnimalAllies.Core.Abstractions;
using AnimalAllies.Core.Database;
using AnimalAllies.Core.Extension;
using AnimalAllies.SharedKernel.Constraints;
using AnimalAllies.SharedKernel.Shared;
using AnimalAllies.SharedKernel.Shared.Ids;
using Discussion.Application.Repository;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Discussion.Application.Features.Commands.CloseDiscussion;

public class CloseDiscussionHandler: ICommandHandler<CloseDiscussionCommand, DiscussionId>
{
    private readonly ILogger<CloseDiscussionHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CloseDiscussionCommand> _validator;
    private readonly IDiscussionRepository _repository;

    public CloseDiscussionHandler(
        ILogger<CloseDiscussionHandler> logger, 
        [FromKeyedServices(Constraints.Context.Discussion)]IUnitOfWork unitOfWork, 
        IValidator<CloseDiscussionCommand> validator,
        IDiscussionRepository repository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result<DiscussionId>> Handle(
        CloseDiscussionCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var discussionId = DiscussionId.Create(command.DiscussionId);

        var discussion = await _repository.GetById(discussionId, cancellationToken);
        if (discussion.IsFailure)
            return discussion.Errors;

        var result = discussion.Value.CloseDiscussion(command.UserId);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("user with id {userId} closed discussion with id {discussionId}",
            command.UserId, command.DiscussionId);

        return discussionId;
    }
}