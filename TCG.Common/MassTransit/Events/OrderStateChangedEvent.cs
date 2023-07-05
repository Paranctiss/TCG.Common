namespace TCG.Common.MassTransit.Events;

public record OrderStateChangedEvent(Guid CorrelationId, Guid PostId);
