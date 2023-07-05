namespace TCG.Common.MassTransit.Events;

public record OrderCreatedEvent(Guid CorrelationId, Guid PostId, char Status, int UserId);
