﻿using System.Text;
using AnimalAllies.Core.Abstractions;
using AnimalAllies.Core.Database;
using AnimalAllies.Core.DTOs;
using AnimalAllies.Core.Extension;
using AnimalAllies.Core.Models;
using AnimalAllies.SharedKernel.Constraints;
using AnimalAllies.SharedKernel.Shared;
using Dapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalAllies.Species.Application.SpeciesManagement.Queries.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationHandlerDapper : IQueryHandler<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IValidator<GetSpeciesWithPaginationQuery> _validator;
    private readonly ILogger<GetSpeciesWithPaginationHandlerDapper> _logger;

    public GetSpeciesWithPaginationHandlerDapper(
        [FromKeyedServices(Constraints.Context.BreedManagement)]ISqlConnectionFactory sqlConnectionFactory,
        ILogger<GetSpeciesWithPaginationHandlerDapper> logger, IValidator<GetSpeciesWithPaginationQuery> validator)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _logger = logger;
        _validator = validator;
    }
    
    public async Task<Result<PagedList<SpeciesDto>>> Handle(GetSpeciesWithPaginationQuery query, CancellationToken cancellationToken = default)
    {
        var validatorResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validatorResult.IsValid)
            return validatorResult.ToErrorList();

        var connection = _sqlConnectionFactory.Create();

        var parameters = new DynamicParameters();

        var sql = new StringBuilder("""
                                    select 
                                        id,
                                        name
                                        from species.species
                                    """);
        
        sql.ApplySorting(query.SortBy, query.SortDirection);
        sql.ApplyPagination(query.Page,query.PageSize);

        var species = await connection.QueryAsync<SpeciesDto>(sql.ToString(), parameters);
        
        _logger.LogInformation("Get species with pagination Page: {Page}, PageSize: {PageSize}",
            query.Page, query.PageSize);

        var speciesDtos = species.ToList();
        
        return new PagedList<SpeciesDto>
        {
            Items = speciesDtos.ToList(),
            PageSize = query.PageSize,
            Page = query.Page,
            TotalCount = speciesDtos.Count()
        };

    }
    
    public async Task<Result<List<SpeciesDto>>> Handle(CancellationToken cancellationToken = default)
    {

        var connection = _sqlConnectionFactory.Create();
        

        var sql = new StringBuilder("""
                                    select 
                                        id,
                                        name
                                        from species.species
                                    """);
        

        var species = await connection.QueryAsync<SpeciesDto>(sql.ToString());
        
        _logger.LogInformation("Get species with pagination Page");


        return species.ToList();
    }
}