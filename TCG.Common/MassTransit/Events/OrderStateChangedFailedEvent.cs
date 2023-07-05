namespace TCG.Common.MassTransit.Events;

public record OrderStateChangedFailedEvent(Guid CorrelationId, Guid PostId, string ErrorMessage);
