﻿using AnimalAllies.Accounts.Domain;
using AnimalAllies.Core.Abstractions;
using AnimalAllies.Core.Database;
using AnimalAllies.Core.Extension;
using AnimalAllies.SharedKernel.Constraints;
using AnimalAllies.SharedKernel.Shared;
using AnimalAllies.SharedKernel.Shared.ValueObjects;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalAllies.Accounts.Application.AccountManagement.Commands.AddSocialNetworks;

public class AddSocialNetworkHandler: ICommandHandler<AddSocialNetworkCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AddSocialNetworkHandler> _logger;
    private readonly IValidator<AddSocialNetworkCommand> _validator;

    public AddSocialNetworkHandler(
        [FromKeyedServices(Constraints.Context.Accounts)]IUnitOfWork unitOfWork,
        UserManager<User> userManager, 
        ILogger<AddSocialNetworkHandler> logger, 
        IValidator<AddSocialNetworkCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result> Handle(AddSocialNetworkCommand command, CancellationToken cancellationToken = default)
    {
        var validatorResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validatorResult.IsValid)
            return validatorResult.ToErrorList();

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);
        if (user is null)
            return Errors.General.NotFound();

        var socialNetworks = command.SocialNetworkDtos
            .Select(s => SocialNetwork.Create(s.Title, s.Url).Value);

        user.AddSocialNetwork(socialNetworks);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Added social networks to user with id {id}", command.UserId);
        
        return Result.Success();
    }
}