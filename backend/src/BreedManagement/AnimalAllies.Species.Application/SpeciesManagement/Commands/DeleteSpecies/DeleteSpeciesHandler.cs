﻿using AnimalAllies.Core.Abstractions;
using AnimalAllies.Core.Database;
using AnimalAllies.Core.Extension;
using AnimalAllies.SharedKernel.Constraints;
using AnimalAllies.SharedKernel.Shared;
using AnimalAllies.SharedKernel.Shared.Errors;
using AnimalAllies.SharedKernel.Shared.Ids;
using AnimalAllies.Species.Application.Repository;
using AnimalAllies.Volunteer.Contracts;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalAllies.Species.Application.SpeciesManagement.Commands.DeleteSpecies;

public class DeleteSpeciesHandler: ICommandHandler<DeleteSpeciesCommand, SpeciesId>
{
    private readonly ISpeciesRepository _repository;
    private readonly IVolunteerContract _volunteerContract;
    private readonly IValidator<DeleteSpeciesCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesHandler> _logger;

    public DeleteSpeciesHandler(
        ISpeciesRepository repository,
        IValidator<DeleteSpeciesCommand> validator,
        ILogger<DeleteSpeciesHandler> logger, 
        [FromKeyedServices(Constraints.Context.BreedManagement)]IUnitOfWork unitOfWork,
        IVolunteerContract volunteerContract)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _volunteerContract = volunteerContract;
    }

    public async Task<Result<SpeciesId>> Handle(DeleteSpeciesCommand command, CancellationToken cancellationToken = default)
    {
        var validatorResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validatorResult.IsValid)
            return validatorResult.ToErrorList();

        var speciesId = SpeciesId.Create(command.SpeciesId);

        var species = await _repository.GetById(speciesId, cancellationToken);
        if (species.IsFailure)
            return Errors.General.NotFound();

        var petOfThisSpecies = await _volunteerContract
            .CheckIfPetBySpeciesIdExist(command.SpeciesId,cancellationToken);
        
        if (petOfThisSpecies.IsFailure || petOfThisSpecies.Value)
            return Errors.Species.DeleteConflict();
        
        var result =  _repository.Delete(species.Value, cancellationToken);
        if (result.IsFailure)
            return Error.Failure("delete.species.failure", "species deletion failed");

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Species with id {speciesId} has been deleted", speciesId.Id);

        return speciesId;
    }
}