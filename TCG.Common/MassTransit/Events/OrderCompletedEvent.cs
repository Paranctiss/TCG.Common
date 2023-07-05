namespace TCG.Common.MassTransit.Events;

public record OrderCompletedEvent(Guid CorrelationId, Guid PostId);