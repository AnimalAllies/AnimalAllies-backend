﻿using AnimalAllies.Core.Validators;
using AnimalAllies.SharedKernel.Shared;
using AnimalAllies.SharedKernel.Shared.Errors;
using FluentValidation;

namespace AnimalAllies.Accounts.Application.AccountManagement.Commands.Refresh;

public class RefreshTokensCommandValidator: AbstractValidator<RefreshTokensCommand>
{
    public RefreshTokensCommandValidator()
    {
        RuleFor(r => r.AccessToken)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("access token"));
        
        RuleFor(r => r.RefreshToken)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("refresh token"));
    }
}