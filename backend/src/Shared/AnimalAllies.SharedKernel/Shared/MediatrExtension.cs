﻿using AnimalAllies.SharedKernel.Shared.Ids;
using AnimalAllies.SharedKernel.Shared.Objects;
using MediatR;

namespace AnimalAllies.SharedKernel.Shared;

public static class MediatrExtension
{
    public static async Task PublishDomainEvents<TId>(
        this IPublisher publisher,
        DomainEntity<TId> entity,
        CancellationToken cancellationToken = default) where TId: BaseId<TId>
    {
        foreach (var @event in entity.DomainEvents)
        {
            await publisher.Publish(@event, cancellationToken);
            entity.RemoveDomainEvent(@event);
        }
    }
    
    public static async Task PublishDomainEvent(
        this IPublisher publisher,
        IDomainEvent @event,
        CancellationToken cancellationToken = default)
    {
        await publisher.Publish(@event, cancellationToken);
    }
}